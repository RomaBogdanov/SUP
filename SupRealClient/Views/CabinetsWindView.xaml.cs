using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SupRealClient.Models;
using SupRealClient.ViewModels;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для CabinetsWindView.xaml
    /// </summary>
    public partial class CabinetsWindView : Window
    {
        public CabinetsWindView()
        {
            InitializeComponent();
            Base3ModelAbstr b = new Base3CabinetsModel(
                (Base3ViewModel)base3.DataContext, this);
            b.OnClose += Handling_OnClose;
            base3.SetViewModel(b);
            CreateColumns();
        }

        private void CreateColumns()
        {
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Номер кабинета",
                Binding = new Binding("CabNum")
            };
            base3.BaseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Описание",
                Binding = new Binding("Descript")
            };
            base3.BaseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Номер двери",
                Binding = new Binding("DoorNum")
            };
            base3.BaseTab.Columns.Add(dataGridTextColumn);
            base3.SetDefaultColumn();
        }
    }
}
