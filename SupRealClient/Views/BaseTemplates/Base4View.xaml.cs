using System.Windows.Controls;
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

        public Base4View()
        {
            InitializeComponent();

            baseTab.SelectionChanged -= baseTab_SelectionChanged;
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

        private void baseTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            baseTab.ScrollIntoView(baseTab.CurrentItem);
            baseTab.UpdateLayout();
            baseTab.ScrollIntoView(baseTab.CurrentItem);

            baseTab.SelectionChanged -= baseTab_SelectionChanged;
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
