using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;
using SupRealClient.Common.Interfaces;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для Organizations1View.xaml
    /// </summary>
    public partial class Base2View : UserControl
    {
        int memCountRows;

        public Base2View()
        {
            InitializeComponent();

            baseTab.LoadingRow -= baseTab_LoadingRow;
            //DataContext = viewModel;
        }
        
        public void SetViewModel(Base1ModelAbstr model)
        {
            ((Base1ViewModel)DataContext).SetModel(model);
            InitializeComponent();
            tbxSearch.Focus();
        }

        public DataGrid BaseTab
        {
            get { return baseTab; }
            set
            {
                baseTab = value;
                baseTab.Focus();
            }
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
            //if (e.Key == Key.Down)
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
            if (e.Key == Key.Up & !BaseTab.IsKeyboardFocusWithin)
            {
                btnUp.Command.Execute(null);
                e.Handled = true;
            }
            else if (e.Key == Key.Down & !BaseTab.IsKeyboardFocusWithin)
            {
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

        private void baseTab_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (memCountRows + 1 == baseTab.Items.Count)
            {
                baseTab.SelectedItems.Clear();
                e.Row.IsSelected = true;
                baseTab.ScrollIntoView(e.Row.Item);
                baseTab.UpdateLayout();
                baseTab.ScrollIntoView(e.Row.Item);

                int LodingRowHashCode = e.Row.Item.GetHashCode();
                int LastRowHashCode = baseTab.Items[baseTab.Items.Count - 1].GetHashCode();

                if (LodingRowHashCode == LastRowHashCode)                                  
                    baseTab.LoadingRow -= baseTab_LoadingRow;

                //int LodingRowHashCode = e.Row.Item.GetHashCode();
                //int LastRowHashCode = baseTab.Items[baseTab.Items.Count - 1].GetHashCode();

                //if (LodingRowHashCode == LastRowHashCode)
                //{
                //    baseTab.SelectedItems.Clear();
                //    e.Row.IsSelected = true;
                //    baseTab.ScrollIntoView(baseTab.Items[baseTab.Items.Count - 1]);
                //    baseTab.UpdateLayout();
                //    baseTab.ScrollIntoView(e.Row.Item);
                //    baseTab.LoadingRow -= baseTab_LoadingRow;
                //}
            }
            else
            {
                baseTab.LoadingRow -= baseTab_LoadingRow;
            }
        }

        private void butAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            memCountRows = baseTab.Items.Count;
            baseTab.LoadingRow += baseTab_LoadingRow;
        }

        private void btnEdit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            baseTab.LoadingRow -= baseTab_LoadingRow;
        }
    }
}
