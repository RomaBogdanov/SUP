using SupRealClient.Common.Interfaces;
using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.ComponentModel;
using System.Windows;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для Search1View.xaml
    /// </summary>
    public partial class Search1View : Window, IWindow
    {
        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "Search1View";

        public IWindow ParentWindow { get; set; }

        public Search1View(ISearchHelper searchHelper)
		{
			var model = new Search1Model();
			model.SetSearchHelper(searchHelper);
			model.OnClose += Handling_OnClose;
			DataContext = new Search1ViewModel();
			((Search1ViewModel)DataContext).SetModel(model);
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
