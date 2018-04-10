using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SupRealClient.Models.OrganizationStructure;
using SupRealClient.Views;
using SupRealClient.TabsSingleton;
using System.Data;

namespace SupRealClient.ViewModels
{
    public class MainOrganizationViewModel : ViewModelBase
    {
        public event Action OnClose;
        int currentOrg;
        int currentDep;
        int currentUnit;
        CurrentLevel currentLevel = CurrentLevel.None;
        string description = "";

        enum CurrentLevel
        {
            None,
            Organization,
            Department,
            Unit
        }

        public object SelectedObject
        {
            get { return _selectedObject; }
            set
            {
                if (value != null)
                {
                    _selectedObject = value;
                    if (value.GetType().GetInterface("IUnit") != null)
                    {
                        UnitEnabled = true;
                        DepartmentEnabled = false;
                        currentUnit = ((Unit)value).Id;
                        description = ((Unit)value).Description;
                        currentDep = (int)DepartmentSectionWrapper.CurrentTable().Table
                            .Rows.Find(currentUnit)["f_dep_id"];
                        currentOrg = (int)DepartmentWrapper.CurrentTable().Table
                            .Rows.Find(currentDep)["f_org_id"];
                        currentLevel = CurrentLevel.Unit;
                    }
                    else if (value.GetType().GetInterface("IDepartment") != null)
                    {
                        UnitEnabled = true;
                        DepartmentEnabled = true;
                        currentDep = ((Department)value).Id;
                        description = ((Department)value).Description;
                        currentOrg = (int)DepartmentWrapper.CurrentTable().Table
                            .Rows.Find(currentDep)["f_org_id"];
                        currentLevel = CurrentLevel.Department;
                    }
                    else if (value.GetType().GetInterface("IOrganization") != null)
                    {
                        UnitEnabled = false;
                        DepartmentEnabled = true;
                        currentOrg = ((Organization)value).Id;
                        description = ((Organization)value).Description;
                        currentLevel = CurrentLevel.Organization;
                    }
                    OnPropertyChanged();
                }
                else
                {
                    currentLevel = CurrentLevel.None;
                }
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
        private ObservableCollection<MainOrganization> _organizations = 
            new ObservableCollection<MainOrganization>();

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
        public ICommand EditCommand { get; set; }

        public MainOrganizationViewModel()
        {
            OrganizationsWrapper.CurrentTable().OnChanged += Query;
            DepartmentWrapper.CurrentTable().OnChanged += Query;
            DepartmentSectionWrapper.CurrentTable().OnChanged += Query;
            Query();
            AddUnitCommand = new RelayCommand(AddUnit());
            AddDepartmentCommand = new RelayCommand(AddDepartment());
            EditCommand = new RelayCommand(Edit());
            OkCommand = new RelayCommand(Ok());
            //CancelCommand = new RelayCommand(Cancel());
        }

        private Action<object> AddDepartment()
        {
            var action = new Action<object>(obj =>
            {
                var viewModel = new UnitViewModel
                {
                    Model = new AddDepModel(this.currentOrg)
                };
                var window = new AddDepartmentView { DataContext = viewModel };
                viewModel.Model.OnClose += window.Close;
                window.ShowDialog();
            });
            return action;
        }

        private Action<object> AddUnit()
        {
            var action = new Action<object>(obj =>
            {
                var viewModel = new UnitViewModel
                {
                    Model = new AddUnitModel(this.currentDep)
                };
                var window = new AddUnitView { DataContext = viewModel };
                viewModel.Model.OnClose += window.Close;
                window.ShowDialog();
            });
            return action;
        }

        private Action<object> Edit()
        {
            return new Action<object>(obj => Ed());
        }

        private Action<object> Ok()
        {
            return new Action<object>(obj => Finish());
        }

        void Ed()
        {
            switch (currentLevel)
            {
                case CurrentLevel.None:
                    break;
                case CurrentLevel.Organization:
                    break;
                case CurrentLevel.Department:
                    var viewModel1 = new UnitViewModel
                    {
                        Model = new EditDepModel(this.currentDep)
                    };
                    viewModel1.Description = description;
                    var window1 = new AddDepartmentView { DataContext = viewModel1 };
                    viewModel1.Model.OnClose += window1.Close;
                    window1.ShowDialog();
                    break;
                case CurrentLevel.Unit:
                    var viewModel2 = new UnitViewModel
                    {
                        Model = new EditUnitModel(this.currentUnit)
                    };
                    viewModel2.Description = description;
                    var window2 = new AddUnitView { DataContext = viewModel2 };
                    viewModel2.Model.OnClose += window2.Close;
                    window2.ShowDialog();
                    break;
                default:
                    break;
            }
        }

        private void Finish()
        {
            OnClose?.Invoke();
        }

        /*private Action<object> Cancel()
        {
            var act = new Action<object>(obj => { isVisible = false; });
            return act;
        }*/

        MainOrganization mainOrganization;

        private void Query()
        {
            Organizations.Clear();
            mainOrganization = new MainOrganization
            {
                Description = "Главные организации",
                Items = new ObservableCollection<Organization>(
                from org in OrganizationsWrapper.CurrentTable().Table.AsEnumerable()
                where org.Field<string>("f_is_basic").ToUpper() == "Y" &
                    org.Field<int?>("f_syn_id") == 0
                select new Organization
                {
                    Description = org.Field<string>("f_full_org_name"),
                    Id = org.Field<int>("f_org_id"),
                    Items = new ObservableCollection<Department>(
                        from department in DepartmentWrapper
                            .CurrentTable().Table.AsEnumerable()
                        where department.Field<int>("f_org_id") == org.Field<int>("f_org_id")
                        select new Department
                        {
                            Description = department.Field<string>("f_dep_name"),
                            Id = department.Field<int>("f_dep_id"),
                            Items = new ObservableCollection<Unit>(
                                from unit in DepartmentSectionWrapper
                                    .CurrentTable().Table.AsEnumerable()
                                where unit.Field<int>("f_dep_id") ==
                                    department.Field<int>("f_dep_id")
                                select new Unit
                                {
                                    Description = unit.Field<string>("f_section_name"),
                                    Id = unit.Field<int>("f_section_id")
                                })
                        })
                })
            };
            Organizations.Add(mainOrganization);
        }
    }
}
