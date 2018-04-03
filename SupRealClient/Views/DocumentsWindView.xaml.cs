using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для DocumentsWindView.xaml
    /// </summary>
    public partial class DocumentsWindView : Window
    {
        public bool IsRealClose { get; set; } = false;

        public DocumentsWindView()
        {
            InitializeComponent();
            Base1ModelAbstr b = new Base1DocsModel(
                (Base1ViewModel)base1.DataContext);
            b.OnClose += Handling_OnClose;
            base1.SetViewModel(b);
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Название",
                Binding = new Binding("DocName")
            };
            base1.BaseTab.Columns.Add(dataGridTextColumn);
        }

        private void Handling_OnClose()
        {
            this.Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                e.Cancel = true;
                Handling_OnClose();
            }
        }
    }
}
