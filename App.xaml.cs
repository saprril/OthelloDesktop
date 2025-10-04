using OthelloDesktop.Views;
using System.Configuration;
using System.Data;
using System.Windows;
using OthelloDesktop.ViewModels;


namespace OthelloDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel()
            };

            mainWindow.Show();
        }
    }



}
