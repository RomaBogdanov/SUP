using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.Windows;
using System.Windows.Input;
using RegulaLib;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для VisitorsDocumentExtView.xaml
    /// </summary>
    public partial class VisitorsMainDocumentView
    {
        /// <summary>
        /// Двигатель фокуса
        /// </summary>
        private TraversalRequest _focusMover = new TraversalRequest(FocusNavigationDirection.Next);

        public VisitorsMainDocumentView(VisitorsMainDocumentModel model, bool editable, CPerson person)
        {
            model.OnClose += Handling_OnClose;
            DataContext = new VisitorsMainDocumentViewModel(editable,person);
            ((VisitorsMainDocumentViewModel)DataContext).SetModel(model);
            InitializeComponent();

            AfterInitialize();
        }

        /// <summary>
        /// Конструктор - заглушка
        /// </summary>
        public VisitorsMainDocumentView()
        {
            InitializeComponent();
            DataContext = new VisitorsMainDocumentViewModel(false,null);
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
