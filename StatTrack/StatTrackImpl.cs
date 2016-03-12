using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using StatTrack.Twitch;
using StatTrack.Twitch.Structures;

namespace StatTrack
{
    public class StatTrackImpl : IStatTrack
    {
        private readonly StatTrackSettings _settings;
        private readonly TwitchApi _api;
        private readonly ApiResponseHandlers _handlers;
        private readonly HashSet<TwitchApiEndpoint> _trackers;
        private readonly CancellationTokenSource _cancelSource;

        //Used for determining if the task is running
        public bool Monitoring { get; private set; }

        public ApiResponseHandlers Handlers { get { return _handlers; } }
        public HashSet<TwitchApiEndpoint> Trackers {get { return _trackers; }} 

        public StatTrackImpl(int updatePeriod, string channel)
        {
            _settings = StatTrackSettings.Instance;
            _settings.UpdatePeriod = updatePeriod;
            _settings.TwitchSettings = new TwitchApiSettings
            {
                Channel = channel
            };

            _api = new TwitchApi(_settings.TwitchSettings);
            _handlers = new ApiResponseHandlers();
            _trackers = new HashSet<TwitchApiEndpoint>();
            _cancelSource = new CancellationTokenSource();
        }

        public void StartMonitoring()
        {
            var ct = _cancelSource.Token;
            Task.Factory.StartNew(() => Monitor(ct), ct);

            Monitoring = true;
            NotifyPropertyChanged("Monitoring");
        }

        private void Monitor(CancellationToken ct)
        {
            while (true)
            {
                try
                {
                    if (ct.IsCancellationRequested)
                        ct.ThrowIfCancellationRequested();

                    GetEndpoints();
                    Thread.Sleep(_settings.UpdatePeriod);
                }
                catch (OperationCanceledException ex)
                {
                    //Exit gracefully
                }
            }
        }

        private void GetEndpoints()
        {
            foreach(var endpoint in Trackers)
                GetEndpoint(endpoint);
        }

        private async void GetEndpoint(TwitchApiEndpoint endpoint)
        {
            ITwitchStructure res = null;
            var getter = _api.Getters[endpoint];
            do
            {
                try
                {
                    res = await getter.Invoke();
                }
                catch (HttpRequestException ex)
                {
                    Console.Error.WriteLine(ex.Data);
                    //TODO: Log there was a 502
                }
            } while (res == null);

            var eventArgs = new ApiResponseHandlers.DataChangedEventArgs
            {
                Data = res,
                Timestamp = DateTime.Now
            };
            var handler = _handlers.Events[endpoint];
            handler.DynamicInvoke(eventArgs);
        }

        public void StopMonitoring()
        {
            if(!_cancelSource.IsCancellationRequested)
                _cancelSource.Cancel();
            
            Monitoring = false;
            NotifyPropertyChanged("Monitoring");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public StatTrackSettings GetSettings()
        {
            return _settings;
        }

        public bool IsMonitoring()
        {
            return Monitoring;
        }
    }
}
