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
    public partial class BidsView
    {
        public BidsView()
        {
            InitializeComponent();            
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
