using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using StatTrack.Twitch.Structures;
using StatTrack.UI.Annotations;

namespace StatTrack.UI.Models.Notifiers
{
    public class ChattersNotifications : INotifyPropertyChanged
    {
        private readonly INotifier _notifier;
        private DateTime _lastUpdate;


        public ChattersNotifications()
        {
            //IF WIN8 ONLY
            _notifier = new WindowsNotifier();
        }

        private ChattersWrapper _current;
        public ChattersWrapper Current
        {
            get { return _current; }
            set
            {
                _current = value;
                OnPropertyChanged();
            }
        }

        public void HandlersOnChattersChanged(ApiResponseHandlers.DataChangedEventArgs dataChangedEventArgs)
        {
            var newChatters = (ChattersWrapper) dataChangedEventArgs.Data;
            if (Current != null)
            {
                var timeSpan = dataChangedEventArgs.Timestamp.Subtract(_lastUpdate);
                NewElements("Viewer", timeSpan, Current.Chatters.Viewers, newChatters.Chatters.Viewers);
            }

            _lastUpdate = dataChangedEventArgs.Timestamp;
            Current = newChatters;
        }

        private void NewElements(string elemName, TimeSpan timeSpan, IEnumerable<string> oldElems, IEnumerable<string> newElems)
        {
            var complement = new HashSet<string>(newElems.Except(oldElems));
            
            switch (complement.Count)
            {
                case 0:
                    return;
                case 1:
                    var enumerator = complement.GetEnumerator();
                    enumerator.MoveNext();
                    _notifier.NotifyNew("New {0} (in the last {1}) : {2}", 
                        elemName, LastTimeInterval(timeSpan), enumerator.Current);
                    break;
                default:
                    _notifier.NotifyNew("New {0}s (in the last {1}): {2}", elemName, 
                        LastTimeInterval(timeSpan), complement.Count);
                    break;
            }
        }

        private static string LastTimeInterval(TimeSpan span)
        {
            if (span.TotalMinutes < 1)
                return string.Format("{0} seconds", (int)span.TotalSeconds);
            if (span.TotalHours < 1)
                return string.Format("{0} minutes", (int)span.TotalMinutes);
            return string.Format("{0} hours", (int)span.TotalHours);
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
