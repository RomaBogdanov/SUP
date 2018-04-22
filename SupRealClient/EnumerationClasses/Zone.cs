using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupRealClient.EnumerationClasses
{
    public class Zone : IdEntity
    {
        public int ZoneNum { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string RelatedDoors { get; set; }
    }
}
