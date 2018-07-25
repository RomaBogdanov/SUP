using SupRealClient.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для FullNameView.xaml
    /// </summary>
    public partial class FullNameView
    {
        public FullNameView(FullNameViewModel viewModel)
        {
            // сортируем список
            viewModel.Orgs = new ObservableCollection<EnumerationClasses.Organization>
                                (viewModel.Orgs.OrderBy(o => (o as FullNameOrganization)?.OrgFullName));
            // селектируем первую организацию
            if (viewModel.Orgs.Count > 0)
                viewModel.SelectedOrg = 0;

            DataContext = viewModel;
            InitializeComponent();

            viewModel.OnClose += Handling_OnClose;

            AfterInitialize();
            tbxSearch.Focus();           
        }

        private void MetroWindow_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.None)
            {
                if (e.Key == Key.Tab)
                {
                    e.Handled = true;
                }

                FullNameViewModel vm = DataContext as FullNameViewModel;
                if (vm == null)
                    return;

                if (e.Key == Key.Up && vm.SelectedOrg - 1 >= 0)
                {                    
                    vm.SelectedOrg--; 
                }
                if (e.Key == Key.Down && vm.SelectedOrg + 1 <= vm.Orgs.Count - 1)
                {                 
                    vm.SelectedOrg++;
                }
            }
        }

        private void MetroWindow_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            tbxSearch.Focus();  
        }

        private void MetroWindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            tbxSearch.Focus();
        }
    }
}
