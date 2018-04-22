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
        }

        public DataGrid BaseTab
        {
            get { return baseTab; }
            set
            {
                baseTab = value;
            }
        }

        public void SetDefaultColumn()
        {
            if (baseTab.Columns.Count > 0)
            {
                baseTab.CurrentColumn = baseTab.Columns[0];
            }
        }
    }
}
