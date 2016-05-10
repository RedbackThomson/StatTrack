using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Prism.Modularity;
using Prism.Regions;
using StatTrack.Lib.Twitch;
using StatTrack.Lib.Twitch.Structures;
using StatTrack.UI.Models;
using StatTrack.UI.Services;
using StatTrack.UI.Views;

namespace StatTrack.UI.ViewModels
{
    interface IOptionsViewModel
    {
        ObservableCollection<TrackOption> RootOptions { get; set; }
        ISettings Settings { get; set; }
        void CreateOptions();
    }

    public class OptionsViewModel : IOptionsViewModel, IModule
    {
        private readonly ITrackerService _trackerService;
        private readonly IGraphManager _graphManager;
        private readonly IRegionManager _regionManager;
		private readonly IOptions _options;

        public ObservableCollection<TrackOption> RootOptions { get; set; }
        public ISettings Settings { get; set; }

        private readonly Dictionary<GraphableProperty, TwitchApiEndpoint> _propertyEndpoints; 

        public OptionsViewModel(ITrackerService trackerService, IGraphManager graphManager, IRegionManager regionManager, 
            ISettings settings, IOptions options)
        {
            _trackerService = trackerService;
            _graphManager = graphManager;
            _regionManager = regionManager;
			_options = options;
            Settings = settings;

            RootOptions = new ObservableCollection<TrackOption>();
            _propertyEndpoints = new Dictionary<GraphableProperty, TwitchApiEndpoint>();
            CreateOptions();
        }

        public void CreateOptions()
        {
            foreach (var endpoint in TwitchApiEndpoint.GetAllEndpoints())
                CreateOptions(endpoint, null, endpoint.ReturnStructure);
        }

        private void CreateOptions(TwitchApiEndpoint endpoint, TrackOption parent, Type structureType)
        {
            foreach (var property in structureType.GetProperties())
            {
                //Get all Graphables underneath the structure
                foreach (var hasGraphable in property.GetCustomAttributes(false).OfType<HasGraphables>())
                {
                    var newOption = new TrackOption {Caption = hasGraphable.Name};
                    if(parent == null)
                        RootOptions.Add(newOption);
                    else
                        parent.Options.Add(newOption);

                    CreateOptions(endpoint, newOption, property.PropertyType);
                }

                //Get all Graphable properties of the Chatters class
                foreach (var graphable in property.GetCustomAttributes(false)
                    .OfType<Graphable>())
                {
                    var graphableProperty = new GraphableProperty { Attribute = graphable, Property = property };
                    var newOption = new TrackOption { Caption = graphable.Name, Property = graphableProperty };
                    newOption.OptionChanged += OnOptionChanged;
                    //Add it under the root
                    if(parent == null)
                        RootOptions.Add(newOption);
                    else
                        parent.Options.Add(newOption);
                    _propertyEndpoints.Add(graphableProperty, endpoint);
                }
            }
        }

        private void OnOptionChanged(GraphableProperty property, bool newOption)
        {
            if (newOption)
			{
                _graphManager.NewGraph(property);
			    Application.Current.Dispatcher.Invoke(() => _options.Current.Add(property));
                _trackerService.AddTracker(_propertyEndpoints[property]);
			}
            else
			{
                _graphManager.DeleteGraph(property);
                Application.Current.Dispatcher.Invoke(() => _options.Current.Remove(property));
                _trackerService.RemoveTracker(_propertyEndpoints[property]);
			}
        }

        public void Initialize()
        {
            _regionManager.Regions["MainRegion"].Add(new OptionsView());
        }
    }
}
