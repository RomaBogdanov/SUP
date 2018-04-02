using SupRealClient.Common.Interfaces;
using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.ComponentModel;
using System.Windows;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для AddItem1View.xaml
    /// </summary>
    public partial class AddItem1View : Window, IWindow
    {
        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "AddItem1View";

        public IWindow ParentWindow { get; set; }

        public AddItem1View(IAddItem1Model model)
        {
            model.OnClose += Handling_OnClose;
            DataContext = new AddItem1ViewModel();
            ((AddItem1ViewModel)DataContext).SetModel(model);
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
