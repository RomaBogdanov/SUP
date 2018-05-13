using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using SupRealClient.Models.OrganizationStructure;
using SupRealClient.Views;
using SupRealClient.TabsSingleton;
using System.Data;

namespace SupRealClient.ViewModels
{
    public class MainOrganizationViewModel : ViewModelBase
    {
        public event Action<object> OnClose;
        int currentOrg;
        int currentDep;
        int parentDep;
        CurrentLevel currentLevel = CurrentLevel.None;
        string description = "";

        enum CurrentLevel
        {
            None,
            Organization,
            Department
        }

        public object SelectedObject
        {
            get { return _selectedObject; }
            set
            {
                if (value != null)
                {
                    _selectedObject = value;
                    if (value.GetType().GetInterface("IDepartment") != null)
                    {
                        DepartmentEnabled = true;
                        currentDep = ((Department)value).Id;
                        parentDep = (int)DepartmentWrapper.CurrentTable().Table
                            .Rows.Find(currentDep)["f_parent_id"];
                        description = ((Department)value).Description;
                        currentOrg = (int)DepartmentWrapper.CurrentTable().Table
                            .Rows.Find(currentDep)["f_org_id"];
                        currentLevel = CurrentLevel.Department;
                    }
                    else if (value.GetType().GetInterface("IOrganization") != null)
                    {
                        DepartmentEnabled = true;
                        currentOrg = ((Organization)value).Id;
                        currentDep = -1;
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
        public ICommand OkCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public MainOrganizationViewModel()
        {
            OrganizationsWrapper.CurrentTable().OnChanged += Query;
            DepartmentWrapper.CurrentTable().OnChanged += Query;
            Query();
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
                    Model = new AddDepModel(this.currentOrg, this.currentDep)
                };
                var window = new AddDepartmentView { DataContext = viewModel };
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
                default:
                    break;
            }
        }

        private void Finish()
        {
            OnClose?.Invoke(currentLevel != CurrentLevel.Department ? null :
                new BaseModelResult { Id = currentDep, Name = description });
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
                    Description = $"{org.Field<string>("f_org_type")} " +
                        $"{org.Field<string>("f_org_name")}",
                    Id = org.Field<int>("f_org_id"),
                    Items = new ObservableCollection<Department>(
                        from department in DepartmentWrapper
                            .CurrentTable().Table.AsEnumerable()
                        where department.Field<int>("f_org_id") == org.Field<int>("f_org_id") &&
                        department.Field<int>("f_parent_id") == -1
                        select new Department
                        {
                            Id = department.Field<int>("f_dep_id"),
                            ParentId = department.Field<int>("f_parent_id"),
                            Description = department.Field<string>("f_dep_name"),
                            Items = GetItems(department.Field<int>("f_dep_id"))
                        })
                })
            };
            Organizations.Add(mainOrganization);
        }

        private ObservableCollection<Department> GetItems(int parentId)
        {
            return new ObservableCollection<Department>(
                from department in DepartmentWrapper.CurrentTable().
                Table.AsEnumerable()
                where department.Field<int>("f_parent_id") == parentId
                select new Department
                {
                    Id = department.Field<int>("f_dep_id"),
                    ParentId = department.Field<int>("f_parent_id"),
                    Description = department.Field<string>("f_dep_name"),
                    Items = GetItems(department.Field<int>("f_dep_id"))
                });
        }
    }
}
