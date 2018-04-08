using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupRealClient.Models.OrganizationStructure
{
    public interface IModel : INotifyPropertyChanged
    {
        string Description { get; set; }
        bool Save { get; set; }
    }
}
