using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupRealClient.Models.OrganizationStructure;
using SupRealClient.Models.OrganizationStructure.Interfaces;

namespace SupRealClient.ViewModels
{
    public class UniversalViewModel<T> where T : IModel
    {
        public T Model { get; set; }
    }
}
