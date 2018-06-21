using SupRealClient.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SupRealClient.Views
{
    /// <summary>
    /// Interaction logic for BidsView.xaml
    /// </summary>
    public partial class BidsView : IWindow
    {
        public BidsView()
        {
            InitializeComponent();

            
        }

        public bool CanMinimize { get; private set; } = false;

        public bool IsRealClose { get; set; } = true;
        public object WindowResult { get; set; }

        public string WindowName { get; private set; } = "BidsView";

        public IWindow ParentWindow { get; set; }

        public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                this.Hide();
            }
        }

        public void Unsuscribe()
        {
            this.Closing -= this.Window_Closing;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

        private void BidsView_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }

        private void MetroWindow_Initialized(object sender, EventArgs e)
        {
            this.DataContext = new ViewModels.BidsViewModel() { BidsModel = new Models.BidsModel() }; // Контекст данных.
            this.Height = (this.DataContext as ViewModels.BidsViewModel).WinSet.Height;
            this.Width = (this.DataContext as ViewModels.BidsViewModel).WinSet.Width;
            this.Left = (this.DataContext as ViewModels.BidsViewModel).WinSet.Left;
            this.Top = (this.DataContext as ViewModels.BidsViewModel).WinSet.Top;
        }
    }    
}
