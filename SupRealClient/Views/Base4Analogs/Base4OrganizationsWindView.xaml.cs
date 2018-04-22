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
    /// Логика взаимодействия для Base4OrganizationsWindView.xaml
    /// </summary>
    public partial class Base4OrganizationsWindView : Window
    {
        public Base4OrganizationsWindView()
        {
            InitializeComponent();
            Base4ViewModel<EnumerationClasses.Organization> viewModel = 
                new Base4ViewModel<EnumerationClasses.Organization>//(new OrganizationsListModel<EnumerationClasses.Organization>());
            {
                Model = new OrganizationsListModel<EnumerationClasses.Organization>()
            };
            base4.DataContext = viewModel;
            CreateColumns();
        }

        private void CreateColumns()
        {
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Тип",
                Binding = new Binding("Type")
            };
            base4.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Название организации",
                Binding = new Binding("Name")
            };
            base4.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Примечание",
                Binding = new Binding("Comment")
            };
            base4.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Основное название",
                Binding = new Binding("FullName")
            };
            base4.baseTab.Columns.Add(dataGridTextColumn);
            // TODO: Продумать как пришить к старым техникам.
            //base4.SetDefaultColumn();
        }
    }
}
