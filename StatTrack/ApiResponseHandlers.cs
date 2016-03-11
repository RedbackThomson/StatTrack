using System;
using System.Collections.Generic;
using StatTrack.Twitch;
using StatTrack.Twitch.Structures;

namespace StatTrack
{
    public class ApiResponseHandlers
    {
        public event DataChangedEventHandler ChattersChanged;
        public event DataChangedEventHandler FollowersChanged;

        public Dictionary<TwitchApiEndpoint, Action<DataChangedEventArgs>> Events { get; private set; }

        public ApiResponseHandlers()
        {
            Events = new Dictionary<TwitchApiEndpoint, Action<DataChangedEventArgs>>
            {
                {TwitchApiEndpoint.Chatters, e => CallEvent(ChattersChanged, e)},
                {TwitchApiEndpoint.Followers, e => CallEvent(FollowersChanged, e)}
            };
        }

        public delegate void DataChangedEventHandler (DataChangedEventArgs e);

        public class DataChangedEventArgs : EventArgs
        {
            public ITwitchStructure Data { get; set; }
            public DateTime Timestamp { get; set; }
        }

        private void CallEvent(DataChangedEventHandler handler, DataChangedEventArgs args)
        {
            if (handler != null)
                handler(args);
        }
    }
}
