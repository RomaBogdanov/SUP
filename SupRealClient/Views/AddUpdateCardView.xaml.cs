using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для AddUpdateCardView.xaml
    /// </summary>
    public partial class AddUpdateCardView
    {
        /// <summary>
        /// Двигатель фокуса
        /// </summary>
        private TraversalRequest _focusMover = new TraversalRequest(FocusNavigationDirection.Next);

        public AddUpdateCardView(IAddUpdateCardModel model)
        {
            model.OnClose += Handling_OnClose;
            DataContext = new AddUpdateCardViewModel();
            ((AddUpdateCardViewModel)DataContext).SetModel(model);
            InitializeComponent();

            AfterInitialize();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            tbCardNum.Focus();
        }

        private void TextBox_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null)
                {
                    elementWithFocus.MoveFocus(_focusMover);
                }
            }
        }

        private void tbComments_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnOK.Focus();
            }
        }

        private void btnOK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnOK.Command.Execute(null);
            }
        }
    }
}
