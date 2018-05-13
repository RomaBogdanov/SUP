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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SupRealClient.ViewModels;

namespace SupRealClient.Views.ListViews
{
    /// <summary>
    /// Interaction logic for BaseListSmallView.xaml
    /// </summary>
    public partial class BaseListSmallView : UserControl
    {
        public BaseListSmallView()
        {
            InitializeComponent();
            DataGrid.Focus();
        }

        private void BaseListSmallView_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up & !DataGrid.IsKeyboardFocusWithin)
            {
                btnprev.Command.Execute(null);
            }
            else if (e.Key == Key.Down & !DataGrid.IsKeyboardFocusWithin)
            {
                btnnext.Command.Execute(null);
            }
            else if (e.Key == Key.Enter)
            {
                btnok.Command.Execute(null);
            }
            else if (e.Key == Key.Insert)
            {
                ((IBaseListViewModelStandartCommands)DataContext).Add.Execute(null);
            }
            else if (e.Key == Key.Home)
            {
                ((IBaseListViewModelStandartCommands)DataContext).Begin.Execute(null);
            }
            else if (e.Key == Key.End)
            {
                ((IBaseListViewModelStandartCommands)DataContext).End.Execute(null);
            }
        }

        private void DataGrid_OnKeyDown(object sender, KeyEventArgs e)
        {
            tbxSearch.Focus();
        }
    }
}
