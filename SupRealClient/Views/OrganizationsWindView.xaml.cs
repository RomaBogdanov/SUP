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

namespace SupRealClient
{
    /// <summary>
    /// Логика взаимодействия для OrganizationsWindView.xaml
    /// </summary>
    public partial class OrganizationsWindView : Window
    {
        public bool IsRealClose { get; set; } = false;

        public OrganizationsWindView()
        {
            InitializeComponent();
            Base1ModelAbstr b = new Base1OrganizationsModel(
                (Base1ViewModel)base2.DataContext);
            b.OnClose += Handling_OnClose;
            base2.SetViewModel(b);
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Тип",
                Binding = new Binding("Type")
            };
            base2.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Название организации",
                Binding = new Binding("Name")
            };
            base2.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Примечание",
                Binding = new Binding("Comment")
            };
            base2.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Основное название",
                Binding = new Binding("FullName")
            };
            base2.baseTab.Columns.Add(dataGridTextColumn);
        }

        private void Handling_OnClose()
        {
            this.Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                e.Cancel = true;
                Handling_OnClose();
            }
        }
    }
}
