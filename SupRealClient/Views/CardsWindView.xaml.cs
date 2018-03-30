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
    /// Логика взаимодействия для CardsWindView.xaml
    /// </summary>
    public partial class CardsWindView : Window
    {
        public bool IsRealClose { get; set; } = false;

        public CardsWindView()
        {
            InitializeComponent();
            Base1ModelAbstr b = new Base1CardsModel(
                (Base1ViewModel)base2.DataContext);
            b.OnClose += Handling_OnClose;
            base2.SetViewModel(b);
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Пропуск",
                Binding = new Binding("CurdNum")
            };
            base2.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Занесён в БД",
                Binding = new Binding("CreateDate")
            };
            base2.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "№ MAFW",
                Binding = new Binding("NumMAFW")
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
                Header = "Состояние",
                Binding = new Binding("State")
            };
            base2.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Кому выдан",
                Binding = new Binding("ReceiversName")
            };
            base2.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Утерян",
                Binding = new Binding("Lost")
            };
            base2.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Изменён",
                Binding = new Binding("ChangeDate")
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
