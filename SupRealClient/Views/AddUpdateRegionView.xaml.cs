using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для AddUpdateOrgsView.xaml
    /// </summary>
    public partial class AddUpdateRegionView
    {
        /// <summary>
        /// Двигатель фокуса
        /// </summary>
        private TraversalRequest _focusMover = new TraversalRequest(FocusNavigationDirection.Next);

        public AddUpdateRegionView(IAddUpdateRegionModel model)
        {
            model.OnClose += Handling_OnClose;
            DataContext = new AddUpdateRegionViewModel();
            ((AddUpdateRegionViewModel)DataContext).SetModel(model);
            InitializeComponent();

            AfterInitialize();            
        }

        /// <summary>
        /// Конструктор - заглушка
        /// </summary>
        public AddUpdateRegionView()
        {
            InitializeComponent();
            DataContext = new AddUpdateOrgsViewModel(this);
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            NameTextBox.Focus();
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

        private void btnOK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnOK.Command.Execute(null);
            }
        }        
    }
}
