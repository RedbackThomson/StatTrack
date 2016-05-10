using System.Collections.Generic;
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

        void AddTracker(TwitchApiEndpoint endpoint);
        void RemoveTracker(TwitchApiEndpoint endpoint);
        void Start();
        void Stop();
    }

    public class TrackerService : ITrackerService
    {
        /// <summary>
        /// Counts the number of times a tracker is added to the list, 
        /// so as to remove it only when nothing depends on it
        /// </summary>
        private readonly Dictionary<TwitchApiEndpoint, int> _trackerCount; 

        protected IStatTrack StatTrack { get; set; }

        public bool CanStart {get { return StatTrack.CanStartMonitoring() && !StatTrack.IsMonitoring(); }}
        public bool CanStop { get { return StatTrack.IsMonitoring(); } }
        public bool IsRunning {get { return StatTrack.IsMonitoring(); }}

        public TrackerService(ISettings settings, IResults results)
        {
            StatTrack = new Lib.StatTrack(settings.UpdatePeriod,
                settings.ChannelUsername);

            //Pass the new data to the results
            StatTrack.Handlers.DataChanged += results.HandleNewData;

            _trackerCount = new Dictionary<TwitchApiEndpoint, int>();
        }

        public void AddTracker(TwitchApiEndpoint endpoint)
        {
            StatTrack.Trackers.Add(endpoint);
            if (_trackerCount.ContainsKey(endpoint))
                _trackerCount[endpoint]++;
            else
                _trackerCount.Add(endpoint, 1);
        }

        public void RemoveTracker(TwitchApiEndpoint endpoint)
        {
            if (_trackerCount.ContainsKey(endpoint) && _trackerCount[endpoint] > 1)
            {
                _trackerCount[endpoint]--;
                return;
            }

            if(StatTrack.Trackers.Contains(endpoint))
                StatTrack.Trackers.Remove(endpoint);
            _trackerCount.Remove(endpoint);
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
