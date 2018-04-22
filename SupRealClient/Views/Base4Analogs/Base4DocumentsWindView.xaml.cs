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
    /// Логика взаимодействия для Base4DocumentsWindView.xaml
    /// </summary>
    public partial class Base4DocumentsWindView : Window
    {
        public Base4DocumentsWindView()
        {
            InitializeComponent();
            Base4ViewModel<EnumerationClasses.Document> viewModel =
                new Base4ViewModel<EnumerationClasses.Document>
                {
                    Model = new DocumentsListModel<EnumerationClasses.Document>()
                };
            base4.DataContext = viewModel;
            CreateColumns();
        }

        private void CreateColumns()
        {
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Название",
                Binding = new Binding("DocName")
            };
            base4.baseTab.Columns.Add(dataGridTextColumn);
            //base4.SetDefaultColumn();
        }
    }
}
