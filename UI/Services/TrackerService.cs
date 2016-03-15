using System.ComponentModel;
using System.Runtime.CompilerServices;
using StatTrack.Lib;
using StatTrack.Lib.Twitch;
using StatTrack.UI.Annotations;

namespace StatTrack.UI.Services
{
    public interface ITrackerService : INotifyPropertyChanged
    {
        bool CanStart { get; }
        bool CanStop { get; }
        bool IsRunning { get; }

        void Start();
        void Stop();
    }

    public class TrackerService : ITrackerService
    {
        private readonly IResults _results;

        protected IStatTrack StatTrack { get; set; }

        public bool CanStart {get { return StatTrack.CanStartMonitoring() && !StatTrack.IsMonitoring(); }}
        public bool CanStop { get { return StatTrack.IsMonitoring(); } }
        public bool IsRunning {get { return StatTrack.IsMonitoring(); }}

        public TrackerService(ISettings settings, IResults results)
        {
            StatTrack = new Lib.StatTrack(settings.UpdatePeriod,
                settings.ChannelUsername);

            _results = results;

            //TODO: Automate
            StatTrack.Trackers.Add(TwitchApiEndpoint.Chatters);

            //Pass the new data to the results
            StatTrack.Handlers.DataChanged += _results.HandleNewData;
        }

        public void Start()
        {
            StatTrack.StartMonitoring();

            OnPropertyChanged("CanStart");
            OnPropertyChanged("CanStop");
            OnPropertyChanged("IsRunning");
        }

        public void Stop()
        {
            StatTrack.StopMonitoring();

            OnPropertyChanged("CanStart");
            OnPropertyChanged("CanStop");
            OnPropertyChanged("IsRunning");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
