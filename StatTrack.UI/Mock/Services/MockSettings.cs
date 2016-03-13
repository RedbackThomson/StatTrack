using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                {"update_frequency", 5000}
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
