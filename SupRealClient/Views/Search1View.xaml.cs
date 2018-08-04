using SupRealClient.Common.Interfaces;
using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.Windows;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для Search1View.xaml
    /// </summary>
    public partial class Search1View
    {
        public Search1View(ISearchHelper searchHelper)
		{
			var model = new Search1Model();
			model.SetSearchHelper(searchHelper);
			model.OnClose += Handling_OnClose;
			DataContext = new Search1ViewModel();
			((Search1ViewModel)DataContext).SetModel(model);
			InitializeComponent();

            AfterInitialize();
        }
    }
}
