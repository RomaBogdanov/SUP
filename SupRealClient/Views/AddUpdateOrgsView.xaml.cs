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
    }
}
