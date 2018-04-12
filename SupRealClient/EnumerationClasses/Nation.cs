using System;

namespace SupRealClient.EnumerationClasses
{
    public class Nation : IdEntity
    {
        public string CountryName { get; set; }
        public string Deleted { get; set; }
        public DateTime RecDate { get; set; }
        public int RecOperator { get; set; }
    }
}
