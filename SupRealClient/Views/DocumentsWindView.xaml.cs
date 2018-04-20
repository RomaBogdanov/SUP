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
        public DocumentsWindView()
        {
            InitializeComponent();
            Base1ModelAbstr b = new Base1DocsModel(
                (Base1ViewModel)base1.DataContext, this);
            b.OnClose += Handling_OnClose;
            base1.SetViewModel(b);
            CreateColumns();
        }

        private void CreateColumns()
        {
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Название",
                Binding = new Binding("DocName")
            };
            base1.BaseTab.Columns.Add(dataGridTextColumn);
            base1.SetDefaultColumn();
        }
    }
}
