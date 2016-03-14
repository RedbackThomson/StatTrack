using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using StatTrack.UI.Models;
using StatTrack.UI.Services;

namespace StatTrack.UI.ViewModels
{
    public interface IShellViewModel
    {
        ITrackerService Tracker { get; }

        ICommand StartMonitoringCommand { get; set; }
        ICommand StopMonitoringCommand { get; set; }

        ICommand CloseCommand { get; set; }
    }
}
