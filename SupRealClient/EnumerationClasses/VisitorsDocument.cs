namespace SupRealClient.EnumerationClasses
{
    public class VisitorsDocument : VisitorsDocumentBase
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
