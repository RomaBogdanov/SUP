using System;

namespace SupRealClient.EnumerationClasses
{
    public class Card
    {
        public int Id { get; set; }
        public int CurdNum { get; set; }
        public DateTime CreateDate { get; set; }
        public int NumMAFW { get; set; }
        public string Comment { get; set; }
        public string State { get; set; }
        public string ReceiversName { get; set; }
        public DateTime? Lost { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}
