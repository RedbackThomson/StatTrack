using System.Collections.Generic;
using StatTrack.UI.Services;

namespace StatTrack.UI.Mock.Services
{
    class MockSettings : ISettings
    {
        private readonly Dictionary<string, object> _settings; 

        public MockSettings()
        {
            _settings = new Dictionary<string, object>
            {
                {"channel_username", "riotgames"},
                {"update_period", 5000}
            };
        }

        public object this[string propertyName]
        {
            get { return _settings[propertyName]; }
            set { _settings.Add(propertyName, value); }
        }

        public void Save()
        {
            
        }
    }
}
