using System.Collections.Generic;
using System.ComponentModel;
using StatTrack.UI.Services;

namespace StatTrack.UI.Mock.Services
{
    public class MockSettings : ISettings
    {
        public MockSettings()
        {

        }

        public object this[string propertyName] 
        {
            get { return null; }
            set { }
        }

        public void Save()
        {
            
        }

        [Category("Twitch")]
        [DisplayName("Twitch Channel")]
        [Description("The username of the Twitch channel.")]
        public string ChannelUsername { get { return "riotgames"; } set {} }

        [Category("StatTrack")]
        [DisplayNameAttribute("Update Period")]
        [DescriptionAttribute("The amount of time between updates (in ms).")]
        public int UpdatePeriod { get { return 5000; } set {} }
    }
}
