using System.Collections.ObjectModel;
using StatTrack.UI.Models;

namespace StatTrack.UI.ViewModels
{
    public class OptionsViewModel : PaneViewModel
    {
        public ObservableCollection<Option> Options { get; set; }

        public OptionsViewModel()
        {
            Header = "Tracker Options";

            Options = new ObservableCollection<Option>();

            var chattersOption = new Option {Caption = "Chatters"};
            chattersOption.Options.Add(new Option { Caption = "Viewers" });
            chattersOption.Options.Add(new Option { Caption = "Moderators" });
            chattersOption.Options.Add(new Option { Caption = "Staff" });
            chattersOption.Options.Add(new Option { Caption = "Global Moderators" });

            var followersOption = new Option {Caption = "Followers"};

            var mainOption = new Option {Caption = "Trackers"};
            mainOption.Options.Add(chattersOption);
            mainOption.Options.Add(followersOption);

            Options.Add(mainOption);
        }
    }
}
