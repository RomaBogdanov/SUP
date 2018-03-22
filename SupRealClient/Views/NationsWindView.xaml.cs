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
    /// Логика взаимодействия для NationsWindView.xaml
    /// </summary>
    public partial class NationsWindView : Window
    {
        public bool IsRealClose { get; set; } = false;

        public NationsWindView()
        {
            InitializeComponent();
            Base1ModelAbstr b = new Base1NationsModel(
                (Base1ViewModel)base1.DataContext);
            b.OnClose += Handling_OnClose;
            base1.SetViewModel(b);
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Название",
                Binding = new Binding("CountryName")
            };
            base1.BaseTab.Columns.Add(dataGridTextColumn);
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
