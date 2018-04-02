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
using SupRealClient.Models;

namespace SupRealClient.Views
{    
    /// <summary>
    /// Логика взаимодействия для Base3View.xaml
    /// </summary>
    public partial class Base3View : UserControl
    {
        Base1ViewModel viewModel = new Base3ViewModel();

        public Base3View()
        {
            DataContext = viewModel;
        }

        public void SetViewModel(Base3ModelAbstr model)
        {
            ((Base3ViewModel)DataContext).SetModel(model);
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
