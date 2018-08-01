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
		}

		public object DocumentImage
		{
			get =>_viewModel.DocumentImage; 
			set => _viewModel.DocumentImage = value;
		}
	}
}
