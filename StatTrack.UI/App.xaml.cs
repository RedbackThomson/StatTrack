using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Mvvm;
using StatTrack.UI.ViewModels;

namespace StatTrack.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var splash = new SplashScreen();
            splash.Show();
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
            splash.Close();
        }
    }
}
