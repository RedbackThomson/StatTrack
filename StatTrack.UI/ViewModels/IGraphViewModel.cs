using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Modularity;
using Prism.Regions;
using StatTrack.UI.Models;
using StatTrack.UI.Services;

namespace StatTrack.UI.ViewModels
{
    public interface IGraphViewModel
    {
        ObservableCollection<GraphData> Viewers { get; set; }
        ObservableCollection<GraphData> Moderators { get; set; }
    }
}
