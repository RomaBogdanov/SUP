using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.Windows;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для AddUpdateOrgsView.xaml
    /// </summary>
    public partial class AddUpdateOrgsView : Window
    {
        public AddUpdateOrgsView(IAddUpdateOrgsModel model)
        {
            model.OnClose += Handling_OnClose;
            DataContext = new AddUpdateOrgsViewModel();
            ((AddUpdateOrgsViewModel)DataContext).SetModel(model);
            InitializeComponent();
        }

        private void Handling_OnClose()
        {
            this.Close();
        }
    }
}
