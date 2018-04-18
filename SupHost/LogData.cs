using System;

namespace SupHost
{
    class LogData
    {
        public DateTime Date { get; set; }

        public string Severity { get; set; }

        public string Message { get; set; }

        public string Class { get; set; }

        public int User { get; set; }

        public string Machine { get; set; }
    }
}
