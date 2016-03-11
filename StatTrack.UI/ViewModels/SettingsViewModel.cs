using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using Microsoft.Practices.Prism.Mvvm;
using StatTrack.UI.Annotations;
using StatTrack.UI.Models.TrackerOptions;

namespace StatTrack.UI.ViewModels
{
    public class SettingsViewModel : BindableBase
    {
        public StatTrackSettings TrackSettings { get; set; }
        public TrackerOptions TrackerOptions { get; set; }
        public ICommand DoCloseWindow { get; set; }

        public ICommand SelectItemsCommand { get; set; }
        public ObservableCollection<SelectableItem<ChatterOptions>> 
            SelectableChatterOptions { get; set; }

        public SettingsViewModel() : this(new StatTrackSettings(), new TrackerOptions())
        {
        }

        public SettingsViewModel(StatTrackSettings trackSettings, TrackerOptions trackerOptions)
        {
            TrackSettings = trackSettings;
            TrackerOptions = trackerOptions;

            SelectItemsCommand = new SelectItemsCommand<ChatterOptions>(ref 
                trackerOptions.SelectedChatterOptions);

            SelectableChatterOptions = new ObservableCollection<SelectableItem<ChatterOptions>>();
            AddSelectedEnums(SelectableChatterOptions, TrackerOptions.SelectedChatterOptions);
            SelectableChatterOptions.CollectionChanged += SelectedChatterOptionsChanged;
        }

        private void SelectedChatterOptionsChanged(object sender, 
            NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            TrackerOptions.SelectedChatterOptions = (List<ChatterOptions>) 
                notifyCollectionChangedEventArgs.NewItems;
        }

        public static void AddSelectedEnums<T>(ObservableCollection<SelectableItem<T>> collection, 
            IList<T> selected) where T : struct
        {
            foreach(var option in GetEnumValues<T>())
                collection.Add(new SelectableItem<T>(option){IsSelected = selected.Contains(option)});
        }

        public static List<T> GetEnumValues<T>() where T : struct
        {
            var enumType = typeof(T);

            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");

            return Enum.GetValues(enumType).Cast<T>().ToList();
        }
    }

    public class SelectItemsCommand<T> : ICommand
    {
        private readonly IList<T> _output;
        public SelectItemsCommand(ref IList<T> output)
        {
            _output = output;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var list = (IList) parameter;
            var collections = list.Cast<SelectableItem<T>>();

            _output.Clear();
            foreach(var item in collections)
                _output.Add(item.Item);
        }

        public event EventHandler CanExecuteChanged;
    }

    public class SelectableItem<T> : INotifyPropertyChanged
    {
        private bool _selected;
        public bool IsSelected 
        { 
            get { return _selected; }
            set 
            { 
                if (value == _selected) return;
                _selected = value; 
                OnPropertyChanged("IsSelected");
            } 
        }
        public T Item { get; set; }

        public SelectableItem(T item)
        {
            Item = item;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class EnumDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var t = value.GetType();
            if (!t.IsEnum) return null;

            var oFieldInfo = value.GetType().GetField(value.ToString());
            if (oFieldInfo == null) return null;

            var oCustomAttributes = oFieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
            return oCustomAttributes.Length > 0 ? ((DescriptionAttribute)oCustomAttributes[0]).Description : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
