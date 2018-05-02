namespace SupRealClient.EnumerationClasses
{
    public class Organization : IdEntity
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public string FullName { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
    }
}
