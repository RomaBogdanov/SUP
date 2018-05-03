using System;
using System.Collections.Generic;

namespace SupRealClient.EnumerationClasses
{
    public class VisitorsDocument : IdEntity
    {
        public string Name { get; set; }
        public int VisitorId { get; set; }
        public List<Guid> Images { get; set; } = new List<Guid>();
        public bool IsChanged { get; set; } = false;

        public override string ToString()
        {
            return Name;
        }
    }
}
