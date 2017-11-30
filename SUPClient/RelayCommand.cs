using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SUPClient
{
    class RelayCommand : ICommand
    {
        public Predicate<object> CanExecuteDelegate
        { get; set; }

        public Action<object> ExecuteDelegate
        { get; set; }

        public RelayCommand(Action<object> action)
        { this.ExecuteDelegate = action; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            this.CanExecuteDelegate?.Invoke(parameter);
            return true;
        }

        public void Execute(object parameter)
        {
            this.ExecuteDelegate?.Invoke(parameter);
        }
    }
}
