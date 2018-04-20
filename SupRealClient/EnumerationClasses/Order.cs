using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupRealClient.EnumerationClasses
{
    public class Order : IdEntity
    {
        public string RegNumber { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Catcher { get; set; }
        public string OrderType { get; set; }
        public string Passes { get; set; }
    }
}
