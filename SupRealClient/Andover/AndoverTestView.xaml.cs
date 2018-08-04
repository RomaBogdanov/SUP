using System.Windows.Controls;
using System.Windows.Data;

namespace SupRealClient.Andover
{
    /// <summary>
    /// Interaction logic for AndoverTestView.xaml
    /// </summary>
    public partial class AndoverTestView
    {
        public AndoverTestView()
        {
            InitializeComponent();

            var viewModel = new AndoverTestViewModel();
            this.DataContext = viewModel;

            CreateColumns();
        }

        private void CreateColumns()
        {
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Номер карты",
                Binding = new Binding("CurdNum")
            };
            this.cardsGrid.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Название",
                Binding = new Binding("Name")
            };
            this.cardsGrid.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Комментарий",
                Binding = new Binding("Comment")
            };
            this.cardsGrid.Columns.Add(dataGridTextColumn);

            var dataGridCheckBoxColumn = new DataGridCheckBoxColumn
            {
                Header = "",
                Binding = new Binding("IsChecked"),
                IsReadOnly = false,
            };
            this.zonesGrid.Columns.Add(dataGridCheckBoxColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Название",
                Binding = new Binding("Name"),
                IsReadOnly = true
            };
            this.zonesGrid.Columns.Add(dataGridTextColumn);
            /*dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Путь",
                Binding = new Binding("Path")
            };
            this.zonesGrid.Columns.Add(dataGridTextColumn);*/
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Внутреннее помещение",
                Binding = new Binding("SpaceIn"),
                IsReadOnly = true
            };
            this.zonesGrid.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Внешнее помещение",
                Binding = new Binding("SpaceOut"),
                IsReadOnly = true
            };
            this.zonesGrid.Columns.Add(dataGridTextColumn);
        }
    }
}
