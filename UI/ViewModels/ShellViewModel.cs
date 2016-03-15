using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Modularity;
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

    public class ShellViewModel : IShellViewModel, IModule
    {
        public ITrackerService Tracker { get { return _tracker; } }

        public ICommand StartMonitoringCommand { get; set; }
        public ICommand StopMonitoringCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        private readonly ITrackerService _tracker;

        public ShellViewModel(ITrackerService tracker)
        {
            _tracker = tracker;

            StartMonitoringCommand = new DelegateCommand(OnStartMonitoring, CanStartMonitoring);
            StopMonitoringCommand = new DelegateCommand(OnStopMonitoring, CanStopMonitoring);
            CloseCommand = new DelegateCommand(Application.Current.MainWindow.Close, () => true);
        }

        private void OnStartMonitoring()
        {
            Tracker.Start();
        }

        private bool CanStartMonitoring()
        {
            return Tracker.CanStart;
        }

        private void OnStopMonitoring()
        {
            Tracker.Stop();
        }

        private bool CanStopMonitoring()
        {
            return Tracker.CanStop;
        }

        public void Initialize()
        {
        }
    }
}
