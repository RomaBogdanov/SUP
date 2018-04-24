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
using SupRealClient.Models;

namespace SupRealClient.Views.Reports
{
    /// <summary>
    /// Interaction logic for MainReportMasterView.xaml
    /// </summary>
    public partial class MainReportMasterView
    {
        private List<Route> _routes = new List<Route>();

        public MainReportMasterView()
        {
            InitializeComponent();

            var route1 = new Route
            {
                Name = "VisitorsListReportMasterView",
                Points = new List<string>
                {
                    "Views/Reports/MainReportMasterPage.xaml",
                    "Views/Reports/VisitorsListReportMasterView.xaml"
                }
            };

            MainFrame.NavigationService.Navigate(new Uri("Views/Reports/MainReportMasterPage.xaml", UriKind.Relative));

            _routes.Add(route1);

            var route2 = new Route
            {
                Name = "IssuanceOfPassesPage",
                Points = new List<string>
                {
                    "Views/Reports/MainReportMasterPage.xaml",
                    "Views/Reports/IssuanceOfPassesPage.xaml"
                }
            };

            _routes.Add(route2);
        }

        private void ButtonBase_OnClick_Forward(object sender, RoutedEventArgs e)
        {
            if ((bool) ((MainReportMasterPage)MainFrame.NavigationService.Content).VisitorsListRadioButton.IsChecked)
            {
                MainFrame.NavigationService.Navigate(new Uri(_routes[0].Points[1], UriKind.Relative));
            }
            else if ((bool)((MainReportMasterPage)MainFrame.NavigationService.Content).IssuanceOfPassesRadioButton.IsChecked)
            {
                MainFrame.NavigationService.Navigate(new Uri(_routes[1].Points[1], UriKind.Relative));
            }

            //MainFrame.NavigationService.Navigate(new Uri("Views/Reports/VisitorsListReportMasterView.xaml", UriKind.Relative));
        }

        private void ButtonBase_OnClick_Back(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Views/Reports/MainReportMasterPage.xaml", UriKind.Relative));
        }
    }
}
