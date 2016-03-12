using System;
using System.Collections.ObjectModel;
using StatTrack.UI.Models;

namespace StatTrack.UI.ViewModels
{
    public class GraphViewModel : PaneViewModel
    {
        public ObservableCollection<GraphData> Viewers { get; set; }
        public ObservableCollection<GraphData> Moderators { get; set; }
 
        public GraphViewModel()
        {
            Header = "Graph";

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
