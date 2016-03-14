using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using StatTrack.Lib.Twitch;
using StatTrack.Lib.Twitch.Structures;
using StatTrack.UI.Models;

namespace StatTrack.UI.Services
{
    public interface IResults
    {
        void HandleNewData(TwitchApiEndpoint endpoint, DataChangedEventArgs e);
        ResultsDataSet GetCurrentDataSet(GraphableProperty endpointProperty);
    }

    public class Results : IResults
    {
        private readonly Dictionary<PropertyInfo, ResultsDataSet> _data;

        public Results()
        {
            _data = new Dictionary<PropertyInfo, ResultsDataSet>();
        }

        public void HandleNewData(TwitchApiEndpoint endpoint, DataChangedEventArgs e)
        {
            BreakUpNewData(e.Data, endpoint.ReturnStructure, e.Timestamp);
        }

        private void BreakUpNewData(object structure, Type structureType, DateTime timestamp)
        {
            foreach (var prop in structureType.GetProperties())
            {
                var attr = prop.GetCustomAttributes(false).OfType<Graphable>().FirstOrDefault();
                if (attr != null)
                {
                    NewDataSet(prop, attr, timestamp, prop.GetValue(structure));
                    continue;
                }
                //Recursively search properties for graphable
                if (prop.PropertyType.IsClass)
                {
                    BreakUpNewData(prop.GetValue(structure), prop.PropertyType, timestamp);
                }
            }
        }

        private void NewDataSet(PropertyInfo prop, Graphable attr, DateTime timestamp, object data)
        {
            if (data == null) return;

            var newSetData = new ResultsData(timestamp, data);
            ResultsDataSet dataSet = GetCurrentDataSet(new GraphableProperty{Attribute = attr, Property = prop});
            dataSet.NewData(newSetData);
        }

        public ResultsDataSet GetCurrentDataSet(GraphableProperty property)
        {
            if (!_data.ContainsKey(property.Property))
                _data.Add(property.Property, new ResultsDataSet(property));

            return _data[property.Property];
        }
    }
}
