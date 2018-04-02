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
    /// Логика взаимодействия для OrganizationsWindView.xaml
    /// </summary>
    public partial class OrganizationsWindView : Window, IWindow
    {
        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "OrganizationsWindView";

        public IWindow ParentWindow { get; set; }

        public OrganizationsWindView()
        {
            InitializeComponent();
            Base1ModelAbstr b = new Base1OrganizationsModel(
                (Base1ViewModel)base2.DataContext, this);
            b.OnClose += Handling_OnClose;
            base2.SetViewModel(b);
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Тип",
                Binding = new Binding("Type")
            };
            base2.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Название организации",
                Binding = new Binding("Name")
            };
            base2.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Примечание",
                Binding = new Binding("Comment")
            };
            base2.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Основное название",
                Binding = new Binding("FullName")
            };
            base2.baseTab.Columns.Add(dataGridTextColumn);
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
