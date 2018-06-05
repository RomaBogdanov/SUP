using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using SupRealClient.Common.Interfaces;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для AddUpdateSpaceView.xaml
    /// </summary>
    public partial class AddUpdateSpaceView : IWindow
    {
        /// <summary>
        /// Двигатель фокуса
        /// </summary>
        private TraversalRequest _focusMover = new TraversalRequest(FocusNavigationDirection.Next);

        public AddUpdateSpaceView()
        {
            InitializeComponent();
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

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            tbNumReal.Focus();
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
