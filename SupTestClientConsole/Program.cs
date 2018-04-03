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
            ITableService tableService = new TableServiceClient(null);
            CompositeType ct = new CompositeType();
            ct.TableName = TableName.VisOrders;
            //ct.TableName = TableName.TestTable1;
            //ct.TableName = TableName.TestTable2Ado;
            try
            {
                DataTable dt = tableService.GetTable(ct, "");
                foreach (var item in dt.Select())
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        Console.Write("{0}\t|", item[i]);
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            
            Console.ReadLine();
        }
    }
}
