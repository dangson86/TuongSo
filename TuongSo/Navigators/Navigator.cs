using DomainContext.Generics;
using GeneralDI;
using Microsoft.Practices.Unity;
using System;
using System.ComponentModel;
using TuongSo.Models;
using TuongSo.ViewModels;

namespace TuongSo.Navigators
{
    public class Navigator : INavigator, INotifyPropertyChanged
    {
        static BaseViewModel _ViewModel;

        public event PropertyChangedEventHandler PropertyChanged;
        private static readonly IUnityContainer _services = TuongSoServiceContainer.GetCollection();
        public BaseViewModel CurrentViewModel
        {
            get => _ViewModel;
            set
            {
                _ViewModel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentViewModel)));
            }
        }
        public IUnityContainer ServicesDI => _services;

        public System.Windows.Input.ICommand UpdateViewModel { get; }
        public Type CurrentViewModelType { get; internal set; }

        public Navigator()
        {
            UpdateViewModel = new UpdateViewControllerCommand(this);
            UpdateViewModel.Execute(typeof(PyCalVM));
        }
    }
    public class UpdateViewControllerCommand : System.Windows.Input.ICommand
    {
        private readonly Navigator nav;

        public event EventHandler CanExecuteChanged;
        public UpdateViewControllerCommand(Navigator nav)
        {
            this.nav = nav;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is Type t)
            {
                var baseVcType = typeof(BaseViewModel);
                if (baseVcType.IsAssignableFrom(t))
                {
                    var vc = this.nav.ServicesDI.Resolve(t);
                    this.nav.CurrentViewModel = vc as BaseViewModel;
                    this.nav.CurrentViewModelType = t;
                }
            }
        }
    }
}
