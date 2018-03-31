using SupRealClient.ViewModels;
using System.Windows;
using System.Windows.Threading;

namespace SupRealClient
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            var errorWindow = new ErrorWindow(new ErrorViewModel(e.Exception, "Проверьте соединение с сервером"));
            errorWindow.ShowDialog();
        }
    }
}
