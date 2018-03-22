using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupRealClient
{
    public interface IAddItem1Model
    {
        event Action OnClose;
        AddItem1ViewModel ViewModel { set; }
        void Ok();
        void Cancel();
    }
}
