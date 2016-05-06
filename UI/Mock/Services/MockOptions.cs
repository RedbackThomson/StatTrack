using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using StatTrack.Lib.Twitch.Structures;
using StatTrack.UI.Models;

namespace StatTrack.UI.Mock.Services
{
    public class MockOptions : IOptions
    {
        public ObservableCollection<GraphableProperty> Current { get; set; }

        public MockOptions()
        {
            Current = new ObservableCollection<GraphableProperty>();

            foreach (var property in typeof(Chatters).GetProperties())
            {
                //Get all Graphable properties of the Chatters class
                foreach (var graphable in property.GetCustomAttributes(false)
                    .OfType<Graphable>())
                {
                    Current.Add(new GraphableProperty{Attribute = graphable, Property = property});
                }
            }
        }
    }
}
