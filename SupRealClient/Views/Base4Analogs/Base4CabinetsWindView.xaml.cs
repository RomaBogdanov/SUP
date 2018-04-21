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
    /// Логика взаимодействия для Base4CabinetsWindView.xaml
    /// </summary>
    public partial class Base4CabinetsWindView : Window
    {
        public Base4CabinetsWindView()
        {
            InitializeComponent();
            Base4ViewModel<EnumerationClasses.Cabinet> viewModel =
    new Base4ViewModel<EnumerationClasses.Cabinet>
    {
        Model = new CabinetsListModel<EnumerationClasses.Cabinet>()
    };
            base4.DataContext = viewModel;
            CreateColumns();
        }

        private void CreateColumns()
        {
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Номер кабинета",
                Binding = new Binding("CabNum")
            };
            base4.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Описание",
                Binding = new Binding("Descript")
            };
            base4.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Номер двери",
                Binding = new Binding("DoorNum")
            };
            base4.baseTab.Columns.Add(dataGridTextColumn);
            //base3.SetDefaultColumn();
        }
    }
}
