using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupRealClient.ViewModels
{
	public class DocumentImageViewModel : ViewModelBase
	{
		private object _documentImage;

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
