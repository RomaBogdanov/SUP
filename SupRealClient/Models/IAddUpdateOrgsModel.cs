using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupRealClient
{
    public interface IAddUpdateOrgsModel
    {
        event Action OnClose;
        AddUpdateOrgsViewModel ViewModel { set; }
        void Ok();
        void Cancel();
    }
}
