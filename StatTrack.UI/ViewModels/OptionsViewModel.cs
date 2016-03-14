using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Prism.Modularity;
using Prism.Regions;
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
            //TODO: Automate getting new options
            var types = new List<Type> { typeof(Chatters), typeof(FollowsWrapper) };
            foreach (var type in types)
            {
                foreach (var property in type.GetProperties())
                {
                    //Get all Graphable properties of the Chatters class
                    foreach (var graphable in property.GetCustomAttributes(false)
                        .OfType<Graphable>())
                    {
                        var newOption = new Option { Caption = graphable.Name, Property = property };
                        newOption.OptionChanged += OnOptionChanged;
                        RootOptions.Add(newOption);
                    }
                }
            }
        }

        private void OnOptionChanged(PropertyInfo member, bool newOption)
        {
            if (newOption)
                _graphManager.NewGraph(member);
            else
                _graphManager.DeleteGraph(member);
        }

        public void Initialize()
        {
            _regionManager.Regions["MainRegion"].Add(new OptionsView());
        }
    }
}
