using System;
using System.Collections.Generic;
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
using System.ComponentModel;
using  SupRealClient;
using SupRealClient.Models.AddUpdateModel;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для AddUpdateBidWindView.xaml
    /// </summary>
    public partial class AddUpdateBidWindView 
    {
        /// <summary>
        /// Двигатель фокуса
        /// </summary>
        private TraversalRequest _focusMover = new TraversalRequest(FocusNavigationDirection.Next);

        public AddUpdateBidWindView()
        {
            InitializeComponent();
        }
        public object WindowResult { get; set; }

        public void Handling_OnClose(object result)
        {
            WindowResult = result;
            this.Close();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            butVisitorName.Focus();
        }

        private void butVisitorName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                butCatcherName.Focus();
                e.Handled = true;
            }
        }

        private void butCatcherName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                tbTimeFrom.Focus();
                e.Handled = true;
            }
        }

      
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
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

    /*
    public class AddUpdateBidWindViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public AddUpdateAbstrModel Model { get; set; }

        public string Visitor { get; set; }

        public string Organization { get; set; }

        public string Catcher { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public string Passes { get; set; }

        public ICommand Ok { get; set; }

        public ICommand Cancel { get; set; }

        public AddUpdateBidWindViewModel()
        {
            Ok = new RelayCommand(arg => OkCommand());
            Cancel = new RelayCommand(arg => CancelCommand());
        }

        private void OkCommand()
        {
            Model.Ok();
        }


        private void CancelCommand()
        {
            Model.Cancel();
        }

    }
    */
}
