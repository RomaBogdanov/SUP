using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupRealClient.EnumerationClasses
{
    public class Card2 : IdEntity
    {
        public string Card { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public DateTime Change { get; set; }
        public string Operator { get; set; }
        public string OrderNum { get; set; }
        public string Comment { get; set; }
    }
}
