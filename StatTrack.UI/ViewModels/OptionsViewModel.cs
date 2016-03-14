using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Prism.Modularity;
using Prism.Regions;
using StatTrack.Lib.Twitch;
using StatTrack.Lib.Twitch.Structures;
using StatTrack.UI.Models;
using StatTrack.UI.Services;
using StatTrack.UI.Views;

namespace StatTrack.UI.ViewModels
{
    public class OptionsViewModel : IOptionsViewModel, IModule
    {
        private readonly IGraphManager _graphManager;
        private readonly IRegionManager _regionManager;

        public ObservableCollection<Option> RootOptions { get; set; }
        public ISettings Settings { get; set; }

        public OptionsViewModel(IGraphManager graphManager, IRegionManager regionManager, ISettings settings)
        {
            _graphManager = graphManager;
            _regionManager = regionManager;
            Settings = settings;

            RootOptions = new ObservableCollection<Option>();
            CreateOptions();
        }

        public void CreateOptions()
        {
            foreach (var type in TwitchApiEndpoint.GetAllEndpoints().Select(x => x.ReturnStructure))
                CreateOptions(null, type);
        }

        private void CreateOptions(Option parent, Type structureType)
        {
            foreach (var property in structureType.GetProperties())
            {
                //Get all Graphables underneath the structure
                foreach (var hasGraphable in property.GetCustomAttributes(false).OfType<HasGraphables>())
                {
                    var newOption = new Option {Caption = hasGraphable.Name};
                    if(parent == null)
                        RootOptions.Add(newOption);
                    else
                        parent.Options.Add(newOption);

                    CreateOptions(newOption, property.PropertyType);
                }

                //Get all Graphable properties of the Chatters class
                foreach (var graphable in property.GetCustomAttributes(false)
                    .OfType<Graphable>())
                {
                    var graphableProperty = new GraphableProperty { Attribute = graphable, Property = property };
                    var newOption = new Option { Caption = graphable.Name, Property = graphableProperty };
                    newOption.OptionChanged += OnOptionChanged;
                    //Add it under the root
                    if(parent == null)
                        RootOptions.Add(newOption);
                    else
                        parent.Options.Add(newOption);
                }
            }
        }

        private void OnOptionChanged(GraphableProperty property, bool newOption)
        {
            if (newOption)
                _graphManager.NewGraph(property);
            else
                _graphManager.DeleteGraph(property);
        }

        public void Initialize()
        {
            _regionManager.Regions["MainRegion"].Add(new OptionsView());
        }
    }
}
