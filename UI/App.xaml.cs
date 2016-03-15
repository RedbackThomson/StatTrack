using System.Windows;

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
