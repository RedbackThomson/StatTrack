using System.Collections.Generic;
using StatTrack.Lib.Twitch;
using StatTrack.UI.Models;

namespace StatTrack.UI.Services
{
    public interface IResults
    {
        void HandleNewData(TwitchApiEndpoint endpoint, DataChangedEventArgs e);
        ResultsDataSet GetCurrentDataSet(TwitchApiEndpoint endpoint);
    }

    public class Results : IResults
    {
        private readonly Dictionary<TwitchApiEndpoint, ResultsDataSet> _data;

        public Results()
        {
            _data = new Dictionary<TwitchApiEndpoint, ResultsDataSet>();
        }

        public void HandleNewData(TwitchApiEndpoint endpoint, DataChangedEventArgs e)
        {
            var dataSet = GetCurrentDataSet(endpoint);

            var newData = new ResultsData()
            {
                Data = e.Data,
                Timestamp = e.Timestamp
            };

            dataSet.DataSet.Add(newData);
        }

        public ResultsDataSet GetCurrentDataSet(TwitchApiEndpoint endpoint)
        {
            if (!_data.ContainsKey(endpoint))
                _data.Add(endpoint, new ResultsDataSet(endpoint));

            return _data[endpoint];
        }
    }
}
