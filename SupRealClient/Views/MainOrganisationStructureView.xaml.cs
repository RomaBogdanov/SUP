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
using SupRealClient.ViewModels;

namespace SupRealClient.Views
{
    /// <summary>
    /// Interaction logic for MainOrganisationStructureView.xaml
    /// </summary>
    public partial class MainOrganisationStructureView
    {
        public MainOrganisationStructureView()
        {
            InitializeComponent();
            
        }

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ((MainOrganizationViewModel) DataContext).SelectedObject = e.NewValue;
        }
    }
}
