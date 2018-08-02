using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.Windows;
using System.Windows.Input;
using SupRealClient.Handlers;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для VisitorsDocumentView.xaml
    /// </summary>
    public partial class VisitorsDocumentView
    {
        /// <summary>
        /// Двигатель фокуса
        /// </summary>
        private TraversalRequest _focusMover = new TraversalRequest(FocusNavigationDirection.Next);
	    public event VisitorsDocumentTestingNameHandler _TestingNameVisitorsDocument;

		public VisitorsDocumentView(VisitorsDocumentModel model)
        {
            model.OnClose += Handling_OnClose;
            DataContext = new VisitorsDocumentViewModel();
            ((VisitorsDocumentViewModel)DataContext).SetModel(model);
			((VisitorsDocumentViewModel)DataContext)._TestingNameVisitorsDocument += VisitorsDocumentView__TestingNameVisitorsDocument;
			

			InitializeComponent();

            AfterInitialize();
            NameTextBox.Focus();
        }

		private void VisitorsDocumentView__TestingNameVisitorsDocument(object sender, System.ComponentModel.CancelEventArgs e)
		{
			_TestingNameVisitorsDocument?.Invoke(sender, e);
		}

		/// <summary>
		/// Конструктор - заглушка
		/// </summary>
		public VisitorsDocumentView()
        {
            InitializeComponent();
            NameTextBox.Focus();
            DataContext = new VisitorsDocumentViewModel();
        }

        private void TextBox_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ((UIElement)sender).MoveFocus(_focusMover);
            }
        }
    }
}
