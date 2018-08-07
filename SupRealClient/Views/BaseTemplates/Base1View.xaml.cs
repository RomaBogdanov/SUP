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
        }

        public DataGrid BaseTab
        {
            get { return baseTab; }           
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

        private void UserControl_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.None)
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
                tbxSearch.Text = string.Empty;
                SortItemsSource();
            }
        }                     

        private void baseTab_Loaded(object sender, RoutedEventArgs e)
        {
            SortItemsSource();
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
