using System;

namespace SupRealClient.EnumerationClasses
{
    public class Region : IdEntity
    {
        public string RegionName { get; set; }
        public string Deleted { get; set; }
        public DateTime RecDate { get; set; }
        public int RecOperator { get; set; }
    }
}
