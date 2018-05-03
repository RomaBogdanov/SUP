using SupRealClient.EnumerationClasses;
using SupRealClient.ViewModels;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для VisitorsDocumentView.xaml
    /// </summary>
    public partial class DocumentImagesView
    {
        public DocumentImagesView(VisitorsDocument document)
        {
            DataContext = new DocumentImagesViewModel();
            ((DocumentImagesViewModel)DataContext).SetDocument(document);
            InitializeComponent();

            AfterInitialize();
        }
    }
}
