using System;
using System.Collections.Generic;
using StatTrack.Lib.Twitch;
using StatTrack.Lib.Twitch.Structures;

namespace StatTrack.Lib
{
    public class ApiResponseHandlers
    {
        public event DataChangedEventHandler DataChanged;

        public delegate void DataChangedEventHandler (TwitchApiEndpoint endpoint, DataChangedEventArgs e);

        public virtual void OnDataChanged(TwitchApiEndpoint endpoint, DataChangedEventArgs e)
        {
            var handler = DataChanged;
            if (handler != null) handler(endpoint, e);
        }
    }
}
