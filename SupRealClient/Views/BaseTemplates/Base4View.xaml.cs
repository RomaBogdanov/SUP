using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using MahApps.Metro.Controls;
using SupRealClient.Common.Interfaces;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для Base4View.xaml
    /// </summary>
    public partial class Base4View : UserControl
    {
        public bool modeEdit { get; set; } // меняются в VisitorsListModel
		public bool modeWatch { get; set; } // меняются в VisitorsListModel

		DataGridColumnHeader headerCliked = null;        

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
                else if (e.Key == Key.Home)
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
            if ((e.Key == Key.Back || (e.Key >= Key.A && e.Key <= Key.Z) || (e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)) && !Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                SelectSearchBox();
            }
        }

        public void SelectSearchBox()
        {
	        if (tbxSearch.IsFocused == false)
	        {
		        tbxSearch.Focus();
			}
        }

        private void baseTab_Loaded(object sender, RoutedEventArgs e)
        {
            SortItemsSource();
	        Keyboard.Focus(baseTab);
        }

        private void UserControl_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow.Visibility == System.Windows.Visibility.Visible)
            {
				// tbxSearch.Focus();
				Keyboard.Focus(baseTab);
				tbxSearch.Text = string.Empty;
                SortItemsSource();
            }
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

            if (dataGrid.Items.Count > 0)
                dataGrid.SelectedItem = dataGrid.Items[0];

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

    public delegate void ModelPropertyChanged(string property);

    /// <summary>
    /// Выдача результата базовой форме.
    /// </summary>
    public class BaseModelResult
    {
        public int Id { get; set; }
        public int IdHi { get; set; }
        public int IdLo { get; set; }
        public string Name { get; set; }
    }

    public class VisitorsModelResult : BaseModelResult
    {
        public int OrganizationId { get; set; }
        public string Organization { get; set; }
		public bool IsBlock { get; set; }
		public bool IsCardIssue { get; set; }
    }
	public class OrdersModelResult : BaseModelResult
	{
		public bool IsDisable { get; set; }
		public DateTime From { get; set; }
		public DateTime To { get; set; }
		public string Signed { get; set; }
		public string Agree { get; set; }
		public string Notes { get; set; }
	}

}
