using System.Collections.ObjectModel;
using Prism.Modularity;
using Prism.Regions;
using StatTrack.UI.Models;
using StatTrack.UI.Views;

namespace StatTrack.UI.ViewModels
{
    public interface INotificationsViewModel
    {
        ObservableCollection<GraphableProperty> Options { get; }
    }

    public class NotificationsViewModel : INotificationsViewModel, IModule
    {
        public ObservableCollection<GraphableProperty> Options { get { return _options.Current; } } 

        private readonly IRegionManager _regionManager;
        private readonly IOptions _options;

        public NotificationsViewModel(IRegionManager regionManager, IOptions options)
        {
            _regionManager = regionManager;
            _options = options;
        }

        public void Initialize()
        {
            _regionManager.Regions["MainRegion"].Add(new NotificationsView());
        }
    }
}
