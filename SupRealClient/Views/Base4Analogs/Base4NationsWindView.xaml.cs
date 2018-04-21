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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для Base4NationsWindView.xaml
    /// </summary>
    public partial class Base4NationsWindView : Window
    {
        public Base4NationsWindView()
        {
            InitializeComponent();
            Base4ViewModel<EnumerationClasses.Nation> viewModel =
    new Base4ViewModel<EnumerationClasses.Nation>
    {
        Model = new NationsListModel<EnumerationClasses.Nation>()
    };
            base4.DataContext = viewModel;
            CreateColumns();
        }

        private void CreateColumns()
        {
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Название",
                Binding = new Binding("CountryName")
            };
            base4.baseTab.Columns.Add(dataGridTextColumn);
            //base4.SetDefaultColumn();
        }
    }
}
