using System.Collections.ObjectModel;
using StatTrack.UI.Models;
using StatTrack.UI.Services;

namespace StatTrack.UI.ViewModels
{
    interface IOptionsViewModel
    {
        ObservableCollection<Option> RootOptions { get; set; }
        ISettings Settings { get; set; }
        void CreateOptions();
    }
}
