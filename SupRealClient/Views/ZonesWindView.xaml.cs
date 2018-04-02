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
    /// Логика взаимодействия для ZonesWindView.xaml
    /// </summary>
    public partial class ZonesWindView : Window
    {
        public ZonesWindView()
        {
            InitializeComponent();
            Base3ModelAbstr b = new Base3ZonesModel(
                (Base3ViewModel)base3.DataContext, this);
            b.OnClose += Handling_OnClose;
            base3.SetViewModel(b);
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Название",
                Binding = new Binding("DocName")
            };
            base3.BaseTab.Columns.Add(dataGridTextColumn);
        }
    }
}
