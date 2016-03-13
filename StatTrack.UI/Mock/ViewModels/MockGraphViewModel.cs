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
using StatTrack.UI.ViewModels;
using StatTrack.UI.Views;

namespace StatTrack.UI.Mock.ViewModels
{
    public class MockGraphViewModel : IGraphViewModel
    {
        public ObservableCollection<GraphData> Viewers { get; set; }
        public ObservableCollection<GraphData> Moderators { get; set; }

        public MockGraphViewModel()
        {
            Viewers = new ObservableCollection<GraphData>();

            Viewers.Add(new GraphData { Time = DateTime.Now.Subtract(new TimeSpan(0, 5, 0)), Data = 5 });
            Viewers.Add(new GraphData { Time = DateTime.Now.Subtract(new TimeSpan(0, 4, 0)), Data = 10 });
            Viewers.Add(new GraphData { Time = DateTime.Now.Subtract(new TimeSpan(0, 3, 0)), Data = 15 });
            Viewers.Add(new GraphData { Time = DateTime.Now.Subtract(new TimeSpan(0, 2, 0)), Data = 100 });
            Viewers.Add(new GraphData { Time = DateTime.Now.Subtract(new TimeSpan(0, 1, 0)), Data = 150 });

            Moderators = new ObservableCollection<GraphData>();

            Moderators.Add(new GraphData { Time = DateTime.Now.Subtract(new TimeSpan(0, 5, 0)), Data = 5 });
            Moderators.Add(new GraphData { Time = DateTime.Now.Subtract(new TimeSpan(0, 4, 0)), Data = 6 });
            Moderators.Add(new GraphData { Time = DateTime.Now.Subtract(new TimeSpan(0, 3, 0)), Data = 10 });
            Moderators.Add(new GraphData { Time = DateTime.Now.Subtract(new TimeSpan(0, 2, 0)), Data = 20 });
            Moderators.Add(new GraphData { Time = DateTime.Now.Subtract(new TimeSpan(0, 1, 0)), Data = 25 });
        }
    }
}
