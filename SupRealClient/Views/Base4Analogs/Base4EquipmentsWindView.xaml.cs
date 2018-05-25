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
    /// Логика взаимодействия для Base4EquipmentsWindView.xaml
    /// </summary>
    public partial class Base4EquipmentsWindView
    {
        public Base4EquipmentsWindView()
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
                Header = "Название",
                Binding = new Binding("Name")
            };
            base4.BaseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Количество",
                Binding = new Binding("Count")
            };
            base4.BaseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Уникальный идентификатор",
                Binding = new Binding("EquipNum")
            };
            base4.BaseTab.Columns.Add(dataGridTextColumn); dataGridTextColumn = new DataGridTextColumn
            {
                Header = "На внос",
                Binding = new Binding("PurposeIn")
            };
            base4.BaseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "На вынос",
                Binding = new Binding("PurposeOut")
            };
            base4.BaseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Организация",
                Binding = new Binding("OrgId")
            };
            base4.BaseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Посетитель",
                Binding = new Binding("VisId")
            };
            base4.BaseTab.Columns.Add(dataGridTextColumn);
        }

        private void SetDefaultColumn()
        {
            base4.baseTab.CurrentColumn = base4.baseTab.Columns[0];
        }
    }
}
