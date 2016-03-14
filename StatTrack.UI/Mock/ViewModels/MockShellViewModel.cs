using System.Windows.Input;
using Prism.Modularity;
using StatTrack.UI.Services;
using StatTrack.UI.ViewModels;

namespace StatTrack.UI.Mock.ViewModels
{
    public class MockShellViewModel : IShellViewModel, IModule
    {
        public ITrackerService Tracker { get; private set; }

        public ICommand StartMonitoringCommand { get; set; }
        public ICommand StopMonitoringCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public void Initialize()
        {
            
        }
    }
}
