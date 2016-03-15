using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using StatTrack.UI.Annotations;

namespace StatTrack.UI.Models
{
    public class ResultsDataSet : INotifyPropertyChanged
    {
        public GraphableProperty Properties { get; set; }
        public ObservableCollection<ResultsData> DataSet { get; set; }

        public ResultsDataSet(GraphableProperty prop)
        {
            Properties = prop;
            DataSet = new ObservableCollection<ResultsData>();
        }

        public void NewData(ResultsData data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                DataSet.Add(data);
                OnPropertyChanged("DataSet");
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ResultsData
    {
        public object Data { get; set; }
        public DateTime Timestamp { get; set; }

        public ResultsData(DateTime timestamp, object data)
        {
            Timestamp = timestamp;
            Data = data;
        }
    }
}
