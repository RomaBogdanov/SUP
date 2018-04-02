using SupRealClient.Common.Interfaces;
using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для NationsWindView.xaml
    /// </summary>
    public partial class NationsWindView : Window, IWindow
    {
        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "NationsWindView";

        public IWindow ParentWindow { get; set; }

        public NationsWindView()
        {
            InitializeComponent();
            Base1ModelAbstr b = new Base1NationsModel(
                (Base1ViewModel)base1.DataContext, this);
            b.OnClose += Handling_OnClose;
            base1.SetViewModel(b);
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Название",
                Binding = new Binding("CountryName")
            };
            base1.BaseTab.Columns.Add(dataGridTextColumn);
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
