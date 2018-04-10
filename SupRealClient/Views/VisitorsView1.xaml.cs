using System;
using System.Collections.Generic;
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
    /// Interaction logic for VisitorsView.xaml
    /// </summary>
    public partial class VisitorsView1
    {
        public VisitorsView1()
        {
            InitializeComponent();
        }
        
        private void ButtonBase_OnClickMain(object sender, RoutedEventArgs e)
        {
            var window = new VisitorsMainTabView();
            window.ShowDialog();
        }

        private void ButtonBase_OnClickPass(object sender, RoutedEventArgs e)
        {
            var window = new VisitorsPassTabView();
            window.ShowDialog();
        }

        private void ButtonBase_OnClickBids(object sender, RoutedEventArgs e)
        {
            var window = new VisitorsBidsTabView();
            window.ShowDialog();
        }

        private void ButtonBase_OnClickEmployee(object sender, RoutedEventArgs e)
        {
            var window = new VisitorsEmployeeTabView();
            window.ShowDialog();
        }
    }
}
