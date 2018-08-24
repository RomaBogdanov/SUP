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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SupRealClient.ViewModels;
using SupRealClient.Models;
using SupRealClient.Common.Interfaces;
using System.Windows.Controls.Primitives;
using System.ComponentModel;

namespace SupRealClient.Views
{    
    /// <summary>
    /// Логика взаимодействия для Base3View.xaml
    /// </summary>
    public partial class Base3View : UserControl
    {
        Base1ViewModel viewModel = new Base3ViewModel();

        DataGridColumnHeader headerCliked = null;

        public Base3View()
        {
            DataContext = viewModel;
        }

        public void Init()
        {
            InitializeComponent();
        }

        public void SetViewModel(Base3ModelAbstr model)
        {
            ((Base3ViewModel)DataContext).SetModel(model);
            InitializeComponent();
            tbxSearch.Focus();
        }

        public DataGrid BaseTab
        {
            get { return baseTab; }           
        }

        public void SetDefaultColumn()
        {
            if (baseTab.Columns.Count > 0)
            {
                baseTab.CurrentColumn = baseTab.Columns[0];
            }
        }

        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.None)
            {
                if (e.Key == Key.Up && btnUp.IsEnabled)
                {
                    btnUp.Command.Execute(null);
                    e.Handled = true;
                }
                else if (e.Key == Key.Down && btnDown.IsEnabled)
                {
                    btnDown.Command.Execute(null);
                    e.Handled = true;
                }
                else if (e.Key == Key.Enter &&
                         btnUpdate.IsEnabled &&
                         btnUpdate.Visibility == Visibility.Visible &&
                         btnOk.Visibility != Visibility.Visible)
                {
                    btnUpdate.Command.Execute(null);
                    e.Handled = true;
                }
                else if (e.Key == Key.Insert)
                {
                    ((ISuperBaseViewModel)DataContext).Add.Execute(null);
                    e.Handled = true;
                }
                else if (e.Key == Key.Home && btnHome.IsEnabled)
                {
                    btnHome.Command.Execute(null);
                    e.Handled = true;
                }
                else if (e.Key == Key.End && btnEnd.IsEnabled)
                {
                    btnEnd.Command.Execute(null);
                    e.Handled = true;
                }
                else if (e.Key == Key.Space)
                {
                    if (btnOk.Visibility == Visibility.Visible && btnOk.IsEnabled)
                    {
                        btnOk.Command?.Execute(null);
                        e.Handled = true;
                    }
                }
            }                
        }

        private void baseTab_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (btnOk.Visibility == Visibility.Visible &&
                Keyboard.Modifiers == ModifierKeys.None &&
                e.Key == Key.Enter &&
                baseTab.CurrentItem != null)
            {
                btnOk.Command.Execute(null);
                e.Handled = true;
            }
        }

        private void BaseTab_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Down && e.Key != Key.Up)
            {
                SelectSearchBox();
            }
        }

        public void SelectSearchBox()
        {
            tbxSearch.Focus();
        }              

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow.Visibility == System.Windows.Visibility.Visible)
            {
                tbxSearch.Focus();
                tbxSearch.Text = string.Empty;
                SortItemsSource();
            }
        }
      
        private void baseTab_Loaded(object sender, RoutedEventArgs e)
        {
            SortItemsSource();
        }

        private void dgColumnHeader_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            headerCliked = sender as DataGridColumnHeader;
        }

        private void baseTab_Sorted(object sender, RoutedEventArgs e)
        {
            if (headerCliked != null)
            {
                baseTab.CurrentColumn = headerCliked.Column;
                headerCliked = null;
            }
        }

        void SortItemsSource()
        {
            if (baseTab.Columns.Count > 0)
            {
                SortDataGrid(baseTab, 0, ListSortDirection.Ascending);
            }

            if (baseTab.Items.Count > 0)
            {
                baseTab.SelectedItem = baseTab.Items[0];
            }
            // Refresh items to display sort
            baseTab.Items?.Refresh();
        }

        void SortDataGrid(DataGrid dataGrid, int columnIndex = 0, ListSortDirection sortDirection = ListSortDirection.Ascending)
        {
            var column = dataGrid.Columns[columnIndex];

            // Clear current sort descriptions
            dataGrid.Items.SortDescriptions.Clear();

            // Add the new sort description
            dataGrid.Items.SortDescriptions.Add(new SortDescription(column.SortMemberPath, sortDirection));

            // Apply sort
            foreach (var col in dataGrid.Columns)
            {
                col.SortDirection = null;
            }
            column.SortDirection = sortDirection;

            dataGrid.CurrentColumn = dataGrid.Columns[columnIndex];
        }

        public void ScrollIntoViewCurrentItem()
        {
            if (baseTab.Items.Count > 0)
            {
                var row = baseTab.CurrentItem;
                if (row == null)
                    return;

                baseTab.ScrollIntoView(row);
                baseTab.UpdateLayout();
                baseTab.ScrollIntoView(row);
            }
        }
    }
}
