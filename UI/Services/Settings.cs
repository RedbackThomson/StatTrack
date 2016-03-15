using System;
using System.ComponentModel;
using Syncfusion.Windows.PropertyGrid;

namespace StatTrack.UI.Services
{
    [TypeConverter(typeof(ExpandableObjects))]
    public interface ISettings
    {
        object this[string propertyName] { get; set; }
        void Save();

        string ChannelUsername { get; set; }
        
        int UpdatePeriod { get; set; }
    }

    public class Settings : ISettings
    {
        [Category("Twitch")]
        [DisplayNameAttribute("Twitch Channel")]
        [DescriptionAttribute("The username of the Twitch channel.")]
        public string ChannelUsername
        {
            get { return (string)this["channel_username"]; }
            set { this["channel_username"] = value; }
        }

        [Category("StatTrack")]
        [DisplayNameAttribute("Update Period")]
        [DescriptionAttribute("The amount of time between updates (in ms).")]
        public int UpdatePeriod
        {
            get { return (int)this["update_period"]; }
            set { this["update_period"] = value; }
        }

        public object this[string propertyName]
        {
            get { return Properties.Settings.Default[propertyName]; }
            set { Properties.Settings.Default[propertyName] = value; Save(); }
        }

        public void Save()
        {
            Properties.Settings.Default.Save();
        }
    }
}
