using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupRealClient
{
    class Nation
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public string Deleted { get; set; }
        public DateTime RecDate { get; set; }
        public int RecOperator { get; set; }
    }
}
