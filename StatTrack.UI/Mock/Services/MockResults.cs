using System;
using System.Collections.Generic;
using StatTrack.Lib.Twitch;
using StatTrack.Lib.Twitch.Structures;
using StatTrack.UI.Models;
using StatTrack.UI.Services;

namespace StatTrack.UI.Mock.Services
{
    public class MockResults : IResults
    {
        private readonly Dictionary<TwitchApiEndpoint, ResultsDataSet> _data;

        public MockResults()
        {
            _data = new Dictionary<TwitchApiEndpoint, ResultsDataSet>();

            var dataSet = new ResultsDataSet(TwitchApiEndpoint.Chatters);

            #region MockData
            var firstWrapper = new ChattersWrapper
            {
                Chatters = new Chatters
                {
                    Viewers = new HashSet<string>
                    {
                        "Redback",
                        "Redback2",
                        "Redback3"
                    }
                }
            };

            var secondWrapper = new ChattersWrapper
            {
                Chatters = new Chatters
                {
                    Viewers = new HashSet<string>
                    {
                        "Redback",
                        "Redback2",
                        "Redback3",
                        "Redback4",
                        "Redback5"
                    }
                }
            };

            var thirdWrapper = new ChattersWrapper
            {
                Chatters = new Chatters
                {
                    Viewers = new HashSet<string>
                    {
                        "Redback",
                        "Redback2",
                        "Redback3",
                        "Redback4",
                        "Redback5",
                        "Redback6",
                        "Redback7"
                    }
                }
            };
#endregion
            
            dataSet.DataSet.Add(new ResultsData { Data = firstWrapper, Timestamp = DateTime.Now.Subtract(new TimeSpan(0, 0, 6, 0)) });
            dataSet.DataSet.Add(new ResultsData { Data = secondWrapper, Timestamp = DateTime.Now.Subtract(new TimeSpan(0, 0, 3, 0)) });
            dataSet.DataSet.Add(new ResultsData { Data = thirdWrapper, Timestamp = DateTime.Now });

            _data.Add(TwitchApiEndpoint.Chatters, dataSet);
        }

        public void HandleNewData(TwitchApiEndpoint endpoint, DataChangedEventArgs e)
        {
            
        }

        public ResultsDataSet GetCurrentDataSet(TwitchApiEndpoint endpoint)
        {
            if (!_data.ContainsKey(endpoint))
                _data.Add(endpoint, new ResultsDataSet(endpoint));

            return _data[endpoint];
        }
    }
}
