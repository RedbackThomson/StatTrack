using System.ComponentModel;
using System.Windows;
using Microsoft.Practices.ServiceLocation;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using StatTrack.UI.Mock.ViewModels;
using StatTrack.UI.Services;
using StatTrack.UI.ViewModels;
using Syncfusion.Windows.Tools.Controls;

namespace StatTrack.UI
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return ServiceLocator.Current.GetInstance<Shell>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow = (Window) Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            RegisterTypeIfMissing(typeof(ISettings), typeof(Settings), true);
            base.ConfigureContainer();
        }

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();

            if (mappings != null)
            {
                mappings.RegisterMapping(typeof(DockingManager), Container.TryResolve<DockingAdapter.DockingAdapter>());
            }

            return mappings;
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            ModuleCatalog catalog = new ModuleCatalog();
            catalog
                .AddModule(typeof(GraphViewModel))
                .AddModule(typeof(OptionsViewModel));
            return catalog;
        }
    }
}
