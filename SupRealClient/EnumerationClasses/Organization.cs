﻿namespace SupRealClient.EnumerationClasses
{
    public class Organization : IdEntity
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public string FullName { get; set; }
    }
}
