using System;

namespace SupRealClient.EnumerationClasses
{
    public class VisitorsMainDocument : VisitorsDocumentBase
    {
        public string Type { get; set; }
        public string Seria { get; set; }
        public string Num { get; set; }
        public DateTime Date { get; set; }
        public string Org { get; set; }
        public DateTime DateTo { get; set; }
        public string Code { get; set; }
        public string Comment { get; set; }

        public override string ToString()
        {
            return Type + " - Серия " + Seria + ", № " + Num +
                ", Дата выдачи " + Date.ToShortDateString();
        }
    }
}
