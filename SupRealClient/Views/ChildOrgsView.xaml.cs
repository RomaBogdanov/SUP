using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для ChildOrgs.xaml
    /// </summary>
    public partial class ChildOrgsView : Window
    {
        public ChildOrgsView()
        {
            InitializeComponent();
            Base1ModelAbstr b = new ChildOrgsModel(
                (Base1ViewModel)base1.DataContext, this);
            b.OnClose += Handling_OnClose;
            base1.SetViewModel(b);
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Тип",
                Binding = new Binding("Type")
            };
            base1.BaseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Название",
                Binding = new Binding("Name")
            };
            base1.BaseTab.Columns.Add(dataGridTextColumn);
            base1.btnUpdate.Content = "Удалить";
        }
    }
}
