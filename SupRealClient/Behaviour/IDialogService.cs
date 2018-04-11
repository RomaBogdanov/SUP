using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupRealClient.Behaviour
{
    interface IDialogService
    {
        void ShowMessage(string message);
        string OpenFileDialog();
    }
}
