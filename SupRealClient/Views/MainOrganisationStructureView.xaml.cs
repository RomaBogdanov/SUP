﻿using System.Collections.ObjectModel;
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
    }
}
