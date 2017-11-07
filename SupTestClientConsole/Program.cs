using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using SupTestClientConsole.TableServiceReference;

namespace SupTestClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ITableService tableService = new TableServiceClient();
            CompositeType ct = new CompositeType();
            ct.TableName = TableName.TestTable1;
            DataTable dt = tableService.GetTable(ct);
            foreach (var item in dt.Select())
            {
                Console.WriteLine("{0} | {1} | {2}", item[0], item[1], item[2]);
            }
            Console.ReadLine();
        }
    }
}
