using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для NationsWindView.xaml
    /// </summary>
    public partial class NationsWindView
    {
        public NationsWindView()
        {
            InitializeComponent();

            // TODO - потом убрать, когда все View на новой модели будут
            base1.SetViewModel(null);

            // TODO - потом перенести в генерируемый код
            Base4ViewModel<EnumerationClasses.Nation> viewModel =
            new Base4ViewModel<EnumerationClasses.Nation>
            {
                OkCaption = "OK",
                ZonesVisibility = Visibility.Hidden,
                Parent = this,
                Model = new NationsListModel<EnumerationClasses.Nation>(),
            };
            viewModel.Model.OnClose += Handling_OnClose;
            base1.DataContext = viewModel;

            AfterInitialize();
        }

        partial void CreateColumns()
        {
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Название",
                Binding = new Binding("CountryName")
            };
            base1.BaseTab.Columns.Add(dataGridTextColumn);
            base1.SetDefaultColumn();
        }
    }
}
