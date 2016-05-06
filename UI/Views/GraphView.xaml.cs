using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using StatTrack.UI.Annotations;
using StatTrack.UI.Models;

namespace StatTrack.UI.Views
{
    /// <summary>
    /// Interaction logic for GraphView.xaml
    /// </summary>
    public partial class GraphView : UserControl, INotifyPropertyChanged
    {
        public string GraphName { get; set; }
        public bool CanClose { get; set; }

        private ResultsDataSet _dataSet;
        public ResultsDataSet DataSet 
        {
            get
            {
                return _dataSet;
            }
            set
            {
                _dataSet = value;
                UpdateSeries();
            }
        }

        public GraphView(string name)
        {
            CanClose = false;
            GraphName = name;

            InitializeComponent();
        }

        private void UpdateSeries()
        {
            //Bind the .Count for lists before the data arrives
            if (string.IsNullOrEmpty(DataSet.Properties.Attribute.GraphableProperty))
                Series.YBindingPath = "Data";
            else
                Series.YBindingPath = "Data." + DataSet.Properties.Attribute.GraphableProperty;

            Series.ItemsSource = DataSet.DataSet;
            Series.Label = DataSet.Properties.Attribute.Name;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
