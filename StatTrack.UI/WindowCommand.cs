using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StatTrack.UI
{
    public class WindowCommand : ICommand
    {
        private readonly Action _command;

        public WindowCommand(Action command)
        {
            _command = command;
        }

        public bool CanExecute(object parameter)
        {
            return _command != null;
        }

        public void Execute(object parameter)
        {
            //Assume no parameters
            _command.Invoke();
        }

        public event EventHandler CanExecuteChanged;
    }
}
