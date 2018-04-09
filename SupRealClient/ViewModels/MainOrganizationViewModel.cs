using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SupRealClient.Models.OrganizationStructure;
using SupRealClient.Views;

namespace SupRealClient.ViewModels
{
    public class MainOrganizationViewModel : ViewModelBase
    {
        public object SelectedObject
        {
            get { return _selectedObject; }
            set
            {
                _selectedObject = value;
                if (value.GetType().GetInterface("IUnit") != null)
                {
                    UnitEnabled = true;
                    DepartmentEnabled = false;
                }
                else if (value.GetType().GetInterface("IDepartment") != null)
                {
                    UnitEnabled = true;
                    DepartmentEnabled = true;
                }
                else if (value.GetType().GetInterface("IOrganization") != null)
                {
                    UnitEnabled = false;
                    DepartmentEnabled = true;
                }
                OnPropertyChanged();
            }
        }
        private object _selectedObject;

        public ObservableCollection<MainOrganization> Organizations
        {
            get { return _organizations; }
            set
            {
                _organizations = value;
                OnPropertyChanged();
            }
        } 
        private ObservableCollection<MainOrganization> _organizations = new ObservableCollection<MainOrganization>();

        public bool UnitEnabled
        {
            get { return _unitEnabled; }
            set
            {
                _unitEnabled = value; 
                OnPropertyChanged();
            }
        }
        private bool _unitEnabled;

        public bool DepartmentEnabled
        {
            get { return _departmentEnabled; }
            set
            {
                _departmentEnabled = value;
                OnPropertyChanged();
            }
        }
        private bool _departmentEnabled;

        public ICommand AddDepartmentCommand { get; set; }
        public ICommand AddUnitCommand { get; set; }
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public MainOrganizationViewModel()
        {
            var unit = new Unit { Description = "Unit1" };

            var department = new Department { Description = "Department1" };
            department.Items.Add(unit);

            var organization = new Organization { Description = "Organization1" };
            organization.Items.Add(department);

            var mainOrganization = new MainOrganization { Description = "MainOrganization1" };
            mainOrganization.Items.Add(organization);

            Organizations.Add(mainOrganization);

            AddUnitCommand = new RelayCommand(AddUnit());
        }

        private Action<object> AddUnit()
        {
            var action = new Action<object>(obj => OpenAddUnitWindow());

            return action;
        }

        private void OpenAddUnitWindow()
        {
            var viewModel = new UnitViewModel();
            var window = new AddUnitView {DataContext = viewModel};
            window.ShowDialog();

            var dc = window.DataContext;

            dc.ToString();

            var newUnit = (Unit) ((UnitViewModel) dc).Model;

            ((Department)SelectedObject).Items.Add(newUnit);
        }
    }
}
