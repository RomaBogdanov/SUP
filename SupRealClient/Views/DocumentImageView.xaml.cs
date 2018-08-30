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
using System.Windows.Shapes;
using SupRealClient.Models;
using SupRealClient.ViewModels;

namespace SupRealClient.Views
{
	/// <summary>
	/// Логика взаимодействия для DocumentImageView.xaml
	/// </summary>
	public partial class DocumentImageView 
	{
		private DocumentImageViewModel _viewModel = new DocumentImageViewModel();

		public DocumentImageView(bool isNew = false)
		{
			InitializeComponent();

			if (DataContext == null)
			{
				DataContext = _viewModel;
			}

			_viewModel.Closing += ViewModel_Closing;
		}

		private void ViewModel_Closing()
		{
			_viewModel.Closing -= ViewModel_Closing;
			Close();
		}

		public object DocumentImage
		{
			get =>_viewModel.DocumentImage; 
			set => _viewModel.DocumentImage = value;
		}

		public Guid DocumentImageGuid
		{
			set => _viewModel.DocumentImage = ImagesHelper.GetImagePath(value);
		}

		private void Window_PreviewKeyDown_AndStop(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				e.Handled = false;
				Close();
				e.Handled = true;
			}
		}
	}
}
