using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupRealClient.EnumerationClasses
{
    public class Order : IdEntity
    {
        public int Number { get; set; } = 0;
        public int TypeId { get; set; } = 0;
        public string Type { get; set; } = "";
        public string RegNumber { get; set; } = "";
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Catcher { get; set; } = "";
        public string OrderType { get; set; } = "";
        public string Passes { get; set; } = "";
    }
}
