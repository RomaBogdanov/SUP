using System.Windows.Input;
using SupRealClient.EnumerationClasses;
using SupRealClient.ViewModels;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для VisitorsDocumentView.xaml
    /// </summary>
    public partial class DocumentImagesView
    {
        public DocumentImagesView(VisitorsDocumentBase document)
        {
            DataContext = new DocumentImagesViewModel();
            ((DocumentImagesViewModel)DataContext).SetDocument(document);
            InitializeComponent();

            AfterInitialize();

			
		}

	    private void Window_PreviewKeyDown_AndStop(object sender, KeyEventArgs e)
	    {
		    if (e.Key == Key.Escape)
		    {
			    e.Handled = false;
			    Close();
		    }
	    }
	}
}
