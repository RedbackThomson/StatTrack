using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using StatTrack.UI.Models;
using Syncfusion.Windows.Shared;

namespace StatTrack.UI.ViewModels
{
    public class ViewModel : NotificationObject
    {
        public ObservableCollection<Workspace> Workspaces { get; set; }

        public OptionsViewModel OptionsViewModel { get; set; }
        public GraphViewModel GraphViewModel { get; set; }


        public ViewModel()
        {
            Workspaces = new ObservableCollection<Workspace>();

            OptionsViewModel = new OptionsViewModel { ViewModel = this};
            GraphViewModel = new GraphViewModel { ViewModel = this, State = DockState.Document };

            Workspaces.Add(GraphViewModel);
            Workspaces.Add(OptionsViewModel);
        }

        private void Exit()
        {
            Application.Current.MainWindow.Close();
        }

        private DelegateCommand<object> _closeCommand;
        public DelegateCommand<object> CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new DelegateCommand<object>(param => Exit())); }
        }
    }
}
