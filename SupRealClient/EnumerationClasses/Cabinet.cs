using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupRealClient.EnumerationClasses
{
    public class Cabinet : IdEntity
    {
        public string CabNum { get; set; }
        public string Descript { get; set; } = "";
        public string DoorNum { get; set; }
    }
}
