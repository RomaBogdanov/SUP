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
using System.Windows;
using SupClientConnectionLib;

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
                if (!string.IsNullOrEmpty(searchingText))
                {
                    Searching(this.searchingText.ToUpper());
                }
                else if (searchResult.Count > 0)
                {
                    searchResult.Clear();
                }
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
                    DepartmentEnabled = false;
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
        public ICommand CancelCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand FartherCommand { get; set; }

        public MainOrganizationViewModel()
        {
            OrganizationsWrapper.CurrentTable().OnChanged += Query;
            DepartmentWrapper.CurrentTable().OnChanged += Query;
            Query();
            AddDepartmentCommand = new RelayCommand(AddDepartment(), (parameter) => DepartmentEnabled);
            EditCommand = new RelayCommand(Edit(), (parameter) => SelectedObject is Department ? true : false);
            RemoveCommand = new RelayCommand(Remove(), (parameter) => SelectedObject is Department ? true : false);
            FartherCommand = new RelayCommand(Next(), (parameter) => searchResult?.Count>0 ? true : false );
            OkCommand = new RelayCommand(Ok());
            CancelCommand = new RelayCommand(Cancel());

            if (Organizations.Count > 0)
            {
                Organizations[0].IsExpanded = true;
                Organizations[0].IsSelected = true;
            }            
        }
        
        private Action<object> Next()
        {
            var action = new Action<object>(obj =>
            {
                int idx = searchResult.IndexOf(SelectedObject as ModelBase);
                if (idx < searchResult.Count - 1)
                {
                    idx++;
                }
                else if (MessageBox.Show("Поиск завершен. Перейти на первый результат поиска?",
                                         "Поиск",
                                          MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                { 
                        idx = 0;
                }
                SelectedObject = searchResult[idx];
                //searchResult[idx].IsExpanded = true;
                searchResult[idx].IsSelected = true;
            });
            return action;            
        }

        private Action<object> AddDepartment()
        {
            var action = new Action<object>(obj =>
            { 
                var viewModel = new UnitViewModel
                {
                    Model = new AddDepModel(this.currentOrg, this.currentDep),
                    Title = @"Новое подразделение"
                };
                var window = new AddDepartmentView { DataContext = viewModel };
                viewModel.Model.OnClose += window.Close;
                window.ShowDialog();

                SelectItem((viewModel.Model as AddDepModel)?.IdEditedItem ?? -1);
            });
            return action;
        }

        private Action<object> Edit()
        {
            return new Action<object>(obj => Ed());
        }

        private Action<object> Remove()
        {
            return new Action<object>(obj => RemoveCom());
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
                        Model = new EditDepModel(this.currentOrg, this.currentDep),
                        Title = @"Отредактировать подразделение"
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

        void RemoveCom()
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись?",
                "Удаление",
                MessageBoxButton.YesNo, MessageBoxImage.Question) ==
                MessageBoxResult.Yes)
            {
                int parentId = (SelectedObject as Department).ParentId;

                var readDeletedDeps = new ObservableCollection<Department>(
                        from department in DepartmentWrapper
                            .CurrentTable().Table.AsEnumerable()
                        where department.Field<int>("f_dep_id") == this.currentDep 
                        select new Department
                        {
                            Id = department.Field<int>("f_dep_id"),
                            ParentId = department.Field<int>("f_parent_id"),
                            Description = department.Field<string>("f_dep_name"),
                            Items = GetItems(department.Field<int>("f_dep_id"))
                        });

                List<Department> deletedDeps = new List<Department>();
                GetDeps(readDeletedDeps, deletedDeps);
               
                DepartmentWrapper.CurrentTable().OnChanged -= Query;
                foreach (var dep in deletedDeps)
                {
                    DataRow row = DepartmentWrapper.CurrentTable().Table.Rows.Find(dep.Id);
                    row.BeginEdit();
                    //row["f_org_id"] = -1;
                    row["f_parent_id"] = -1;
                    row["f_rec_date"] = DateTime.Now;
                    row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                    row["f_deleted"] = CommonHelper.BoolToString(true);                   
                }           
                DepartmentWrapper.CurrentTable().Table.AcceptChanges();              
                DepartmentWrapper.CurrentTable().OnChanged += Query;
                Query();
                               
                if (parentId == -1)               
                {
                    parentId = memOrgs.Find(x => x.Id == currentOrg)?.Id ?? -1;
                }
                SelectItem(parentId);                            
            }
        }

        private void Finish()
        {
            OnClose?.Invoke(currentLevel != CurrentLevel.Department ? null :
                new BaseModelResult { Id = currentDep, Name = description });
        }

        private Action<object> Cancel()
        {
            var act = new Action<object>(obj => { OnClose?.Invoke(obj); });
            return act;
        }

        MainOrganization mainOrganization;

        private void Query()
        {
            memIsExpandedIsSelectedState();

            Organizations.Clear();
            mainOrganization = new MainOrganization
            {
                Description = "Главные организации",
                Items = new ObservableCollection<Organization>(
                from org in OrganizationsWrapper.CurrentTable().Table.AsEnumerable()
                where org.Field<string>("f_is_basic").ToUpper() == "Y" &&
                      org.Field<int?>("f_syn_id") == 0 &&
                      org.Field<string>("f_deleted") != CommonHelper.BoolToString(true)
                select new Organization
                {
                    Description = $"{org.Field<string>("f_org_type")} " +
                        $"{org.Field<string>("f_org_name")}",
                    Id = org.Field<int>("f_org_id"),
                    IsExpanded = memOrgs.Find(x => x.Id == org.Field<int>("f_org_id"))?.IsExpanded ?? false,
                    IsSelected = memOrgs.Find(x => x.Id == org.Field<int>("f_org_id"))?.IsSelected ?? false,
                    Items = new ObservableCollection<Department>(
                        from department in DepartmentWrapper
                            .CurrentTable().Table.AsEnumerable()
                        where department.Field<int>("f_org_id") == org.Field<int>("f_org_id") &&
                              department.Field<int>("f_parent_id") == -1 &&
                              department.Field<string>("f_deleted") != CommonHelper.BoolToString(true)
                        select new Department
                        {
                            Id = department.Field<int>("f_dep_id"),
                            ParentId = department.Field<int>("f_parent_id"),
                            Description = department.Field<string>("f_dep_name"),
                            IsExpanded = memDeps.Find(x => x.Id == department.Field<int>("f_dep_id"))?.IsExpanded ?? false,
                            IsSelected = memDeps.Find(x => x.Id == department.Field<int>("f_dep_id"))?.IsSelected ?? false,
                            Items = GetItems(department.Field<int>("f_dep_id"))
                        })                    
                })
            };            
            Organizations.Add(mainOrganization);

            mainOrganization.IsExpanded = true;            
        }

        private ObservableCollection<Department> GetItems(int parentId)
        {
            return new ObservableCollection<Department>(
                from department in DepartmentWrapper.CurrentTable().
                Table.AsEnumerable()
                where department.Field<int>("f_parent_id") == parentId &&
                      department.Field<string>("f_deleted") != CommonHelper.BoolToString(true)
                select new Department
                {
                    Id = department.Field<int>("f_dep_id"),
                    ParentId = department.Field<int>("f_parent_id"),
                    Description = department.Field<string>("f_dep_name"),
                    IsExpanded = memDeps.Find(x => x.Id == department.Field<int>("f_dep_id"))?.IsExpanded ?? false,
                    IsSelected = memDeps.Find(x => x.Id == department.Field<int>("f_dep_id"))?.IsSelected ?? false,
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
        
        void memIsExpandedIsSelectedState()
        {
            memOrgs = new System.Collections.Generic.List<Organization>();
            memDeps = new System.Collections.Generic.List<Department>();

            foreach (var imainOrganization in Organizations)
            {
                foreach (var iOrg in imainOrganization.Items)
                {
                    memOrgs.Add(iOrg);
                    GetDeps(iOrg.Items, memDeps);
                }
            }
        }

        void GetDeps(ObservableCollection<Department> oDeps, System.Collections.Generic.List<Department> memDeps)
        {
            foreach (var iDep in oDeps)
            {
                memDeps.Add(iDep);
                GetDeps(iDep.Items, memDeps);
            }
        }

        void SelectItem(int Id)
        {
            memIsExpandedIsSelectedState();

            var dep = (memDeps.Find(o => o.Id == Id) as ModelBase) ?? 
                      (memOrgs.Find(o => o.Id == Id) as ModelBase);
            if (dep != null)
            {
                var selectedItem = (ModelBase)null; 
                if (SelectedObject is Department)
                {
                    selectedItem = memDeps.Find(o => o.Id == (SelectedObject as ModelBase).Id);
                }
                else if (SelectedObject is Organization)
                {
                    selectedItem = memOrgs.Find(o => o.Id == (SelectedObject as ModelBase).Id);
                }               
                if (selectedItem != null)
                {
                    selectedItem.IsExpanded = true;
                }
                dep.IsSelected = true;
                SelectedObject = dep;
            }           
        }
    }
}
