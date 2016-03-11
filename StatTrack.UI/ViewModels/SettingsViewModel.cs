using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using Microsoft.Practices.Prism.Mvvm;
using StatTrack.UI.Models.TrackerOptions;

namespace StatTrack.UI.ViewModels
{
    public class SettingsViewModel : BindableBase
    {
        public StatTrackSettings TrackSettings { get; set; }
        public TrackerOptions TrackerOptions { get; set; }
        public ICommand DoCloseWindow { get; set; }

        public ICommand SelectItemsCommand { get; set; }
        public IList<SelectableItem<ChatterOptions>> 
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
            SelectableChatterOptions = new SelectableItems<ChatterOptions>
                (GetEnumValues<ChatterOptions>(), trackerOptions.SelectedChatterOptions).GetSelectableItems();
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
            var selected = (SelectableItem<T>[]) parameter;

            _output.Clear();
            foreach(var item in selected)
                _output.Add(item.Item);
        }

        public event EventHandler CanExecuteChanged;
    }

    public class SelectableItems<T>
    {
        private readonly ICollection<T> _items;
        private readonly ICollection<T> _preselected; 

        public IList<SelectableItem<T>> GetSelectableItems()
        {
            return _items.Select(item => new SelectableItem<T>
            {
                IsSelected = _preselected.Contains(item), 
                Item = item
            }).ToList();
        }

        public SelectableItems(ICollection<T> items) : this(items, new List<T>()) 
        {
        }

        public SelectableItems(ICollection<T> items, ICollection<T> selected)
        {
            _items = items;
            _preselected = selected;
        }
    }

    public class SelectableItem<T>
    {
        public bool IsSelected { get; set; }
        public T Item { get; set; }
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
