using SupRealClient.EnumerationClasses;
using SupRealClient.ViewModels;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для SynonimView.xaml
    /// </summary>
    public partial class SynonimView
    {
        public SynonimView(Organization data)
        {
            var viewModel = new SynonimViewModel(data);
            DataContext = viewModel;
            InitializeComponent();

            viewModel.OnClose += Handling_OnClose;

            AfterInitialize();
        }
    }
}
