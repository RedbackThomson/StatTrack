﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Prism.Modularity;
using Prism.Regions;
using StatTrack.UI.Models;
using StatTrack.UI.Services;
using StatTrack.UI.Views;

namespace StatTrack.UI.ViewModels
{
    public class OptionsViewModel : IOptionsViewModel, IModule
    {
        private readonly IRegionManager _regionManager;

        public ObservableCollection<Option> RootOptions { get; set; }
        public ISettings Settings { get; set; }

        public OptionsViewModel(IRegionManager regionManager, ISettings settings)
        {
            _regionManager = regionManager;
            Settings = settings;

            RootOptions = new ObservableCollection<Option>();

            var chattersOption = new Option { Caption = "Chatters" };
            chattersOption.Options.Add(new Option { Caption = "Viewers" });
            chattersOption.Options.Add(new Option { Caption = "Moderators" });
            chattersOption.Options.Add(new Option { Caption = "Staff" });
            chattersOption.Options.Add(new Option { Caption = "Global Moderators" });

            var followersOption = new Option { Caption = "Followers" };

            var mainOption = new Option { Caption = "Trackers" };
            mainOption.Options.Add(chattersOption);
            mainOption.Options.Add(followersOption);

            RootOptions.Add(mainOption);
        }

        private void OptionsChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            
        }

        public void Initialize()
        {
            _regionManager.Regions["MainRegion"].Add(new OptionsView());
        }
    }
}