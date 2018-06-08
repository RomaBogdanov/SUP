using SupRealClient.Models;
using SupRealClient.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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

        public AddUpdateOrgsView(IAddUpdateOrgsModel model)
        {
            model.OnClose += Handling_OnClose;
            DataContext = new AddUpdateOrgsViewModel();
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
            DataContext = new AddUpdateOrgsViewModel();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            TypeTextBox.Focus();
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
            string tbText = TypeTextBox.Text;

            TypeTextBox.SelectedValue = null;
            TypeTextBox.Text = tbText;
            locationEndCursorCB(TypeTextBox);             

            if (!TypeTextBox.IsDropDownOpen)
                TypeTextBox.IsDropDownOpen = true;

            CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(TypeTextBox.ItemsSource);

            //cv.Filter = ((o) =>
            //{
            //    if (String.IsNullOrEmpty(tbText)) return true;
            //    else
            //    {
            //        if (o != null && ((string)o).StartsWith(tbText)) return true;
            //        else return false;
            //    }
            //});
            //cv.Refresh();


            foreach (var item in cv)
            {
                if (item != null && ((string)item)==(tbText))
                {
                    TypeTextBox.SelectedValue = item;
                    e.Handled = true;
                    break;
                }
            }
        }

        void locationEndCursorCB(ComboBox cmBox)
        {
            var textBox = (cmBox.Template.FindName("PART_EditableTextBox",
                           cmBox) as TextBox);
            if (textBox != null)
            {
                textBox.Focus();
                textBox.SelectionStart = textBox.Text.Length;
            }
        }
    } 
}
