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
    /// Логика взаимодействия для Base4SpacesWindView.xaml
    /// </summary>
    public partial class Base4SpacesWindView
    {
        public Base4SpacesWindView()
        {
            InitializeComponent();
            base4.tbxSearch.Focus();
            AfterInitialize();

            ((Base4ViewModel<EnumerationClasses.Space>)base4.DataContext)
                .ScrollCurrentItem = base4.ScrollIntoViewCurrentItem;
            base4.Focus();
        }

        private void CreateColumns()
        {
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Реальный номер",
                Binding = new Binding("NumReal")
            };
            base4.BaseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Строительный номер",
                Binding = new Binding("NumBuild")
            };
            base4.BaseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Описание",
                Binding = new Binding("Descript")
            };
            base4.BaseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Примечание",
                Binding = new Binding("Note")
            };
            base4.BaseTab.Columns.Add(dataGridTextColumn);
        }

        private void SetDefaultColumn()
        {
            base4.baseTab.CurrentColumn = base4.baseTab.Columns[0];
        }
    }
}
