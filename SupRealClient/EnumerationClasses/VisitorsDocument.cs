namespace SupRealClient.EnumerationClasses
{
    public class VisitorsDocument : IdEntity
    {
        public string Name { get; set; }
        public int VisitorId { get; set; } = 0;

        public override string ToString()
        {
            return Name;
        }
    }
}
