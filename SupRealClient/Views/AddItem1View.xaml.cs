using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для AddItem1View.xaml
    /// </summary>
    public partial class AddItem1View
    {
        public AddItem1View(IAddItem1Model model)
        {
            model.OnClose += Handling_OnClose;
            DataContext = new AddItem1ViewModel();
            ((AddItem1ViewModel)DataContext).SetModel(model);
            InitializeComponent();

            AfterInitialize();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            tbInputCountry.Focus();
        }

        private void btnOK_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnOK.Command.Execute(null);
            }
        }
    }
}
