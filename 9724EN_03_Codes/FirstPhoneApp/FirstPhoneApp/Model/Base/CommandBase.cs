using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FirstPhoneApp.Model.Base
{
    public class RelayCommand : ICommand
    {
        Action<object> theAction;
        Func<bool> canExecuteAction;

        public RelayCommand(Action<object> theaction)
        {
            theAction = theaction;
        }
        public RelayCommand(Action<object> theaction, Func<bool> canexecuteAction)
        {
            this.theAction = theaction;
            this.canExecuteAction = canexecuteAction;
        }
        public bool CanExecute(object parameter)
        {
            if (this.canExecuteAction != null)
                return this.canExecuteAction();

            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if(this.CanExecute(parameter))
            {
                if (this.theAction != null)
                    this.theAction(parameter);
            }
        }
    }
}
