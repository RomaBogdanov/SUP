using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using SupRealClient.Common.Interfaces;

namespace SupRealClient.Views.AddUpdateView
{
    /// <summary>
    /// Логика взаимодействия для AddUpdateTemplateView.xaml
    /// </summary>
    public partial class AddUpdateTemplateView : IWindow
    {
        /// <summary>
        /// Двигатель фокуса
        /// </summary>
        private TraversalRequest _focusMover = new TraversalRequest(FocusNavigationDirection.Next);

        public AddUpdateTemplateView()
        {
            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public bool CanMinimize { get; private set; }

        public bool IsRealClose { get; set; }
        public object WindowResult { get; set; }

        public string WindowName { get; private set; }

        public IWindow ParentWindow { get; set; }

        public void CloseWindow(CancelEventArgs e)
        {
        }

        public void Unsuscribe()
        {
            throw new NotImplementedException();
        }

        public void Handling_OnClose(object result)
        {
            WindowResult = result;
            this.Close();
        }

        private void TextBox_OnKeyUp(object sender, KeyEventArgs e)
        {
            tbName.Text = tbName.Text.TrimStart();
            tbDescript.Text = tbDescript.Text.TrimStart();

            if (e.Key == Key.Enter)
            {
                ((UIElement)sender).MoveFocus(_focusMover);
            }
        }
    }
}
