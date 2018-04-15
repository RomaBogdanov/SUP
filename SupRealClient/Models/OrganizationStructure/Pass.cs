using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupRealClient.Models.OrganizationStructure.Interfaces;

namespace SupRealClient.Models.OrganizationStructure
{
    public class Pass : IModel
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime TimePassChange { get; set; }
        public Human Human { get; set; }
        public int BidId { get; set; }
        public string Additionally { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public string Description { get; set; }
        public bool Save { get; set; }
        public event Action OnClose;
        public void EditItem()
        {
            throw new NotImplementedException();
        }

        public void Cancel()
        {
            throw new NotImplementedException();
        }
    }
}
