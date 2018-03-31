using System;

namespace SupRealClient.EnumerationClasses
{
    class Document
    {
        public int Id { get; set; }
        public string DocName { get; set; }
        public string Deleted { get; set; }
        public DateTime RecDate { get; set; }
        public int RecOperator { get; set; }
    }
}
