using System.Collections.ObjectModel;

namespace SupRealClient.Models.OrganizationStructure
{
    public class Department : ModelBase, IDepartment
    {
        public int ParentId;

        public ObservableCollection<Department> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Department> _items =
            new ObservableCollection<Department>();        
    }
}
