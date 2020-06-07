using DomainContext;
using DomainContext.Entities;
using DomainContext.Generics;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace TuongSo.ViewModels
{
    public class CustomerListVM : BaseViewModel
    {
        private readonly LocalDomainContext domainContext;
        public ObservableCollection<Customer> Customers { get; private set; } = new ObservableCollection<Customer>();
        public CustomerListVM()
        {

        }
        public CustomerListVM(LocalDomainContext domainContext)
        {
            this.domainContext = domainContext;
        }

        public async Task GetCustomerList()
        {
            var customerList = await domainContext.Customers.ToListAsync();
            this.Customers.AddRange(customerList);
        }
    }
}
