using System.Windows.Input;
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
