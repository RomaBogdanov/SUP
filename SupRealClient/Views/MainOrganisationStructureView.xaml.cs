using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            ((MainOrganizationViewModel)DataContext).OnClose += 
                Handling_OnClose;

            AfterInitialize();
            tbSearch.Focus();
        }

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ((MainOrganizationViewModel)DataContext).SelectedObject = e.NewValue;
        }

        private void TreeViewSelectedItemChanged(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = sender as TreeViewItem;
            if (item != null)
            {
                item.BringIntoView();
                e.Handled = true;
            }
        }

        private void tbSearch_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ((MainOrganizationViewModel)DataContext).Next();
            }
        }

        private void MetroWindow_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Window oWindow = Window.GetWindow(this);
            if (oWindow.Visibility == System.Windows.Visibility.Hidden)
            {
                tbSearch.Text = string.Empty;
                tbSearch.Focus();              
            }
        }
    }
}
