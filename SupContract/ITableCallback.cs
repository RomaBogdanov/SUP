using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace SupContract
{
    public interface ITableCallback
    {
        [OperationContract(IsOneWay = true)]
        void InsRow(string tableName, object[] objs);
        [OperationContract(IsOneWay = true)]
        void UpdRow(string tableName, int rowNumber, object[] objs);
        [OperationContract(IsOneWay = true)]
        void DelRow(string tableName, object[] objs);
    }
}
