using SupRealClient.ViewModels;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для FullNameView.xaml
    /// </summary>
    public partial class FullNameView
    {
        public FullNameView(FullNameViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();

            viewModel.OnClose += Handling_OnClose;

            AfterInitialize();
        }
    }
}
