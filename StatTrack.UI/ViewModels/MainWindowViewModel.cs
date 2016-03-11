using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using StatTrack.UI.Views;

namespace StatTrack.UI.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowViewWindowFactory WindowFactory { get; set; }

        public MainWindowViewModel()
        {
            MainViewModel = new MainViewModel();
            SettingsViewModel = new SettingsViewModel(MainViewModel.Settings, 
                MainViewModel.TrackerOptions);

            //Set the specific settings for the settings view model
            WindowFactory = new MainWindowViewWindowFactory {Model = SettingsViewModel};
            SettingsViewModel.DoCloseWindow = WindowFactory.DoCloseSettingsWindow;
        }

        public MainViewModel MainViewModel { get; set; }
        public SettingsViewModel SettingsViewModel { get; set; }
    }

    public class MainWindowViewWindowFactory
    {
        public SettingsViewModel Model { get; set; }
        private SettingsView _view;

        public ICommand DoCreateSettingsWindow { get; set; }
        public ICommand DoCloseSettingsWindow { get; set; }

        public MainWindowViewWindowFactory()
        {
            DoCreateSettingsWindow = new WindowCommand(CreateSettingsWindow);
            DoCloseSettingsWindow = new WindowCommand(CloseSettingsWindow);
        }
        
        private void CreateSettingsWindow()
        {
            _view = new SettingsView
            {
                DataContext = Model
            };
            _view.Show();
        }

        private void CloseSettingsWindow()
        {
            _view.Close();
        }
    }
}
