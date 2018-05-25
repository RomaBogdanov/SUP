using System;

namespace SupRealClient.EnumerationClasses
{
    public abstract class EntityBase : IdEntity
    {
        public bool Deleted { get; set; }

        public DateTime RecDate { get; set; }

        public int RecOperator { get; set; }
    }
}
