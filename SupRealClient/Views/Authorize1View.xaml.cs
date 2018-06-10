using SupRealClient.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для Authorize1View.xaml
    /// </summary>
    public partial class Authorize1View : UserControl
    {
        /// <summary>
        /// Двигатель фокуса
        /// </summary>
        private TraversalRequest _focusMover = new TraversalRequest(FocusNavigationDirection.Next);

        public Authorize1View()
        {
            InitializeComponent();
        }

        public void Reset()
        {
            (this.DataContext as Authorize1ViewModel).Reset();
        }

        private void TextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MoveFocusCursor();
            }
        }

        private void Button_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnOK.Command.Execute(null);
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            txtLogin.Focus();
            MoveFocusCursor();
        }

        /// <summary>
        /// Переводит фокус на следующий элемент.
        /// </summary>
        private void MoveFocusCursor()
        {
            UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;
            if (elementWithFocus != null)
            {
                elementWithFocus.MoveFocus(_focusMover);
            }
        }
    }
}
