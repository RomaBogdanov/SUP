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
using SupRealClient.EnumerationClasses;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для VisitorsListWindView.xaml
    /// </summary>
    public partial class VisitorsListWindView : Window
    {
        public VisitorsListWindView()
        {
            InitializeComponent();
            var viewModel = new Base4ViewModel<SupRealClient.EnumerationClasses.Visitor>
            {
                Model = new VisitorsListModel<SupRealClient.EnumerationClasses.Visitor>()
            };
            base4.DataContext = viewModel;
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "ФИО",
                Binding = new Binding("FullName")
            };
            base4.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Организация",
                Binding = new Binding("Organization")
            };
            base4.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Примечание",
                Binding = new Binding("Comment")
            };
            base4.baseTab.Columns.Add(dataGridTextColumn);
        }
    }
}
