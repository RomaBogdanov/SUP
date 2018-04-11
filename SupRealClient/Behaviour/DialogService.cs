using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace SupRealClient.Behaviour
{
    public static class DialogService
    {
        public static void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        public static string OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }

            return "";
        }
    }
}
