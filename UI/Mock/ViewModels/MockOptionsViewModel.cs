using System.Collections.ObjectModel;
using StatTrack.UI.Mock.Services;
using StatTrack.UI.Models;
using StatTrack.UI.Services;
using StatTrack.UI.ViewModels;

namespace StatTrack.UI.Mock.ViewModels
{
    public class MockOptionsViewModel : IOptionsViewModel
    {
        public ObservableCollection<TrackOption> RootOptions { get; set; }
        public ISettings Settings { get; set; }

        public void CreateOptions()
        {
            var chattersOption = new TrackOption { Caption = "Chatters" };
            chattersOption.Options.Add(new TrackOption { Caption = "Viewers" });
            chattersOption.Options.Add(new TrackOption { Caption = "Moderators" });
            chattersOption.Options.Add(new TrackOption { Caption = "Staff" });
            chattersOption.Options.Add(new TrackOption { Caption = "Global Moderators" });

            var followersOption = new TrackOption { Caption = "Followers" };

            var mainOption = new TrackOption { Caption = "Trackers" };
            mainOption.Options.Add(chattersOption);
            mainOption.Options.Add(followersOption);

            RootOptions.Add(mainOption);
        }

        public MockOptionsViewModel()
        {
            Settings = new MockSettings();

            RootOptions = new ObservableCollection<TrackOption>();

            CreateOptions();
        }
    }
}
