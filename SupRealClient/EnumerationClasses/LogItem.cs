using System;

namespace SupRealClient.EnumerationClasses
{
    class LogItem
    {
        public long Id { get; set; }
        public int RecOperator { get; set; }
        public string Severity { get; set; }
        public string Message { get; set; }
        public DateTime RecDate { get; set; }
        public string Machine { get; set; }
    }
}
