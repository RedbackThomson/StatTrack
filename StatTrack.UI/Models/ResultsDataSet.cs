using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using StatTrack.Lib.Twitch;
using StatTrack.Lib.Twitch.Structures;

namespace StatTrack.UI.Models
{
    public class ResultsDataSet
    {
        private string AxisTitle { get; set; }
        public ObservableCollection<ResultsData> DataSet { get; set; }

        public ResultsDataSet(TwitchApiEndpoint endpoint)
        {
            AxisTitle = endpoint.Name;
            DataSet = new ObservableCollection<ResultsData>();
        }
    }

    public class ResultsData
    {
        public ITwitchStructure Data { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
