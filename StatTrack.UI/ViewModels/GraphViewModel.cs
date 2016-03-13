using System;
using System.Collections.ObjectModel;
using System.Runtime.Remoting.Messaging;
using Prism.Modularity;
using Prism.Regions;
using StatTrack.UI.Models;
using StatTrack.UI.Services;
using StatTrack.UI.Views;

namespace StatTrack.UI.ViewModels
{
    public class GraphViewModel : IGraphViewModel, IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly ISettings _settings;

        public ObservableCollection<GraphData> Viewers { get; set; }
        public ObservableCollection<GraphData> Moderators { get; set; }

        public GraphViewModel(IRegionManager regionManager, ISettings settings)
        {
            _regionManager = regionManager;
            _settings = settings;
        }

        public void Initialize()
        {
            _regionManager.Regions["MainRegion"].Add(new GraphView());
        }
    }
}
