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

namespace SupRealClient
{
    /// <summary>
    /// Логика взаимодействия для AddItem1View.xaml
    /// </summary>
    public partial class AddItem1View : Window
    {
        public AddItem1View(IAddItem1Model model)
        {
            model.OnClose += Hanling_OnClose;
            DataContext = new AddItem1ViewModel();
            ((AddItem1ViewModel)DataContext).SetModel(model);
            InitializeComponent();
        }

        private void Hanling_OnClose()
        {
            this.Close();
        }
    }
}
