using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using SupRealClient.Models.OrganizationStructure;
using SupRealClient.Views;
using SupRealClient.TabsSingleton;
using System.Data;
using SupRealClient.Common;
using System.Collections.Generic;

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
        private string searchingText;
        private List<ModelBase> searchResult = new List<ModelBase>();

        System.Collections.Generic.List<Organization> memOrgs = new System.Collections.Generic.List<Organization>();
        System.Collections.Generic.List<Department> memDeps = new System.Collections.Generic.List<Department>();
        bool IsAddDep = false;

        enum CurrentLevel
        {
            None,
            Organization,
            Department
        }

        public string SearchingText
        {
            get { return this.searchingText; }
            set
            {
                this.searchingText = value;
                OnPropertyChanged("SearchingText");
                Searching(this.searchingText.ToUpper());
            }
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
            AddDepartmentCommand = new RelayCommand(AddDepartment(), (parameter) => DepartmentEnabled);
            EditCommand = new RelayCommand(Edit());
            OkCommand = new RelayCommand(Ok());
            //CancelCommand = new RelayCommand(Cancel());
        }

        public void Next()
        {
            if (searchResult == null || !searchResult.Any())
            {
                return;
            }

            int idx = searchResult.IndexOf(SelectedObject as ModelBase);
            if (idx < searchResult.Count - 1)
            {
                idx++;
            }
            else
            {
                idx = 0;
            }
            SelectedObject = searchResult[idx];
            //searchResult[idx].IsExpanded = true;
            searchResult[idx].IsSelected = true;
        }

        private Action<object> AddDepartment()
        {
            var action = new Action<object>(obj =>
            {
                memIsExpandedIsSelectedState();
                IsAddDep = true;

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
                    memIsExpandedIsSelectedState();
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
            mainOrganization.IsExpanded = true;
            checkIsExpandedState();
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

        private bool Searching(string pattern)
        {
            searchResult = new List<ModelBase>();
            foreach (var mainOrganization in Organizations)
            {
                mainOrganization.IsExpanded = true;
                foreach (var org in mainOrganization.Items)
                {
                    Searching(org, pattern);
                }
            }
            if (searchResult.Any())
            {
                SelectedObject = searchResult[0];
                //searchResult[0].IsExpanded = true;
                searchResult[0].IsSelected = true;
            }
            else
            {
                SelectedObject = null;
            }

            return searchResult.Any();
        }

        private void Searching(Organization org, string pattern)
        {
            org.IsExpanded = true;
            if (CommonHelper.IsSearchConditionMatch(org.Description, pattern))
            {
                searchResult.Add(org);
            }

            foreach (var child in org.Items)
            {
                Searching(child, pattern);
            }
        }

        private void Searching(Department dep, string pattern)
        {
            dep.IsExpanded = true;
            if (CommonHelper.IsSearchConditionMatch(dep.Description, pattern))
            {
                searchResult.Add(dep);
            }

            foreach (var child in dep.Items)
            {
                Searching(child, pattern);
            }
        }

        void checkIsExpandedState()
        {
            Department maxIdDep = null;
            object currSelObj = null;

            foreach (var imainOrganization in Organizations)
            {
                foreach (var iOrg in imainOrganization.Items)
                {
                    FindOrgsIsExpandedIsSelectedState(iOrg, ref currSelObj);
                    CheckExpandedDeps(iOrg.Items, 
                                      ref maxIdDep, ref currSelObj);
                }
            }     

            if (IsAddDep && maxIdDep != null)
            {
                if (currSelObj is ModelBase && !((ModelBase)currSelObj).IsExpanded)
                {
                    ((ModelBase)currSelObj).IsExpanded = true;
                }

                maxIdDep.IsSelected = true;
               
                IsAddDep = false;
            }
        }

        void CheckExpandedDeps(ObservableCollection<Department> oDeps, 
                               ref Department maxIdDep,
                               ref object currSelObj)
        {
            foreach (var iDep in oDeps)
            {
                FindDepsIsExpandedIsSelectedState(iDep, ref currSelObj);
                if (maxIdDep == null || iDep.Id > maxIdDep.Id)
                    maxIdDep = iDep;
                CheckExpandedDeps(iDep.Items, ref maxIdDep, ref currSelObj);
            }            
        }

        void FindOrgsIsExpandedIsSelectedState(Organization oOrg, ref object currSelObj)
        {
            Organization findOrg = memOrgs.Find(x => x.Id == oOrg.Id);
            if (findOrg != null)
            {
                oOrg.IsExpanded = findOrg.IsExpanded;
                oOrg.IsSelected = findOrg.IsSelected;
                if (IsAddDep && oOrg.IsSelected && !oOrg.IsExpanded)
                    currSelObj = oOrg;
            }
        }

        void FindDepsIsExpandedIsSelectedState(Department oDep, ref object currSelObj)
        {
            Department findDep = memDeps.Find(x => x.Id == oDep.Id);
            if (findDep != null)
            {
                oDep.IsExpanded = findDep.IsExpanded;
                oDep.IsSelected = findDep.IsSelected;
                if (IsAddDep && oDep.IsSelected && !oDep.IsExpanded)
                    currSelObj = oDep;
            }
        }

        void memIsExpandedIsSelectedState()
        {
            memOrgs = new System.Collections.Generic.List<Organization>();
            memDeps = new System.Collections.Generic.List<Department>();

            foreach (var imainOrganization in Organizations)
            {
                foreach (var iOrg in imainOrganization.Items)
                {
                    memOrgs.Add(iOrg);
                    GetDeps(iOrg.Items, ref memDeps);
                }
            }
        }

        void GetDeps(ObservableCollection<Department> oDeps, ref System.Collections.Generic.List<Department> memDeps)
        {
            foreach (var iDep in oDeps)
            {
                memDeps.Add(iDep);
                GetDeps(iDep.Items, ref memDeps);
            }
        }
    }
}
