using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SupHost
{
    interface ITableBehavior
    {
        DataTable GetTable();
        void InsertRow();
        void UpdateRow();
        void DeleteRow();
    }
}
