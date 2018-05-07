using System;
using System.Windows;
using System.Windows.Input;
using SupRealClient.Models;
using SupRealClient.ViewModels;

namespace SupRealClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private bool _keyOpenWindowPressed;
        private bool _keyExitPressed;

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
