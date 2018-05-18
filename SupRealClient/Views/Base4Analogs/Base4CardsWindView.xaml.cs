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

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для Base4CardsWindView.xaml
    /// </summary>
    public partial class Base4CardsWindView
    {
        public Base4CardsWindView()
        {
            InitializeComponent();
            base4.tbxSearch.Focus();
            AfterInitialize();
            base4.Focus();
        }


        private void CreateColumns()
        {
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Пропуск",
                Binding = new Binding("CurdNum")
            };
            base4.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Занесён в БД",
                Binding = new Binding("CreateDate")
            };
            base4.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "№ MAFW",
                Binding = new Binding("NumMAFW")
            };
            base4.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Примечание",
                Binding = new Binding("Comment")
            };
            base4.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Состояние",
                Binding = new Binding("State")
            };
            base4.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Кому выдан",
                Binding = new Binding("ReceiversName")
            };
            base4.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Утерян",
                Binding = new Binding("Lost")
            };
            base4.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Изменён",
                Binding = new Binding("ChangeDate")
            };
            base4.baseTab.Columns.Add(dataGridTextColumn);
            base4.SetDefaultColumn();
        }

        private void SetDefaultColumn()
        {
            base4.baseTab.CurrentColumn = base4.baseTab.Columns[0];
        }
    }
}
