using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.Windows;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для AddUpdateCardView.xaml
    /// </summary>
    public partial class AddUpdateCardView : Window
    {
        public AddUpdateCardView(IAddUpdateCardModel model)
        {
            model.OnClose += Handling_OnClose;
            DataContext = new AddUpdateCardViewModel();
            ((AddUpdateCardViewModel)DataContext).SetModel(model);
            InitializeComponent();
        }
    }
}
