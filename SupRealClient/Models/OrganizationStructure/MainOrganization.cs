using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupRealClient.Models.OrganizationStructure
{
    public class MainOrganization : ModelBase
    {
        public ObservableCollection<Organization> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Organization> _items = new ObservableCollection<Organization>();
    }
}
