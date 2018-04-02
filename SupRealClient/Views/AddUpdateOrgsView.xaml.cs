using SupRealClient.Common.Interfaces;
using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.ComponentModel;
using System.Windows;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для AddUpdateOrgsView.xaml
    /// </summary>
    public partial class AddUpdateOrgsView : Window, IWindow
    {
        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "AddUpdateOrgsView";

        public IWindow ParentWindow { get; set; }

        public AddUpdateOrgsView(IAddUpdateOrgsModel model)
        {
            model.OnClose += Handling_OnClose;
            DataContext = new AddUpdateOrgsViewModel();
            ((AddUpdateOrgsViewModel)DataContext).SetModel(model);
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
