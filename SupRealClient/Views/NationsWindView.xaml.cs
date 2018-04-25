using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для NationsWindView.xaml
    /// </summary>
    public partial class NationsWindView : Window
    {
        public NationsWindView()
        {
            InitializeComponent();
            Base1ModelAbstr b = new Base1NationsModel(
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
                Binding = new Binding("CountryName")
            };
            base1.BaseTab.Columns.Add(dataGridTextColumn);
            base1.SetDefaultColumn();
        }
    }
}
