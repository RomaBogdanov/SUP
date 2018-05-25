namespace SupRealClient.EnumerationClasses
{
    public class Region : EntityBase
    {
        public string Name { get; set; }
        public int CountryId { get; set; } = 0;
        public string Country { get; set; }
    }
}
