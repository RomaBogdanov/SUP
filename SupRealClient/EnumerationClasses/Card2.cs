using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupRealClient.EnumerationClasses
{
    public class Card2 : IdEntity
    {
        public int CardIdHi { get; set; }
        public int CardIdLo { get; set; }
        public string Card { get; set; }
        public string CardNumber { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public DateTime Change { get; set; }
        public string Operator { get; set; }
        public string OrderNum { get; set; }
        public string Comment { get; set; }
        public string Orders { get; set; }
		public int StateId { get; set; }
    }
}
