using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatTrack.UI.Mock.Services;
using StatTrack.UI.Models;
using StatTrack.UI.ViewModels;

namespace StatTrack.UI.Mock.ViewModels
{
    class MockNotificationsViewModel  : INotificationsViewModel
    {
        public ObservableCollection<GraphableProperty> Options { get { return _options.Current; } }

        private readonly IOptions _options;

        public MockNotificationsViewModel()
        {
            _options = new MockOptions();
        }
    }
}
