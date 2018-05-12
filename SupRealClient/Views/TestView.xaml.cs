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

namespace SupRealClient.Views
{
    /// <summary>
    /// Interaction logic for TestView.xaml
    /// </summary>
    public partial class TestView
    {
        public TestView()
        {
            InitializeComponent();

            //BaseListSmallView.aaa.Focus();

            BaseListSmallView.Focus();
        }

        private void TestView_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
            else if (e.Key == Key.Up & !BaseListSmallView.DataGrid.IsKeyboardFocusWithin)
            {
                BaseListSmallView.btnprev.Command.Execute(null);
            }
            else if (e.Key == Key.Down & !BaseListSmallView.DataGrid.IsKeyboardFocusWithin)
            {
                BaseListSmallView.btnnext.Command.Execute(null);
            }
            else if (e.Key == Key.Home & !BaseListSmallView.DataGrid.IsKeyboardFocusWithin)
            {
                BaseListSmallView.btnstart.Command.Execute(null);
            }
            else if (e.Key == Key.End & !BaseListSmallView.DataGrid.IsKeyboardFocusWithin)
            {
                BaseListSmallView.btnend.Command.Execute(null);
            }
        }
    }
}
