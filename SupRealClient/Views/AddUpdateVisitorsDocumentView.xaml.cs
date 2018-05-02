using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для AddUpdateVisitorsDocumentView.xaml
    /// </summary>
    public partial class AddUpdateVisitorsDocumentView
    {
        /// <summary>
        /// Двигатель фокуса
        /// </summary>
        private TraversalRequest _focusMover = new TraversalRequest(FocusNavigationDirection.Next);

        public AddUpdateVisitorsDocumentView(VisitorsDocumentModel model)
        {
            model.OnClose += Handling_OnClose;
            DataContext = new AddUpdateVisitorsDocumentViewModel();
            ((AddUpdateVisitorsDocumentViewModel)DataContext).SetModel(model);
            InitializeComponent();

            AfterInitialize();
            NameTextBox.Focus();
        }

        /// <summary>
        /// Конструктор - заглушка
        /// </summary>
        public AddUpdateVisitorsDocumentView()
        {
            InitializeComponent();
            NameTextBox.Focus();
            DataContext = new AddUpdateVisitorsDocumentViewModel();
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
