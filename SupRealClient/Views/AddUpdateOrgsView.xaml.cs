using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для AddUpdateOrgsView.xaml
    /// </summary>
    public partial class AddUpdateOrgsView
    {
        /// <summary>
        /// Двигатель фокуса
        /// </summary>
        private TraversalRequest _focusMover = new TraversalRequest(FocusNavigationDirection.Next);

        private bool _loaded = false;

        public AddUpdateOrgsView(IAddUpdateOrgsModel model)
        {
            Closing += (s, e) =>
            {
                TypePopup.IsOpen = false;
                NamePopup.IsOpen = false;
            };

            LocationChanged += (s, e) =>
            {
                if (_loaded)
                {
                    if (TypePopup.IsOpen)
                    {
                        TypePopup.IsOpen = false;
                        TypePopup.IsOpen = true;
                    }
                    if (NamePopup.IsOpen)
                    {
                        NamePopup.IsOpen = false;
                        NamePopup.IsOpen = true;
                    }
                }
            };

            Loaded += (s, e) =>
            {
                _loaded = true;
            };

            model.OnClose += Handling_OnClose;
            DataContext = new AddUpdateOrgsViewModel();
            ((AddUpdateOrgsViewModel)DataContext).SetModel(model);
            InitializeComponent();

            AfterInitialize();
            TypeTextBox.Focus();
        }

        /// <summary>
        /// Конструктор - заглушка
        /// </summary>
        public AddUpdateOrgsView()
        {
            InitializeComponent();
            TypeTextBox.Focus();
            DataContext = new AddUpdateOrgsViewModel();
        }
        
        private void TextBox_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ((UIElement) sender).MoveFocus(_focusMover);
            }
        }
        
        private void TypeTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (TypePopup != null)
            {
                TypePopup.IsOpen = true;
            }
        }
        
        private void TypeTextBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            TypePopup.IsOpen = false;
        }

        private void DescriptionTextBlock_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (NamePopup != null)
            {
                NamePopup.IsOpen = true;
            }
        }

        private void DescriptionTextBlock_OnLostFocus(object sender, RoutedEventArgs e)
        {
            NamePopup.IsOpen = false;
        }

        private void DescriptionTextBlock_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (NamePopup != null)
            {
                NamePopup.IsOpen = true;
            }
        }

        private void TypeTextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (TypePopup != null)
            {
                TypePopup.IsOpen = true;
            }
        }
    }
}
