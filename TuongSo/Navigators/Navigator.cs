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
        static BaseVC _ViewModel;

        public event PropertyChangedEventHandler PropertyChanged;
        private static readonly IUnityContainer _services = TuongSoServiceCollection.GetCollection();
        public BaseVC CurrentViewModel
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
                var baseVcType = typeof(BaseVC);
                if (baseVcType.IsAssignableFrom(t))
                {
                    var vc = this.nav.ServicesDI.Resolve(t);
                    this.nav.CurrentViewModel = vc as BaseVC;
                    this.nav.CurrentViewModelType = t;
                }
            }
        }
    }
}
