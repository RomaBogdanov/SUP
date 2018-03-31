using SupRealClient.ViewModels;
using System.Windows;

namespace SupRealClient
{
	/// <summary>
	/// Interaction logic for ErrorWindow.xaml
	/// </summary>
	public partial class ErrorWindow : Window
	{
		public ErrorWindow(ErrorViewModel viewModel)
		{
			DataContext = viewModel;
			InitializeComponent();
		}
	}
}
