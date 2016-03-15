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
    public interface IGraphViewModel
    {

    }

    public class GraphViewModel : IGraphViewModel, IModule
    {
        public GraphViewModel()
        {
        }

        public void Initialize()
        {
        }
    }
}
