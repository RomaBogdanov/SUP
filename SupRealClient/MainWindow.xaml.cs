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

        private double _height;
        private double _width;

        //private double _bidsHeight;
        //private double _bidsWidth;
        //private double _bidsLeft;
        //private double _bidsTop;

        //private double _visitorsHeight;
        //private double _visitorsWidth;
        //private double _visitorsLeft;
        //private double _visitorsTop;

        private ChildWindowSettings _childSettings = new ChildWindowSettings();

        public MainWindow()
        {
            InitializeComponent();

            /*Loaded += (s, e) =>
            {
                _height = Height;
                _width = Width;

                CalculateChildWindow();
            };*/
        }

        private void CalculateChildWindow()
        {
            _childSettings.BidsWidth = (_width - 130) / 2;
            _childSettings.VisitorsWidth = (_width - 130) / 2;

            _childSettings.BidsHeight = _height - 40 - 30;
            _childSettings.VisitorsHeight = _height - 40 - 30;

            _childSettings.BidsLeft = 130;
            _childSettings.VisitorsLeft = 130 + _childSettings.BidsWidth;

            _childSettings.BidsTop = 40;
            _childSettings.VisitorsTop = 40;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            var window = sender as Window;
            if (window.WindowState == WindowState.Minimized || window.WindowState == WindowState.Normal)
            {
                ViewManager.Instance.SetChildrenState(sender as Window, true);
                this.Height = 120;
            }
        }
    }
}
