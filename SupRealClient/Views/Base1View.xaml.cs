using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.Windows.Controls;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для Base1View.xaml
    /// </summary>
    public partial class Base1View : UserControl
    {
        Base1ViewModel viewModel = new Base1ViewModel();

        public Base1View()
        {
            DataContext = viewModel;
        }

        public void SetViewModel(Base1ModelAbstr model)
        {
            ((Base1ViewModel)DataContext).SetModel(model);
            InitializeComponent();
            
            /*baseTab.Focus();
            baseTab.SelectedIndex = 2;*/
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
