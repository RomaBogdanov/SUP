using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;
using SupRealClient.Common.Interfaces;
using SupRealClient.EnumerationClasses;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для Organizations1View.xaml
    /// </summary>
    public partial class Base2View : UserControl
    {
        DataGridColumnHeader headerCliked = null;

        public Base2View()
        {
            InitializeComponent();

            baseTab.SelectionChanged -= baseTab_SelectionChanged;            
            //DataContext = viewModel;
        }
        
        public void SetViewModel(Base1ModelAbstr model)
        {
            ((Base1ViewModel)DataContext).SetModel(model);
            InitializeComponent();
            tbxSearch.Focus();
        }
               

        public void SetDefaultColumn()
        {
            if (baseTab.Columns.Count > 0)
            {
                baseTab.CurrentColumn = baseTab.Columns[0];
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

        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.None)
            {
                if (e.Key == Key.Up)
                {
                    baseTab.SelectionChanged += baseTab_SelectionChanged;
                    btnUp.Command.Execute(null);
                    e.Handled = true;
                }
                else if (e.Key == Key.Down)
                {
                    baseTab.SelectionChanged += baseTab_SelectionChanged;
                    btnDown.Command.Execute(null);
                    e.Handled = true;
                }
                else if (e.Key == Key.Enter)
                {
                    btnEdit.Command.Execute(null);
                }
                else if (e.Key == Key.Insert)
                {
                    ((ISuperBaseViewModel)DataContext).Add.Execute(null);
                }
                else if (e.Key == Key.Home)
                {
                    ((ISuperBaseViewModel)DataContext).Begin.Execute(null);
                }
                else if (e.Key == Key.End)
                {
                    ((ISuperBaseViewModel)DataContext).End.Execute(null);
                }
            }            
            else if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (e.Key == Key.G)
                {
                    baseTab.SelectionChanged += baseTab_SelectionChanged;
                }                    
            }
        }

        private void UserControl_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow.Visibility == System.Windows.Visibility.Hidden)
            {
                tbxSearch.Text = string.Empty;
                baseTab.SelectedItems?.Clear();
                baseTab.SelectionChanged += baseTab_SelectionChanged;

                if (baseTab.ItemsSource is System.Collections.ObjectModel.ObservableCollection<Organization>)
                    SortDataGrid(baseTab, 1, ListSortDirection.Ascending);
                else if (baseTab.Columns.Count > 0)
                    SortDataGrid(baseTab, 0, ListSortDirection.Ascending);
            }
        }

        private void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            baseTab.SelectionChanged += baseTab_SelectionChanged;
        }

        private void baseTab_LoadingRow(object sender, DataGridRowEventArgs e)
        {            
            DataGridRow oRow = e.Row;
            var item = oRow.Item;

            if (item is Organization)
                RowColorOrganizationTable(oRow);
        }

        void RowColorOrganizationTable(DataGridRow oRow)
        {
            Organization oOrg = oRow.Item as Organization;            
            if (oOrg?.FullName == string.Empty)
            {
                if (IsOrgHasSynonim(oOrg))
                    oRow.Background = Brushes.LightGreen;
                else
                    oRow.Background = Brushes.GreenYellow;                
            }
            else
                oRow.Background = Brushes.White;            
        }

        bool IsOrgHasSynonim(Organization org)
        {
            foreach (var item in baseTab.ItemsSource)
            {
                if ((item as Organization)?.FullName == org.Type + @" " + org.Name)
                    return true;
            }

            return false;
        }

        public void ScrollIntoViewNewItem()
        {
            if (baseTab.Items.Count > 0)
            {
                var row = baseTab.Items[baseTab.Items.Count - 1];
                if (row == null)
                    return;

                baseTab.ScrollIntoView(row);
            }           
        }

        private void baseTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            baseTabCurrentItemScrollIntoView();
            baseTab.SelectionChanged -= baseTab_SelectionChanged;           
        }

        void baseTabCurrentItemScrollIntoView()
        {
            if (baseTab.CurrentItem != null)
            {
                baseTab.ScrollIntoView(baseTab.CurrentItem);
                baseTab.UpdateLayout();
                baseTab.ScrollIntoView(baseTab.CurrentItem);
            }
        }

        private void baseTab_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (baseTab.ItemsSource is System.Collections.ObjectModel.ObservableCollection<Organization>)
                SortDataGrid(baseTab, 1, ListSortDirection.Ascending);
            else if (baseTab.Columns.Count > 0)
                SortDataGrid(baseTab, 0, ListSortDirection.Ascending);
        } 

        static void SortDataGrid(DataGrid dataGrid, int columnIndex = 0, ListSortDirection sortDirection = ListSortDirection.Ascending)
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

            if (dataGrid.Items.Count > 0)
                dataGrid.SelectedItem = dataGrid.Items[0];

            dataGrid.CurrentColumn = dataGrid.Columns[columnIndex];

            // Refresh items to display sort
            dataGrid.Items.Refresh();
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

                if (!string.IsNullOrEmpty(tbxSearch.Text))
                {
                    tbxSearch.Text = tbxSearch.Text;
                    baseTabCurrentItemScrollIntoView();
                }

                headerCliked = null;
            }
        }

        private void tbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            baseTabCurrentItemScrollIntoView();
        }
    }

    public class CustomDataGrid : DataGrid
    {
        // Create a custom routed event by first registering a RoutedEventID
        // This event uses the bubbling routing strategy
        public static readonly RoutedEvent SortedEvent = EventManager.RegisterRoutedEvent(
            "Sorted", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CustomDataGrid));

        // Provide CLR accessors for the event
        public event RoutedEventHandler Sorted
        {
            add { AddHandler(SortedEvent, value); }
            remove { RemoveHandler(SortedEvent, value); }
        }

        // This method raises the Sorted event
        void RaiseSortedEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(CustomDataGrid.SortedEvent);
            RaiseEvent(newEventArgs);
        }

        protected override void OnSorting(DataGridSortingEventArgs eventArgs)
        {
            base.OnSorting(eventArgs);
            RaiseSortedEvent();
        }
    }
}
