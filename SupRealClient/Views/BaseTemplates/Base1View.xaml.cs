using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;
using SupRealClient.Common.Interfaces;
using System.Windows.Controls.Primitives;
using System.Windows;
using System.ComponentModel;
using SupRealClient.EnumerationClasses;
using System.Collections.ObjectModel;
using System.Linq;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для Base1View.xaml
    /// </summary>
    public partial class Base1View : UserControl
    {
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
        }

        private void UserControl_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow.Visibility == System.Windows.Visibility.Hidden)
            {
                tbxSearch.Text = string.Empty;
                baseTab.SelectedItems?.Clear();
                baseTab.SelectionChanged += baseTab_SelectionChanged;

                SortItemsSource();
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

            if (baseTab.ItemsSource is ObservableCollection<Nation>)
            {
                var nationSource = baseTab.ItemsSource as ObservableCollection<Nation>;
                var onlyRus = nationSource.Where(o => o.CountryName.ToUpper() == @"РОССИЯ");
                var withoutRus = nationSource.Where(o => o.CountryName.ToUpper() != @"РОССИЯ").OrderBy(o => o.CountryName);
                baseTab.ItemsSource = new ObservableCollection<Nation>(onlyRus.Union(withoutRus));
            }
            else if (baseTab.ItemsSource is ObservableCollection<Document>)
            {               
                var docSource = baseTab.ItemsSource as ObservableCollection<Document>;
                var onlyPass = docSource.Where(o => o.DocName.ToUpper() == @"ПАСПОРТ");
                var withoutPass = docSource.Where(o => o.DocName.ToUpper() != @"ПАСПОРТ").OrderBy(o => o.DocName);
                baseTab.ItemsSource = new ObservableCollection<Document>(onlyPass.Union(withoutPass));
            }

            if (baseTab.Items.Count > 0)
            {
                baseTab.SelectedItem = baseTab.Items[0];
            }                

            // Refresh items to display sort
            baseTab.Items.Refresh();
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
            
            dataGrid.CurrentColumn = dataGrid.Columns[columnIndex];
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
            SortItemsSource();
        }

        private void baseTab_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            
        }

        private void BaseTab_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Down && e.Key != Key.Up && Keyboard.Modifiers == ModifierKeys.None)
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
