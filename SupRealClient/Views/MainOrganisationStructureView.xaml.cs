﻿using System.Windows;
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
                MainOrganisationStructureView_OnClose;

            AfterInitialize();
        }

        private void MainOrganisationStructureView_OnClose()
        {
            this.Close();
        }

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ((MainOrganizationViewModel) DataContext).SelectedObject = e.NewValue;
        }
    }
}
