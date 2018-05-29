using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SupRealClient.Common.Interfaces;

namespace SupRealClient.Views.AddUpdateView
{
    /// <summary>
    /// Логика взаимодействия для AddUpdateDoorWindView.xaml
    /// </summary>
    public partial class AddUpdateDoorWindView : IWindow
    {
        /// <summary>
        /// Двигатель фокуса
        /// </summary>
        private TraversalRequest _focusMover = new TraversalRequest(FocusNavigationDirection.Next);

        public AddUpdateDoorWindView()
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

        private void TextBox_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ((UIElement)sender).MoveFocus(_focusMover);
            }
        }
    }
}
