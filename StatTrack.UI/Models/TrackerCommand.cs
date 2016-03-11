using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StatTrack.UI.Models
{
    public class TrackerCommand : ICommand
    {
        private readonly IStatTrack _tracker;
        private readonly Action _command;
        private readonly Predicate<IStatTrack> _canExecute;

        public TrackerCommand(IStatTrack tracker, Action command, Predicate<IStatTrack> canExecute)
        {
            _tracker = tracker;
            _command = command;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _command != null && _canExecute.Invoke(_tracker);
        }

        public void Execute(object parameter)
        {
            _command.Invoke();
        }

        public event EventHandler CanExecuteChanged;
    }
}
