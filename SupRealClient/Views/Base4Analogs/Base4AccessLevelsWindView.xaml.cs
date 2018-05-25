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
    /// Логика взаимодействия для Base4AccessLevelsWindView.xaml
    /// </summary>
    public partial class Base4AccessLevelsWindView
    {
        public Base4AccessLevelsWindView()
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
                Header = "Область доступа",
                Binding = new Binding("AreaId")
            };
            base4.BaseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Расписание",
                Binding = new Binding("ScheduleId")
            };
            base4.BaseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Описание уровня доступа",
                Binding = new Binding("AccessLevelNote")
            };
            base4.BaseTab.Columns.Add(dataGridTextColumn);

        }

        private void SetDefaultColumn()
        {
            base4.baseTab.CurrentColumn = base4.baseTab.Columns[0];
        }
    }
}
