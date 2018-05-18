using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для BaseOrgs.xaml
    /// </summary>
    public partial class BaseOrgsView
    {
        public BaseOrgsView()
        {
            InitializeComponent();
            base1.tbxSearch.Focus();
            Base1ModelAbstr b = new BaseOrgsModel(
                (Base1ViewModel)base1.DataContext, this);
            b.OnClose += Handling_OnClose;
            base1.SetViewModel(b);
            AfterInitialize();
            base1.Focus();
        }

        partial void CreateColumns()
        {
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
            base1.SetDefaultColumn();
        }
    }
}
