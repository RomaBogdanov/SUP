using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using SupRealClient.Common.Interfaces;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для Base4View.xaml
    /// </summary>
    public partial class Base4View : UserControl
    {
        int memCountRows = 0;
        DataGridColumnHeader headerCliked = null;

        public bool modeEdit { get; set; }

        public Base4View()
        {
            InitializeComponent();            
        }

        public DataGrid BaseTab
        {
            get { return baseTab; }            
        }

        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {                
                btnUp.Command.Execute(null);
                e.Handled = true;
            }
            else if (e.Key == Key.Down)
            {
                btnDown.Command.Execute(null);
                e.Handled = true;
            }
            else if (e.Key == Key.Enter)
            {
                btnUpdate.Command.Execute(null);
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
            else if (e.Key == Key.Space)
            {
                if (btnOk.Visibility == Visibility.Visible && btnOk.IsEnabled)
                {
                    btnOk.Command?.Execute(null);
                    e.Handled = true;
                }
            }
        }

        private void baseTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            baseTabCurrentItemScrollIntoView();
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

        private void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (((Button)sender).Name == "butAdd")
                memCountRows = baseTab.Items.Count;           
        }

        private void baseTab_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (memCountRows + 1 == baseTab.Items.Count)
            {
                memCountRows = 0;
                baseTab.SelectedItems.Clear();
                baseTab.SelectedItem = baseTab.Items[baseTab.Items.Count - 1];
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

        private void UserControl_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow.Visibility == System.Windows.Visibility.Hidden)
            {
                tbxSearch.Text = string.Empty;
                baseTab.SelectedItems?.Clear();

                if (baseTab.Columns.Count > 0)
                    SortDataGrid(baseTab, 0, ListSortDirection.Ascending);
            }
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

        private void baseTab_Loaded(object sender, RoutedEventArgs e)
        {
            if (baseTab.Columns.Count > 0)
                SortDataGrid(baseTab, 0, ListSortDirection.Ascending);
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

        private void btnUpdate_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            modeEdit = true;
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

    public delegate void ModelPropertyChanged(string property);

    /// <summary>
    /// Выдача результата базовой форме.
    /// </summary>
    public class BaseModelResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class VisitorsModelResult : BaseModelResult
    {
        public int OrganizationId { get; set; }
        public string Organization { get; set; }
    }
}
