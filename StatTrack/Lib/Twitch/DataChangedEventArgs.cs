using System;
using StatTrack.Lib.Twitch.Structures;

namespace StatTrack.Lib.Twitch
{
    public class DataChangedEventArgs : EventArgs
    {
        public ITwitchStructure Data { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
