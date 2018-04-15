using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupRealClient.Models;
using SupRealClient.Models.OrganizationStructure.Interfaces;

namespace SupRealClient.ViewModels
{
    public partial class DetailVisitsViewModel : ViewModelBase
    {
        public DetailVisitsViewModel(IModel model)
        {
            var localModel = (Visit)model;

            Id = localModel.Id;
            StartTime = localModel.StartTime;
            EndTime = localModel.EndTime;
            Human = localModel.Visitor;
            Organization = localModel.Organization;
            Pass = localModel.Pass;
            RealBidId = localModel.RealBidId;
            RemovedBidId = localModel.RemovedBidId;
            Activ = true;
        }
    }
}
