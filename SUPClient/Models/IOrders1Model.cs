using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPClient
{
    interface IOrders1Model
    {
        void CreateOrder();
        void SaveOrder();
        void EditOrder();
        void DeleteOrder();
    }
}
