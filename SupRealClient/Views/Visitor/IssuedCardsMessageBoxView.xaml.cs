using SupRealClient.ViewModels;

namespace SupRealClient.Views.Visitor
{
    /// <summary>
    /// Interaction logic for IssuedCardsMessageBoxView.xaml
    /// </summary>
    public partial class IssuedCardsMessageBoxView
    {
        public IssuedCardsMessageBoxView(IssuedCardsMessageBoxViewModel viewModel)
        {
            viewModel.OnClose += () => this.Close();
            this.DataContext = viewModel;

            InitializeComponent();
        }
    }
}
