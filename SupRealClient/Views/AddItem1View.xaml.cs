using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.Windows;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для AddItem1View.xaml
    /// </summary>
    public partial class AddItem1View : Window
    {
        public AddItem1View(IAddItem1Model model)
        {
            model.OnClose += Handling_OnClose;
            DataContext = new AddItem1ViewModel();
            ((AddItem1ViewModel)DataContext).SetModel(model);
            InitializeComponent();
        }
    }
}
