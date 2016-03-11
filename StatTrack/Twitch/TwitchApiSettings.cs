using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTrack.Twitch
{
    public class TwitchApiSettings
    {
        private string _channel;
        public string Channel
        {
            get { return _channel.ToLower(); }
            set { _channel = value; }
        }
    }
}
