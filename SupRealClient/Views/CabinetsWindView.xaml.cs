using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для CabinetsWindView.xaml
    /// </summary>
    public partial class CabinetsWindView : Window
    {
        public CabinetsWindView()
        {
            InitializeComponent();

            base3.Init();

            // TODO - потом перенести в генерируемый код
            Base4ViewModel<EnumerationClasses.Cabinet> viewModel =
            new Base4ViewModel<EnumerationClasses.Cabinet>
            {
                OkCaption = "OK",
                ZonesVisibility = Visibility.Visible,
                Parent = this,
                Model = new CabinetsListModel<EnumerationClasses.Cabinet>(),
            };
            viewModel.Model.OnClose += Handling_OnClose;
            base3.DataContext = viewModel;

            AfterInitialize();
        }

        partial void CreateColumns()
        {
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Номер кабинета",
                Binding = new Binding("CabNum")
            };
            base3.BaseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Описание",
                Binding = new Binding("Descript")
            };
            base3.BaseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Номер двери",
                Binding = new Binding("DoorNum")
            };
            base3.BaseTab.Columns.Add(dataGridTextColumn);
            base3.SetDefaultColumn();
        }
    }
}
