using System;
using System.Windows;

namespace SupRealClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            var window = sender as Window;
            if (window.WindowState == WindowState.Minimized || window.WindowState == WindowState.Normal)
            {
                ViewManager.Instance.SetChildrenState(sender as Window, true);
            }
        }
    }
}
