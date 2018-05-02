using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.Windows;
using System.Windows.Input;

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

        public VisitorsDocumentView(VisitorsDocumentModel model)
        {
            model.OnClose += Handling_OnClose;
            DataContext = new VisitorsDocumentViewModel();
            ((VisitorsDocumentViewModel)DataContext).SetModel(model);
            InitializeComponent();

            AfterInitialize();
            NameTextBox.Focus();
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
