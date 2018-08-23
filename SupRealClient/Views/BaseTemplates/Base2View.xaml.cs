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
using System.Windows.Data;
using System.Collections.ObjectModel;

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
        }

        #region Под удаление
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
        #endregion

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
                         btnEdit.IsEnabled &&
                         btnEdit.Visibility == Visibility.Visible &&
                         btnOk.Visibility != Visibility.Visible)
                    {
                    btnEdit.Command.Execute(null);
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

        private void UserControl_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow.Visibility == System.Windows.Visibility.Visible)
            {
                tbxSearch.Focus();
                tbxSearch.Text = string.Empty;
                SortItemsSource();
            }
        }       

        private void baseTab_LoadingRow(object sender, DataGridRowEventArgs e)
        {            
            DataGridRow oRow = e.Row;
            var item = oRow.Item;

            if (item is Organization)
            {
                RowColorOrganizationTable(oRow);
            }                        
        }
        
        private void baseTab_Loaded(object sender, System.Windows.RoutedEventArgs e)
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
            if (baseTab.ItemsSource is System.Collections.ObjectModel.ObservableCollection<Organization>)
            {
                SortDataGrid(baseTab, 1, ListSortDirection.Ascending);
            }
            else if (baseTab.Columns.Count > 0)
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

        void RowColorOrganizationTable(DataGridRow oRow)
        {
            Organization oOrg = oRow.Item as Organization;
            if (oOrg.FullName == string.Empty)
            {
                if (IsOrgHasSynonim(oOrg))
                    oRow.Background = Brushes.LightGreen;
                else
                    oRow.Background = Brushes.GreenYellow;
            }
            else
                oRow.Background = Brushes.White;

            if (OrganizationsHelper.GetBasicParametr(oOrg.Id, true))
            {
                oRow.FontWeight = FontWeights.Bold;
            }
            if (OrganizationsHelper.IsChildOrg(oOrg))
            {
                oRow.FontStyle = FontStyles.Oblique;
                oRow.Foreground = Brushes.Blue;
            }
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
