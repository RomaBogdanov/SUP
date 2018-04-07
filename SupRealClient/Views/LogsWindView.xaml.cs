using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для LogsWindView.xaml
    /// </summary>
    public partial class LogsWindView : Window
    {
        public LogsWindView()
        {
            InitializeComponent();
            Base1ModelAbstr b = new Base1LogsModel(
                (Base1ViewModel)base1.DataContext, this);
            b.OnClose += Handling_OnClose;
            base1.SetViewModel(b);
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Дата",
                Binding = new Binding("RecDate")
            };
            base1.BaseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Уровень",
                Binding = new Binding("Severity")
            };
            base1.BaseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Сообщение",
                Binding = new Binding("Message")
            };
            base1.BaseTab.Columns.Add(dataGridTextColumn);
            //dataGridTextColumn = new DataGridTextColumn
            //{
            //    Header = "Пользователь",
            //    Binding = new Binding("RecOperator")
            //};
            //base1.BaseTab.Columns.Add(dataGridTextColumn);
        }
    }
}
