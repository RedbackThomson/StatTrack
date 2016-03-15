using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StatTrack.Lib.Twitch;
using StatTrack.Lib.Twitch.Structures;
using StatTrack.UI.Models;
using StatTrack.UI.Services;

namespace StatTrack.UI.Mock.Services
{
    public class MockResults : IResults
    {
        private readonly Dictionary<GraphableProperty, ResultsDataSet> _data;

        public MockResults()
        {
            _data = new Dictionary<GraphableProperty, ResultsDataSet>();

            var prop = typeof (Chatters).GetProperties().First(x => x.Name == "Viewers");
            var attr = prop.GetCustomAttributes().OfType<Graphable>().First();
            var graphProp = new GraphableProperty {Attribute = attr, Property = prop};
            var dataSet = new ResultsDataSet(graphProp);

            #region MockData

            var firstWrapper = new HashSet<string>
            {
                "Redback",
                "Redback2",
                "Redback3"
            };

            var secondWrapper = new HashSet<string>
            {
                "Redback",
                "Redback2",
                "Redback3",
                "Redback4",
                "Redback5"
            };

            var thirdWrapper = new HashSet<string>
            {
                "Redback",
                "Redback2",
                "Redback3",
                "Redback4",
                "Redback5",
                "Redback6",
                "Redback7"
            };
#endregion

            dataSet.DataSet.Add(new ResultsData(DateTime.Now.Subtract(new TimeSpan(0, 0, 6, 0)), firstWrapper));
            dataSet.DataSet.Add(new ResultsData(DateTime.Now.Subtract(new TimeSpan(0, 0, 3, 0)), secondWrapper));
            dataSet.DataSet.Add(new ResultsData(DateTime.Now, thirdWrapper));

            _data.Add(graphProp, dataSet);
        }

        public void HandleNewData(TwitchApiEndpoint endpoint, DataChangedEventArgs e)
        {
            
        }

        public ResultsDataSet GetCurrentDataSet(GraphableProperty prop)
        {
            if(!_data.ContainsKey(prop))
                _data.Add(prop, new ResultsDataSet(prop));
            return _data[prop];
        }
    }
}
