using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupHost
{
    class GetTestTable1Behavior : IGetTableBehavior
    {
        DataTable table;

        public GetTestTable1Behavior()
        {
            this.table = new DataTable("TabTest1");
            this.table.Columns.Add("ColTest1");
            this.table.Columns.Add("ColTest2");
            this.table.Columns.Add("ColTest3");
            this.CreateRows();
        }

        public DataTable GetTable()
        {
            return this.table;
        }

        void CreateRows()
        {
            this.AddRow("1", "2", "3");
            this.AddRow("Roma", "Bogdanov", "Developer");
            this.AddRow("Hello", "World", "!");
        }

        void AddRow(string column1, string column2, string column3)
        {
            DataRow dr = this.table.NewRow();
            dr[0] = column1;
            dr[1] = column2;
            dr[2] = column3;
            this.table.Rows.Add(dr);
        }
    }
}
