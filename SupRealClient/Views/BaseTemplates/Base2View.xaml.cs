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
        int memCountRows = 0;

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
        private void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (((Button)sender).Name == "butAdd")
                memCountRows = baseTab.Items.Count;
            else
                baseTab.SelectionChanged += baseTab_SelectionChanged;
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

            if (memCountRows + 1 == baseTab.Items.Count)
            {
                memCountRows = 0;
                baseTab.SelectedItems.Clear();
                baseTab.SelectionChanged += baseTab_SelectionChanged;
                baseTab.SelectedItem = baseTab.Items[baseTab.Items.Count - 1];
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

        private void baseTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            baseTab.ScrollIntoView(baseTab.CurrentItem);
            baseTab.UpdateLayout();
            baseTab.ScrollIntoView(baseTab.CurrentItem);

            baseTab.SelectionChanged -= baseTab_SelectionChanged;
        }
    }
}
