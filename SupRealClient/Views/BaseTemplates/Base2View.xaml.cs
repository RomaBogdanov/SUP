using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;
using SupRealClient.Common.Interfaces;
using SupRealClient.EnumerationClasses;
using System.Windows.Media;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для Organizations1View.xaml
    /// </summary>
    public partial class Base2View : UserControl
    {

        public Base2View()
        {
            InitializeComponent();

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
            DataGridRow oRow = e.Row;
            var item = oRow.Item;
            if (item is Organization)
            {
                if (IsOrgHasSynonim(item as Organization))
                    oRow.Background = Brushes.LightGreen;
                else if (oRow.GetIndex() % 2 == 0)
                    oRow.Background = Brushes.White;
                else
                    oRow.Background = Brushes.AliceBlue;
            }
        }

        bool IsOrgHasSynonim(Organization org)
        {            
            if (org.FullName == string.Empty)
                foreach (var item in baseTab.ItemsSource)
                {
                    if ((item as Organization).FullName == org.Type + @" " + org.Name)
                        return true;
                }

            return false;
        }
    }
}
