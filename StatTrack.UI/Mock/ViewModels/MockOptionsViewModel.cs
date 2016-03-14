using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Regions;
using StatTrack.UI.Mock.Services;
using StatTrack.UI.Models;
using StatTrack.UI.Services;
using StatTrack.UI.ViewModels;
using StatTrack.UI.Views;

namespace StatTrack.UI.Mock.ViewModels
{
    public class MockOptionsViewModel : IOptionsViewModel
    {
        public ObservableCollection<Option> RootOptions { get; set; }
        public ISettings Settings { get; set; }

        public void CreateOptions()
        {
            var chattersOption = new Option { Caption = "Chatters" };
            chattersOption.Options.Add(new Option { Caption = "Viewers" });
            chattersOption.Options.Add(new Option { Caption = "Moderators" });
            chattersOption.Options.Add(new Option { Caption = "Staff" });
            chattersOption.Options.Add(new Option { Caption = "Global Moderators" });

            var followersOption = new Option { Caption = "Followers" };

            var mainOption = new Option { Caption = "Trackers" };
            mainOption.Options.Add(chattersOption);
            mainOption.Options.Add(followersOption);

            RootOptions.Add(mainOption);
        }

        public MockOptionsViewModel()
        {
            Settings = new MockSettings();

            RootOptions = new ObservableCollection<Option>();

            CreateOptions();
        }
    }
}
