using System;
using System.Collections.Generic;

namespace SupRealClient.EnumerationClasses
{
    public abstract class VisitorsDocumentBase : IdEntity
    {
        public int VisitorId { get; set; }
        public int TypeId { get; set; }
        public List<Guid> Images { get; set; } = new List<Guid>();
        public bool IsChanged { get; set; } = false;
    }
}
