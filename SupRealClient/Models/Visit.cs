using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupRealClient.Models.OrganizationStructure;
using SupRealClient.Models.OrganizationStructure.Interfaces;

namespace SupRealClient.Models
{
    public class Visit : IModel
    {
        public int Id { get; set; }
        public Human Visitor { get; set; }
        //Пока оставил так, возможно это будет класс организации
        public string Organization { get; set; }
        public Pass Pass { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string RealBidId { get; set; }
        public string RemovedBidId { get; set; }
        public Human Author { get; set; }
        public Human Steward { get; set; }

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
