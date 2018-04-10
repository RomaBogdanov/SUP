using System;

namespace SupRealClient.EnumerationClasses
{
    public class Document : IdEntity
    {
        public string DocName { get; set; }
        public string Deleted { get; set; }
        public DateTime RecDate { get; set; }
        public int RecOperator { get; set; }
    }
}
