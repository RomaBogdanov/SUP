﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupRealClient.EnumerationClasses
{
    public class Visitor : IdEntity
    {
        public string FullName { get; set; }
        public string Organization { get; set; }
        public string Comment { get; set; }
    }
}
