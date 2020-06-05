﻿using System;
using System.ComponentModel;
using TuongSo.Models;
using TuongSo.ViewModels;

namespace TuongSo.Navigators
{
    public class Navigator : INavigator, INotifyPropertyChanged
    {
        BaseVC _ViewControler = new PyCalVM();

        public event PropertyChangedEventHandler PropertyChanged;

        public BaseVC CurrentViewModel
        {
            get => _ViewControler; set
            {
                _ViewControler = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentViewModel)));
            }
        }

        public System.Windows.Input.ICommand UpdateViewModel { get; }
        public Type CurrentViewModelType { get; internal set; }

        public Navigator()
        {
            UpdateViewModel = new UpdateViewControllerCommand(this);
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
                    var vc = Activator.CreateInstance(t);
                    this.nav.CurrentViewModel = vc as BaseVC;
                    this.nav.CurrentViewModelType = t;
                }
            }
        }
    }
}
