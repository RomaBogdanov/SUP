using SupRealClient.Common.Interfaces;
using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.ComponentModel;
using System.Windows;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для AddUpdateCardView.xaml
    /// </summary>
    public partial class AddUpdateCardView : Window, IWindow
    {
        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "AddUpdateCardView";

        public IWindow ParentWindow { get; set; }

        public AddUpdateCardView(IAddUpdateCardModel model)
        {
            model.OnClose += Handling_OnClose;
            DataContext = new AddUpdateCardViewModel();
            ((AddUpdateCardViewModel)DataContext).SetModel(model);
            InitializeComponent();
        }

        private void Handling_OnClose()
        {
            this.Hide();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

        public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                Handling_OnClose();
            }
        }
    }
}
