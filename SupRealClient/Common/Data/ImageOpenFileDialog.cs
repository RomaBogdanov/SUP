using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SupRealClient.Common.Data
{
    public class ImageOpenFileDialog
    {
		private OpenFileDialog openFileDialog = new OpenFileDialog();

		public string FileName
		{
			get { return openFileDialog?.FileName; }
		}

		public string SafeFileName
		{
			get { return openFileDialog?.SafeFileName; }
		}

		public ImageOpenFileDialog()
		{
			openFileDialog.Multiselect = false;
			openFileDialog.Filter = "Изображения (*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG";
			//openFileDialog.FilterIndex = 1;
			openFileDialog.CheckFileExists = true;
		}


		public DialogResult ShowDialog()
		{
			return openFileDialog.ShowDialog();
		}

		public DialogResult ShowDialog(IWin32Window owner)
		{
			return openFileDialog.ShowDialog(owner);
		}

	}
}
