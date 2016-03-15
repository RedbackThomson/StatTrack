using Prism.Modularity;
using Prism.Regions;
using StatTrack.UI.Services;
using StatTrack.UI.Views;

namespace StatTrack.UI.ViewModels
{
    interface ISettingsViewModel
    {
        ISettings Settings { get; set; }
    }

    public class SettingsViewModel : ISettingsViewModel, IModule
    {
        public ISettings Settings { get; set; }
        private readonly IRegionManager _regionManager;

        public SettingsViewModel(IRegionManager regionManager, ISettings settings)
        {
            _regionManager = regionManager;
            Settings = settings;
        }

        public void Initialize()
        {
            _regionManager.Regions["MainRegion"].Add(new SettingsView());
        }
    }
}
