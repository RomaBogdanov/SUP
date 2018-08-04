using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupRealClient.Models.OrganizationStructure.Interfaces;

namespace SupRealClient.Models.OrganizationStructure
{
    public class Organization : ModelBase, IOrganization
    {
        public ObservableCollection<Department> Items
        {
            get { return _items; }
            set
            {
                _items = value; 
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Department> _items = new ObservableCollection<Department>();        
    }

    internal interface IOrganization
    {
    }
}
