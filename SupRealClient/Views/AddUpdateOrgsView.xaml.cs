using SupRealClient.Models;
using SupRealClient.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

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
        
        public AddUpdateOrgsView(IAddUpdateOrgsModel model)
        {
            model.OnClose += Handling_OnClose;
            DataContext = new AddUpdateOrgsViewModel(this);
            ((AddUpdateOrgsViewModel)DataContext).SetModel(model);
            InitializeComponent();
            
            AfterInitialize();            
        }

        /// <summary>
        /// Конструктор - заглушка
        /// </summary>
        public AddUpdateOrgsView()
        {
            InitializeComponent();            
            DataContext = new AddUpdateOrgsViewModel(this);
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            TypeTextBox.Focus();

            if (!butFullName.IsEnabled)
                 this.Background = Brushes.LightGreen;

            TypeTextBox.KeyUp -= TextBox_OnKeyUp;
        }

        /// <summary>
        /// Для не раскрытия списка TypeTextBox, при открытии формы с помощью Ctrl+D
        /// </summary>
        private void TypeTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TypeTextBox.KeyUp += TextBox_OnKeyUp;
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
            else if (sender is ComboBox && ((ComboBox)sender).Name == "TypeTextBox")
            {
                TypeTextBoxTextChanged(sender, e);
            }
            else if (sender is ComboBox && ((ComboBox)sender).Name == "NameTextBox")
            {
                NameTextBoxTextChanged(sender, e);
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

        void TypeTextBoxTextChanged(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Escape)
                return;

            int selectionStartType;
            var textBox = (TypeTextBox.Template.FindName("PART_EditableTextBox",
                           TypeTextBox) as TextBox);    

            if (e.Key != Key.Up && e.Key != Key.Down)
            {
                selectionStartType = textBox.SelectionStart;
                CollectionView cv = ((AddUpdateOrgsViewModel)DataContext).TypeList;
                cv.Refresh();
            }
            else
                selectionStartType = textBox.Text.Length;

            if (TypeTextBox.Items.Count > 0)
                TypeTextBox.IsDropDownOpen = true;
            else
                TypeTextBox.IsDropDownOpen = false;

            textBox.SelectionStart = selectionStartType;            
        }
        
        void NameTextBoxTextChanged(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Escape)
                return;

            var textBox = (NameTextBox.Template.FindName("PART_EditableTextBox",
                           NameTextBox) as TextBox);

            int selectionStartName;
            if (e.Key != Key.Up && e.Key != Key.Down)
            {
                selectionStartName = textBox.SelectionStart;
                CollectionView cv = ((AddUpdateOrgsViewModel)DataContext).DescriptionList;
                cv.Refresh();
            }
            else
                selectionStartName = textBox.Text.Length;

            if (NameTextBox.Items.Count > 0)
                NameTextBox.IsDropDownOpen = true;
            else
                NameTextBox.IsDropDownOpen = false;

            textBox.SelectionStart = selectionStartName;
        }
    } 
}
