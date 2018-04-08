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
using SupRealClient.Models.OrganizationStructure;
using SupRealClient.ViewModels;

namespace SupRealClient.Views
{
    /// <summary>
    /// Interaction logic for AddUnitView.xaml
    /// </summary>
    public partial class AddUnitView
    {
        public AddUnitView()
        {
            InitializeComponent();

            //DataContext = new UnitViewModel();
        }

        private void UIElement_OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MessageBox.Show("Когда будет понятно логика, нужно подвесить исполнение команды на событие!");
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            (DataContext as UnitViewModel).OkCommand.Execute(null);

            Close();
        }
    }
}
