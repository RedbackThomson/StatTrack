using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using StatTrack.UI.Annotations;

namespace StatTrack.UI.Models
{
    public class TrackOption : INotifyPropertyChanged
    {
        public GraphableProperty Property { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public event OptionChangedEventHandler OptionChanged;
        public delegate void OptionChangedEventHandler(GraphableProperty property, bool newOption);

        private string _caption = string.Empty;
        private bool? _isChecked = false;
        private ObservableCollection<TrackOption> _options;

        internal List<TrackOption> CheckedItems;
        internal List<TrackOption> UnCheckedItems;
        internal TrackOption Parent;

        public TrackOption()
        {
            Property = null;

            CheckedItems = new List<TrackOption>();
            UnCheckedItems = new List<TrackOption>();

            Options = new ObservableCollection<TrackOption>();            
            Options.CollectionChanged += Options_CollectionChanged;
        }

        private void Options_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != NotifyCollectionChangedAction.Add) return;
            foreach (TrackOption model in e.NewItems)
            {
                model.Parent = this;
            }
        }

        public string Caption
        {
            get 
            { 
                return _caption; 
            }
            set
            {
                _caption = value;
                OnPropertyChanged();             
            }
        }

        public bool? IsChecked
        {
            get 
            { 
                return _isChecked; 
            }
            set
            {
                _isChecked = value;
                OnPropertyChanged();
                OnCheckedChanged(this);
                OnOptionChanged();
            }
        }

        public ObservableCollection<TrackOption> Options
        {
            get 
            {
                return _options; 
            }
            set 
            { 
                _options = value;
                OnPropertyChanged();
            }
        }

        private static void OnCheckedChanged(object sender)
        {
            var instance = sender as TrackOption;

            if (instance.IsChecked.HasValue && instance.IsChecked.Value)
            {
                if (instance.Parent != null && instance.Parent.UnCheckedItems.Contains(instance))
                {
                    instance.Parent.UnCheckedItems.Remove(instance);
                }
                if (instance.Parent != null && !instance.Parent.CheckedItems.Contains(instance))
                {
                    instance.Parent.CheckedItems.Add(instance);
                    if (instance.Parent.CheckedItems.Count == instance.Parent.Options.Count)
                    {
                        instance.Parent.IsChecked = true;
                    }
                }

                foreach (var model in instance.Options)
                {
                    model.IsChecked = true;
                }
            }

            if (instance.IsChecked.HasValue && !instance.IsChecked.Value)
            {
                if (instance.Parent != null && instance.Parent.CheckedItems.Contains(instance))
                {
                    instance.Parent.CheckedItems.Remove(instance);
                }
                if (instance.Parent != null && !instance.Parent.UnCheckedItems.Contains(instance))
                {
                    instance.Parent.UnCheckedItems.Add(instance);
                    if (instance.Parent != null && instance.Parent.UnCheckedItems.Count == instance.Parent.Options.Count)
                    {
                        instance.Parent.IsChecked = false;
                    }
                }

                foreach (var model in instance.Options)
                {
                    model.IsChecked = false;
                }
            }

            if (instance.Parent != null && instance.Parent.CheckedItems.Count != 0 &&
                instance.Parent.UnCheckedItems.Count != 0)
            {
                instance.Parent.IsChecked = null;
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnOptionChanged()
        {
            if (OptionChanged != null && _isChecked.HasValue)
                OptionChanged(Property, _isChecked.Value);
        }
    }
}
