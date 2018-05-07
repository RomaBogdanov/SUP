using SupRealClient.Models;

namespace SupRealClient.EnumerationClasses
{
    public class Organization : IdEntity
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public string FullName { get; set; }
        public int CountryId { get; set; } = 0;
        public string Country { get; set; }
        public int RegionId { get; set; } = 0;
        public string Region { get; set; }
        public int SynId { get; set; } = 0;

        public override string ToString()
        {
            return Type + OrganizationsHelper.UntrimName(Name);
        }
    }
}
