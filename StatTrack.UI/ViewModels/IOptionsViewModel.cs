using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Modularity;
using StatTrack.UI.Models;
using StatTrack.UI.Services;

namespace StatTrack.UI.ViewModels
{
    interface IOptionsViewModel
    {
        ObservableCollection<Option> RootOptions { get; set; }
        ISettings Settings { get; set; }
    }
}
