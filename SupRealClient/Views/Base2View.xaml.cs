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

namespace SupRealClient
{
    /// <summary>
    /// Логика взаимодействия для Organizations1View.xaml
    /// </summary>
    public partial class Base2View : UserControl
    {
        Base1ViewModel viewModel = new Base1ViewModel();

        public Base2View()
        {
            DataContext = viewModel;
        }

        public void SetViewModel(Base1ModelAbstr model)
        {
            ((Base1ViewModel)DataContext).SetModel(model);
            InitializeComponent();
        }

        public DataGrid BaseTab
        {
            get { return baseTab; }
            set
            {
                baseTab = value;
            }
        }
    }
}
