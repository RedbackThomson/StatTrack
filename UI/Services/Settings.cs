using System;
using System.Collections.Generic;
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
        private readonly Dictionary<string, object> _settingCache = new Dictionary<string, object>();

        [Category("Twitch")]
        [DisplayNameAttribute("Twitch Channel")]
        [DescriptionAttribute("The username of the Twitch channel.")]
        public string ChannelUsername
        {
            get { return (string) this["channel_username"]; }
            set { SaveProperty("ChannelUsername", "channel_username", value); }
        }

        [Category("StatTrack")]
        [DisplayNameAttribute("Update Period")]
        [DescriptionAttribute("The amount of time between updates (in ms).")]
        public int UpdatePeriod
        {
            get { return (int) this["update_period"]; }
            set { SaveProperty("UpdatePeriod", "update_period", value); }
        }

        private void SaveProperty(string propertyName, string settingProperty, object value)
        {
            this[settingProperty] = value;
        }

        public object this[string propertyName]
        {
            get
            {
                if (_settingCache.ContainsKey(propertyName))
                    return _settingCache[propertyName];
                _settingCache.Add(propertyName, Properties.Settings.Default[propertyName]);
                return _settingCache[propertyName];
            }
            set
            {
                _settingCache[propertyName] = value;
                Properties.Settings.Default[propertyName] = value;
                Save();
            }
        }

        public void Save()
        {
            Properties.Settings.Default.Save();
        }
    }
}
