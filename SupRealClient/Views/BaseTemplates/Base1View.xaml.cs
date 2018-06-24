using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;
using SupRealClient.Common.Interfaces;
using System.Windows.Controls.Primitives;
using System.Windows;
using System.ComponentModel;
using SupRealClient.EnumerationClasses;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для Base1View.xaml
    /// </summary>
    public partial class Base1View : UserControl
    {
        int memCountRows = 0;
        DataGridColumnHeader headerCliked = null;
        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public Base1View()
        {
            InitializeComponent();

            baseTab.SelectionChanged -= baseTab_SelectionChanged;
            //DataContext = viewModel;
        }

        public DataGrid BaseTab
        {
            get { return baseTab; }           
        }

        private void UserControl_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
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

        #region Под удаление

        Base1ViewModel viewModel = new Base1ViewModel();

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

        private void baseTab_Loaded(object sender, RoutedEventArgs e)
        {
            if (baseTab.ItemsSource is System.Collections.ObjectModel.ObservableCollection<Organization>)
                SortDataGrid(baseTab, 1, ListSortDirection.Ascending);
            else if (baseTab.Columns.Count > 0)
                SortDataGrid(baseTab, 0, ListSortDirection.Ascending);
        }

        private void baseTab_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (memCountRows + 1 == baseTab.Items.Count)
            {
                memCountRows = 0;
                baseTab.SelectedItems.Clear();
                baseTab.SelectionChanged += baseTab_SelectionChanged;
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

        private void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (((Button)sender).Name == "butAdd")
                memCountRows = baseTab.Items.Count;
            else
                baseTab.SelectionChanged += baseTab_SelectionChanged;
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
}
