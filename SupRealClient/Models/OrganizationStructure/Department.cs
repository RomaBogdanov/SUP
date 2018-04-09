using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupRealClient.Models.OrganizationStructure.Interfaces;

namespace SupRealClient.Models.OrganizationStructure
{
    public class Department : ModelBase, IDepartment
    {
        public ObservableCollection<Unit> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Unit> _items = new ObservableCollection<Unit>();
    }
}
