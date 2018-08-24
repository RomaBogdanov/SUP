using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;
using SupRealClient.ViewModels;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Input;

namespace SupRealClient.Views.Visitor
{
    /// <summary>
    /// Interaction logic for DeactivateCardsView.xaml
    /// </summary>
    public partial class DeactivateCardsView
    {
        public DeactivateCardsView(DeactivateCardsViewModel viewModel)
        {
            viewModel.OnClose += () => this.Close();
            this.DataContext = viewModel;

            InitializeComponent();
        }
    }
}
