using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для VisitorsDocumentExtView.xaml
    /// </summary>
    public partial class VisitorsDocumentExtView
    {
        /// <summary>
        /// Двигатель фокуса
        /// </summary>
        private TraversalRequest _focusMover = new TraversalRequest(FocusNavigationDirection.Next);

        public VisitorsDocumentExtView(VisitorsDocumentModel model)
        {
            model.OnClose += Handling_OnClose;
            DataContext = new VisitorsDocumentExtViewModel();
            ((VisitorsDocumentExtViewModel)DataContext).SetModel(model);
            InitializeComponent();

            AfterInitialize();
            NameTextBox.Focus();
        }

        /// <summary>
        /// Конструктор - заглушка
        /// </summary>
        public VisitorsDocumentExtView()
        {
            InitializeComponent();
            NameTextBox.Focus();
            DataContext = new VisitorsDocumentExtViewModel();
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
