using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SupRealClient.ViewModels
{
	public class DocumentImageViewModel : ViewModelBase
	{
		public ICommand Close;
		public event Action Closing;

		private object _documentImage;

		public DocumentImageViewModel()
		{
			Close= new RelayCommand(arg => Closing?.Invoke());
		}

		public object DocumentImage
		{
			get { return _documentImage; }
			set
			{
				_documentImage = value;
				OnPropertyChanged(nameof(DocumentImage));
			}
		}
	}
}
