using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatTrack.Twitch;

namespace StatTrack
{
    public class StatTrackSettings
    {
        /// <summary>
        /// The number of seconds between updates
        /// </summary>
        public int UpdatePeriod { get; set; }

        /// <summary>
        /// The Twitch API settings
        /// </summary>
        public TwitchApiSettings TwitchSettings { get; set; }

        private static readonly StatTrackSettings instance = new StatTrackSettings();
        public static StatTrackSettings Instance { get { return instance; } }

        private StatTrackSettings()
        {
        }
    }
}
