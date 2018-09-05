using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SupRealClient.Models.OrganizationStructure;
using SupRealClient.ViewModels;

namespace SupRealClient.Views
{
    /// <summary>
    /// Interaction logic for MainOrganisationStructureView.xaml
    /// </summary>
    public partial class SelectedMainOrganisationStructureView
	{
        public SelectedMainOrganisationStructureView(Visibility okVisibility)
        {
            InitializeComponent();
            ((MainOrganizationViewModel)DataContext).OnClose += 
                Handling_OnClose;

            AfterInitialize();

            ((MainOrganizationViewModel)DataContext).OkVisibility = okVisibility;

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

        private void MetroWindow_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Window oWindow = Window.GetWindow(this);
            if (oWindow.Visibility == System.Windows.Visibility.Visible)
            {                
                tbSearch.Text = string.Empty;
                tbSearch.Focus();

                MainOrganizationViewModel vm = DataContext as MainOrganizationViewModel;
                if (vm != null && vm.Organizations.Count > 0)
                {
                    CollapseOrgs(vm.Organizations[0].Items);
                    vm.Organizations[0].IsExpanded = true;
                    vm.Organizations[0].IsSelected = true;
                }                
            }
        }

        private void MetroWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.None && 
                e.Key != Key.Insert && 
                e.Key != Key.Escape &&
                e.Key != Key.Delete)
            {
                if (e.Key == Key.Up || 
                    e.Key == Key.Down || 
                    e.Key == Key.Left ||
                    e.Key == Key.Right)
                {
                    treeView.Focus();
                }
                else if (e.Key != Key.Enter &&
                         e.Key != Key.Tab)
                {
                    tbSearch.Focus();
                }
            }            
        }  
        
        void CollapseOrgs(ObservableCollection<Organization> orgs)
        {
            foreach (var org in orgs)
            {
                org.IsExpanded = false;
                CollapseDeps(org.Items);
            }
        }

        void CollapseDeps(ObservableCollection<Department> deps)
        {
            foreach (var dep in deps)
            {
                dep.IsExpanded = false;
                CollapseDeps(dep.Items);
            }
        }              

        private void treeViewItemTextBlock_Drop(object sender, DragEventArgs e)
        {
            object dropData = (sender as TextBlock)?.DataContext;
            Department dragData = e.Data.GetData("SupRealClient.Models.OrganizationStructure.Department") as Department;
            if (dragData != null && 
                !dragData.Equals(dropData) &&
                (dropData is Department || dropData is Organization))
            {
                ((MainOrganizationViewModel)DataContext).DragAndDropDepartment(dropData, dragData);
            }

            e.Handled = true;
        }

        private void treeViewItemTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock item = sender as TextBlock;
            if (item != null && item.DataContext is Department)
            {
                DragDrop.DoDragDrop(item, item.DataContext, DragDropEffects.Move);
            }            
        }

		public void SetSelectedOrganizationId(object orgID)
		{
			if(orgID!=null && orgID is int?)
				((MainOrganizationViewModel)DataContext).SetSelectedOrganizationID(orgID as int?);
			else
				((MainOrganizationViewModel)DataContext).SetSelectedOrganizationID(null);
		}
    }
}
