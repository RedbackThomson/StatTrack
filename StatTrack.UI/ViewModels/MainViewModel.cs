using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Practices.Prism.Mvvm;
using StatTrack.Twitch;
using StatTrack.UI.Models;
using StatTrack.UI.Models.Notifiers;
using StatTrack.UI.Models.TrackerOptions;

namespace StatTrack.UI.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public StatTrackImpl Tracker { get; set; }

        public ICommand StartTracker { get; set; }
        public ICommand StopTracker { get; set; }

        public ChattersNotifications ChatterNotifications { get; private set; }

        private StatTrackSettings _settings;
        public StatTrackSettings Settings
        {
            get { return _settings; }
            set { SetProperty(ref _settings, value); }
        }

        private TrackerOptions _options;
        public TrackerOptions TrackerOptions
        {
            get { return _options; }
            set { SetProperty(ref _options, value); }
        }

        public MainViewModel()
        {
            Tracker = new StatTrackImpl(30000, "tsm_dyrus");
            _settings = Tracker.GetSettings();

            _options = new TrackerOptions()
            {
                SelectedChatterOptions = new List<ChatterOptions>{ChatterOptions.Viewers}
            };

            StartTracker = new TrackerCommand(Tracker, 
                Tracker.StartMonitoring, 
                track => !track.IsMonitoring());

            StopTracker = new TrackerCommand(Tracker,
                Tracker.StopMonitoring,
                track => track.IsMonitoring());

            Tracker.Trackers.Add(TwitchApiEndpoint.Chatters);

            ChatterNotifications = new ChattersNotifications();
            Tracker.Handlers.ChattersChanged += ChatterNotifications.HandlersOnChattersChanged;
        }
    }

    public class BoolToStringConverter : BoolToValueConverter<string> {}
    public class BoolToValueConverter<T> : IValueConverter
    {
        public T FalseValue { get; set; }
        public T TrueValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return FalseValue;
            return (bool)value ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value != null && value.Equals(TrueValue);
        }
    }

    public class RangeObservableCollection<T> : ObservableCollection<T>
    {
        public void AddRange(IEnumerable<T> items)
        {
            CheckReentrancy();
            foreach(var item in items)
                Items.Add(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
