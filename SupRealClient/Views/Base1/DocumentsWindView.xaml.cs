using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для DocumentsWindView.xaml
    /// </summary>
    public partial class DocumentsWindView
    {
        public DocumentsWindView()
        {
            InitializeComponent();

            // TODO - потом убрать, когда все View на новой модели будут
            base1.SetViewModel(null);

            // TODO - потом перенести в генерируемый код
            Base4ViewModel<EnumerationClasses.Document> viewModel =
            new Base4ViewModel<EnumerationClasses.Document>
            {
                OkCaption = "OK",
                ZonesVisibility = Visibility.Hidden,
                Parent = this,
                Model = new DocumentsListModel<EnumerationClasses.Document>(),
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
                Binding = new Binding("DocName")
            };
            base1.BaseTab.Columns.Add(dataGridTextColumn);
            base1.SetDefaultColumn();
        }
    }
}
