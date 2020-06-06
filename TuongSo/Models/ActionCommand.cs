using System;
using System.Windows.Input;

namespace TuongSo.Models
{
    public class ActionCommand : ICommand
    {
        public Action Action { get; }

        public event EventHandler CanExecuteChanged;
        public ActionCommand(Action action)
        {
            Action = action;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.Action?.Invoke();
        }
    }
}
