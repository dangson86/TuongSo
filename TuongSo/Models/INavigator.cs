using DomainContext.Generics;
using System.Windows.Input;
using TuongSo.ViewModels;

namespace TuongSo.Models
{
    public interface INavigator
    {
        BaseViewModel CurrentViewModel { get; }
        ICommand UpdateViewModel { get; }
        void NavigateToVM<T>() where T : BaseViewModel;
    }
}
