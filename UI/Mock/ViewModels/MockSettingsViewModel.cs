using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatTrack.UI.Mock.Services;
using StatTrack.UI.Services;
using StatTrack.UI.ViewModels;

namespace StatTrack.UI.Mock.ViewModels
{
    public class MockSettingsViewModel : ISettingsViewModel
    {
        public ISettings Settings { get; set; }

        public MockSettingsViewModel()
        {
            Settings = new MockSettings();
        }
    }
}
