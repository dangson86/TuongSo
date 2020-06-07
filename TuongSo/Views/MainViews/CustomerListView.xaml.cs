using DomainContext.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TuongSo.Navigators;
using TuongSo.ViewModels;

namespace TuongSo.Views.MainViews
{
    /// <summary>
    /// Interaction logic for CustomerListView.xaml
    /// </summary>
    public partial class CustomerListView : UserControl
    {
        public CustomerListVM Context => this.DataContext as CustomerListVM;
        public CustomerListView()
        {
            InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await Context.GetCustomerList();
        }

        private void LoadCustomer_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;

            if (btn?.DataContext is Customer c)
            {
                this.Context.SetSelectedCustomer(c.Id);
            }
        }

        private async void RemoveCustomer_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn?.DataContext is Customer c)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    await this.Context.DeleteCustomer(c.Id);
                }
            }
        }
    }
}
