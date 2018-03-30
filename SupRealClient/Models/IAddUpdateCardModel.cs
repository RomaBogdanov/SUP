using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupRealClient
{
    public interface IAddUpdateCardModel
    {
        event Action OnClose;
        AddUpdateCardViewModel ViewModel { set; }
        void Ok();
        void Cancel();
        void ChangeState();
    }
}
