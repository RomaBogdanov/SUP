namespace SupRealClient.EnumerationClasses
{
    public class VisitorsDocument : VisitorsDocumentBase
    {
        public string Name { get; set; }

	    public bool IsCanAddChanges { get; set; } = true;

	    public override string ToString()
        {
            return Name;
        }
    }
}
