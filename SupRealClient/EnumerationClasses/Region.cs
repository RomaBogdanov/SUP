using System;

namespace SupRealClient.EnumerationClasses
{
    public class Region : IdEntity
    {
        public string Name { get; set; }
        public int CountryId { get; set; } = 0;
        public string Country { get; set; }
        public string Deleted { get; set; }
        public DateTime RecDate { get; set; }
        public int RecOperator { get; set; }
    }
}
