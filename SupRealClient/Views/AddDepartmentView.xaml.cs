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
    /// Interaction logic for AddDepartmentView.xaml
    /// </summary>
    public partial class AddDepartmentView 
    {
        public AddDepartmentView()
        {
            InitializeComponent();
            tbName.Focus();
        }

        private void UIElement_OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //MessageBox.Show("Когда будет понятно логика, нужно подвесить исполнение команды на событие!");
            }
        }
    }
}
