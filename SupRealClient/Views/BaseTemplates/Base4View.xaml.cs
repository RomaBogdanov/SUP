using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using SupRealClient.Annotations;
using SupRealClient.TabsSingleton;
using SupRealClient.EnumerationClasses;
using System.Data;
using SupRealClient.Search;
using SupRealClient.Common.Interfaces;
using SupRealClient.Models;
using System.Windows;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для Base4View.xaml
    /// </summary>
    public partial class Base4View : UserControl
    {
        public Base4View()
        {
            InitializeComponent();
        }
    }

    public class Base4ViewModel<T> : INotifyPropertyChanged
    {
        // ==========
        private string searchingText;
        private string okCaption;
        private Visibility zonesVisibility;

        // ==========
        private IBase4Model<T> _model;

        public ICommand Add { get; set; }
        public ICommand Update { get; set; }
        public ICommand Search { get; set; }
        public ICommand Farther { get; set; }
        public ICommand Begin { get; set; }
        public ICommand Prev { get; set; }
        public ICommand Next { get; set; }
        public ICommand End { get; set; }
        public ICommand Ok { get; set; }
        public ICommand Close { get; set; }
        public ICommand Zones { get; set; }

        public IWindow Parent { get; set; }

        public string OkCaption
        {
            get { return okCaption; }
            set
            {
                okCaption = value;
                OnPropertyChanged();
            }
        }

        public Visibility ZonesVisibility
        {
            get { return zonesVisibility; }
            set
            {
                zonesVisibility = value;
                OnPropertyChanged();
            }
        }

        public string SearchingText
        {
            get { return this.searchingText; }
            set
            {
                this.searchingText = value;
                OnPropertyChanged();
                _model?.Searching(this.searchingText.ToUpper());
            }
        }

        public T CurrentItem
        {
            get
            {
                return Model != null ? Model.CurrentItem : default(T);
            }
            set
            {
                if (Model != null && value != null)
                {
                    Model.CurrentItem = value;
                    OnPropertyChanged();
                }
            }
        }

        public DataGridColumn CurrentColumn
        {
            get { return Model != null ? Model.CurrentColumn : null; }
            set
            {
                if (Model != null && value != null)
                {
                    Model.CurrentColumn = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<T> Set
        {
            get { return Model?.Set; }
            set
            {
                if (Model != null) Model.Set = value;
                OnPropertyChanged();
            }
        }

        public int SelectedIndex
        {
            get { return Model != null ? Model.SelectedIndex : default(int); }
            set
            {
                if (Model != null)
                {
                    Model.SelectedIndex = value;
                    OnPropertyChanged();
                }
            }
        }

        public IBase4Model<T> Model
        {
            get { return _model; }
            set
            {
                if (_model != null)
                {
                    _model.OnModelPropertyChanged -= OnPropertyChanged;
                }
                _model = value;
                _model.Parent = Parent;
                _model.OnModelPropertyChanged += OnPropertyChanged;
                OnPropertyChanged();
            }
        }

        public Base4ViewModel()
        {
            Add = new RelayCommand(obj => AddCom());
            Update = new RelayCommand(obj => UpdateCom());
            Search = new RelayCommand(obj => SearchCom());
            Farther = new RelayCommand(obj => FartherCom());
            Begin = new RelayCommand(obj => BeginCom());
            Prev = new RelayCommand(obj => PrevCom());
            Next = new RelayCommand(obj => NextCom());
            End = new RelayCommand(obj => EndCom());
            Close = new RelayCommand(obj => CloseCom());
            Ok = new RelayCommand(obj => OkCom());
            Zones = new RelayCommand(obj => MessageBox.Show("Zones"));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddCom() { this.Model.Add(); }
        private void UpdateCom() { this.Model.Update(); }
        private void SearchCom() { this.Model.Search(); }
        private void FartherCom() { this.Model.Farther(); }
        private void BeginCom()
        {
            this.Model.Begin();
            Reset();
        }
        private void EndCom()
        {
            this.Model.End();
            Reset();
        }
        private void PrevCom()
        {
            this.Model.Prev();
            Reset();
        }
        private void NextCom()
        {
            this.Model.Next();
            Reset();
        }
        private void OkCom()
        {
            this.Model.Ok();
        }
        private void CloseCom()
        {
            this.Model.Close();
        }

        private void Reset()
        {
            SelectedIndex = Model.SelectedIndex;
            CurrentItem = Model.CurrentItem;
        }
    }

    public delegate void ModelPropertyChanged(string property);

    public interface IBase4Model<T>
    {
        event ModelPropertyChanged OnModelPropertyChanged;
        event Action<object> OnClose;

        ObservableCollection<T> Set { get; set; }
        T CurrentItem { get; set; }
        int SelectedIndex { get; set; }
        DataGridColumn CurrentColumn { get; set; }
        IWindow Parent { get; set; }

        void Add();
        void Begin();
        void Ok();
        void Close();
        void End();
        void Farther();
        void Next();
        void Prev();
        void Search();
        void Update();

        void Searching(string pattern);
    }

    public abstract class Base4ModelAbstr<T> : IBase4Model<T>, ISearchHelper
    {
        public event ModelPropertyChanged OnModelPropertyChanged;
        public event Action<object> OnClose;

        public IWindow Parent { get; set; }

        protected ObservableCollection<T> set;
        protected T currentItem;
        protected int selectedIndex;

        protected SearchResult searchResult = new SearchResult();
        public DataGridColumn CurrentColumn {get;set;}

        public virtual ObservableCollection<T> Set
        {
            get { return set; }
            set
            {
                set = value;
                OnModelPropertyChanged?.Invoke("Set");
            }
        }

        public virtual T CurrentItem
        {
            get { return currentItem; }
            set { currentItem = value; }
        }

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; }
        }

        public virtual void Begin()
        {
            if (Set.Count > 0)
            {
                SelectedIndex = 0;
                CurrentItem = Set[SelectedIndex];
            }
            else
            {
                SelectedIndex = -1;
            }
        }
        public virtual void End()
        {
            if (Set.Count > 0)
            {
                SelectedIndex = Set.Count - 1;
                CurrentItem = Set[SelectedIndex];
            }
            else
            {
                SelectedIndex = -1;
            }
        }
        public virtual void Prev()
        {
            if (Set.Count > 0)
            {
                if (SelectedIndex > 0)
                {
                    SelectedIndex--;
                    CurrentItem = Set[SelectedIndex];
                }
            }
            else
            {
                SelectedIndex = -1;
            }
        }
        public virtual void Next()
        {
            if (Set.Count > 0)
            {
                if (SelectedIndex < Set.Count - 1)
                {
                    SelectedIndex++;
                    CurrentItem = Set[SelectedIndex];
                }
            }
            else
            {
                SelectedIndex = -1;
            }
        }
        public abstract void Add();
        public virtual void Farther()
        {
            SetAt(searchResult.Next());
        }
        public virtual void Search()
        {
            ViewManager.Instance.Search(this, Parent);
        }
        public abstract void Update();
        public void Ok()
        {
            OnClose?.Invoke(GetResult());
        }
        public virtual void Close()
        {
            OnClose?.Invoke(null);
        }
        protected abstract BaseModelResult GetResult();

        protected void Query()
        {
            int oldIndex = SelectedIndex;

            DoQuery();

            if (oldIndex >= 0 && oldIndex < Set.Count - 1)
            {
                SelectedIndex = oldIndex;
                CurrentItem = Set[SelectedIndex];
            }
            else if (Set.Count > 0)
            {
                CurrentItem = Set[0];
            }
            OnModelPropertyChanged?.Invoke("CurrentItem");
        }

        protected abstract void DoQuery();

        public virtual DataRow[] Rows { get { return Table.AsEnumerable().ToArray(); } }

        public virtual IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>();
        }

        public virtual long GetId(int index)
        {
            return -1;
        }

        public virtual void SetAt(long id)
        {
            for (int i = 0; i < Set.Count(); i++)
            {
                if ((Set.ElementAt(i) as IdEntity).Id == id)
                {
                    CurrentItem = Set.ElementAt(i);
                    OnModelPropertyChanged?.Invoke("CurrentItem");
                    break;
                }
            }
        }

        public virtual void Searching(string pattern)
        {
            searchResult = new SearchResult();
            if (CurrentColumn == null ||
                !GetColumns().ContainsKey(CurrentColumn.SortMemberPath))
            {
                return;
            }
            string path = GetColumns()[CurrentColumn.SortMemberPath];
            for (int i = 0; i < Rows.Length; i++)
            {
                object obj = Rows[i].Field<object>(path);
                if (obj != null && obj.ToString().ToUpper().Contains(pattern))
                {
                    searchResult.Add(GetId(i));
                }
            }
            SetAt(searchResult.Begin());
        }

        protected abstract DataTable Table { get; }

        protected virtual IDictionary<string, string> GetColumns()
        {
            return new Dictionary<string, string>();
        }
    }

    public class OrganizationsListModel<T> : Base4ModelAbstr<T>
        where T : Organization, new()
    {
        public OrganizationsListModel()
        {
            OrganizationsWrapper.CurrentTable().OnChanged += Query;
            Query();
            Begin();
        }

        #region BtnHandlers

        public override void Add()
        {
            ViewManager.Instance.AddObject(new AddOrgsModel(), Parent);
        }

        public override void Update()
        {
            ViewManager.Instance.UpdateObject(new UpdateOrgsModel(CurrentItem), Parent);
        }

        #endregion

        protected override BaseModelResult GetResult()
        {
            return new BaseModelResult { Id = CurrentItem.Id, Name = CurrentItem.FullName };
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
                from orgs in OrganizationsWrapper.CurrentTable().Table.AsEnumerable()
                where orgs.Field<int>("f_org_id") != 0
                select new T
                {
                    Id = orgs.Field<int>("f_org_id"),
                    Type = orgs.Field<string>("f_org_type"),
                    FullName = orgs.Field<int>("f_syn_id") == 0 ? "" :
                        OrganizationsWrapper.CurrentTable().Table.AsEnumerable().
                        FirstOrDefault(arg => arg.Field<int>("f_org_id") == 
                        orgs.Field<int>("f_syn_id"))["f_full_org_name"].ToString(),
                    Name = orgs.Field<string>("f_org_name"),
                    Comment = orgs.Field<string>("f_comment"),
                    CountryId = orgs.Field<int>("f_cntr_id"),
                    Country = orgs.Field<int>("f_cntr_id") == 0 ? 
                        "" : CountriesWrapper.CurrentTable()
                        .Table.AsEnumerable().FirstOrDefault(
                        arg => arg.Field<int>("f_cntr_id") ==
                        orgs.Field<int>("f_cntr_id"))["f_cntr_name"].ToString(),
                    RegionId = orgs.Field<int>("f_region_id"),
                    Region = RegionsWrapper.CurrentTable().Table
                        .AsEnumerable().FirstOrDefault(
                        arg => arg.Field<int>("f_region_id") ==
                        orgs.Field<int>("f_region_id"))["f_region_name"].ToString()
                });
        }

        public override long GetId(int index)
        {
            return Rows[index].Field<int>("f_org_id");
        }

        public override DataRow[] Rows
        {
            get
            {
                return (from orgs in Table.AsEnumerable()
                        where orgs.Field<int>("f_org_id") != 0 select orgs).
                        AsEnumerable().ToArray();
            }
        }

        public override IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>()
            {
                { "f_org_type", "Тип" },
                { "f_full_org_name", "Основное название" },
                { "f_org_name", "Название организации" },
                { "f_comment", "Примечание" },
            };
        }

        protected override DataTable Table
        {
            get
            {
                return OrganizationsWrapper.CurrentTable().Table;
            }
        }

        protected override IDictionary<string, string> GetColumns()
        {
            return new Dictionary<string, string>()
            {
                { "Type", "f_org_type" },
                { "FullName", "f_full_org_name"},
                { "Name", "f_org_name" },
                { "Comment", "f_comment" },
            };
        }
    }

    public class VisitorsListModel<T> : Base4ModelAbstr<T>
        where T : EnumerationClasses.Visitor, new()
    {
        public VisitorsListModel()
        {
            VisitorsWrapper.CurrentTable().OnChanged += Query;
            OrganizationsWrapper.CurrentTable().OnChanged += Query;
            Query();
            Begin();
        }

        #region BtnHandlers

        public override void Add()
        {
            Visitor.AddVisitorView wind = new Visitor.AddVisitorView();
            wind.Show();
        }

        public override void Farther()
        {
            System.Windows.Forms.MessageBox.Show("Farther");
        }

        public override void Search()
        {
            System.Windows.Forms.MessageBox.Show("Search");
        }

        public override void Update()
        {
            System.Windows.Forms.MessageBox.Show("Update");
        }

        #endregion

        protected override BaseModelResult GetResult()
        {
            return new BaseModelResult { Id = CurrentItem.Id, Name = CurrentItem.Name };
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
                from visitors in VisitorsWrapper.CurrentTable().Table.AsEnumerable()
                where visitors.Field<int>("f_visitor_id") != 0
                select new T
                {
                    Id = visitors.Field<int>("f_visitor_id"),
                    FullName = visitors.Field<string>("f_full_name"),
                    Organization = (string)OrganizationsWrapper.CurrentTable()
                        .Table.AsEnumerable().FirstOrDefault(arg =>
                        arg.Field<int>("f_org_id") == 
                        visitors.Field<int>("f_org_id"))["f_full_org_name"],
                    Comment = visitors.Field<string>("f_vr_text")
                }
                );
        }

        public override long GetId(int index)
        {
            return Rows[index].Field<int>("f_visitor_id");
        }

        protected override DataTable Table
        {
            get
            {
                return VisitorsWrapper.CurrentTable().Table;
            }
        }
    }

    public class NationsListModel<T> : Base4ModelAbstr<T>
        where T : EnumerationClasses.Nation, new()
    {
        public NationsListModel()
        {
            CountriesWrapper.CurrentTable().OnChanged += Query;
            Query();
            Begin();
        }

        public override void Add()
        {
            ViewManager.Instance.Add(new AddItemNationsModel(), Parent);
        }

        public override void Update()
        {
            ViewManager.Instance.Update(new UpdateItemNationsModel(CurrentItem), Parent);
        }

        protected override BaseModelResult GetResult()
        {
            return new BaseModelResult { Id = CurrentItem.Id, Name = CurrentItem.CountryName };
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
    from nats in CountriesWrapper.CurrentTable().Table.AsEnumerable()
    where nats.Field<int>("f_cntr_id") != 0
    select new T
    {
        Id = nats.Field<int>("f_cntr_id"),
        CountryName = nats.Field<string>("f_cntr_name"),
        Deleted = nats.Field<string>("f_deleted"),
        RecDate = nats.Field<DateTime>("f_rec_date"),
        RecOperator = nats.Field<int>("f_rec_operator")
    }
    );
        }

        public override IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>()
            {
                { "f_cntr_name", "Название" }
            };
        }

        public override long GetId(int index)
        {
            return Rows[index].Field<int>("f_cntr_id");
        }

        protected override DataTable Table
        {
            get
            {
                return CountriesWrapper.CurrentTable().Table;
            }
        }

        protected override IDictionary<string, string> GetColumns()
        {
            return new Dictionary<string, string>()
            {
                { "CountryName", "f_cntr_name" },
            };
        }
    }

    public class CabinetsListModel<T> : Base4ModelAbstr<T>
        where T : Cabinet, new()
    {
        public CabinetsListModel()
        {
            CabinetsWrapper.CurrentTable().OnChanged += Query;
            Query();
            Begin();
        }

        public override void Add()
        {
            ViewManager.Instance.OpenWindow(new AddUpdateCabinetView(), Parent);
        }

        public override void Update()
        {
            ViewManager.Instance.OpenWindow(new AddUpdateCabinetView(CurrentItem), Parent);
        }

        protected override BaseModelResult GetResult()
        {
            return new BaseModelResult { Id = CurrentItem.Id, Name = CurrentItem.Descript};
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
    from cabs in CabinetsWrapper.CurrentTable().Table.AsEnumerable()
    where cabs.Field<int>("f_cabinet_id") != 0
    select new T
    {
        Id = cabs.Field<int>("f_cabinet_id"),
        CabNum = cabs.Field<string>("f_cabinet_num"),
        Descript = cabs.Field<string>("f_cabinet_desc"),
        DoorNum = cabs.Field<string>("f_door_num")
    }
    );
        }

        public override IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>()
            {
                { "f_cabinet_desc", "Описание" }
            };
        }

        public override long GetId(int index)
        {
            return Rows[index].Field<int>("f_cabinet_id");
        }

        protected override DataTable Table
        {
            get
            {
                return CabinetsWrapper.CurrentTable().Table;
            }
        }

        protected override IDictionary<string, string> GetColumns()
        {
            return new Dictionary<string, string>()
            {
                { "CabNum", "f_cabinet_num" },
                { "Descript", "f_cabinet_desc" },
                { "DoorNum", "f_door_num" },
            };
        }
    }

    public class DocumentsListModel<T> : Base4ModelAbstr<T>
        where T : Document, new()
    {
        public DocumentsListModel()
        {
            DocumentsWrapper.CurrentTable().OnChanged += Query;
            Query();
            Begin();
        }

        public override void Add()
        {
            ViewManager.Instance.Add(new AddItemDocumentsModel(), Parent);
        }

        public override void Update()
        {
            ViewManager.Instance.Update(new UpdateItemDocumentsModel(CurrentItem), Parent);
        }

        protected override BaseModelResult GetResult()
        {
            return new BaseModelResult { Id = CurrentItem.Id, Name = CurrentItem.DocName };
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
    from docs in DocumentsWrapper.CurrentTable().Table.AsEnumerable()
    where docs.Field<int>("f_doc_id") != 0
    select new T
    {
        Id = docs.Field<int>("f_doc_id"),
        DocName = docs.Field<string>("f_doc_name"),
        Deleted = docs.Field<string>("f_deleted"),
        RecDate = docs.Field<DateTime>("f_rec_date"),
        RecOperator = docs.Field<int>("f_rec_operator")
    }
    );
        }

        public override IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>()
            {
                { "f_doc_name", "Название" }
            };
        }

        public override long GetId(int index)
        {
            return Rows[index].Field<int>("f_doc_id");
        }

        protected override DataTable Table
        {
            get
            {
                return DocumentsWrapper.CurrentTable().Table;
            }
        }

        protected override IDictionary<string, string> GetColumns()
        {
            return new Dictionary<string, string>()
            {
                { "DocName", "f_doc_name" },
            };
        }
    }

    public class RegionsListModel<T> : Base4ModelAbstr<T>
        where T : EnumerationClasses.Region, new()
    {
        public RegionsListModel()
        {
            RegionsWrapper.CurrentTable().OnChanged += Query;
            Query();
            Begin();
        }

        public override void Add()
        {
            ViewManager.Instance.Add(new AddItemRegionsModel(), Parent);
        }

        public override void Update()
        {
            ViewManager.Instance.Update(new UpdateItemRegionsModel(CurrentItem), Parent);
        }

        protected override BaseModelResult GetResult()
        {
            return new BaseModelResult { Id = CurrentItem.Id, Name = CurrentItem.RegionName };
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
                from regs in RegionsWrapper.CurrentTable().Table.AsEnumerable()
                where regs.Field<int>("f_region_id") != 0
                select new T
                {
                    Id = regs.Field<int>("f_region_id"),
                    RegionName = regs.Field<string>("f_region_name"),
                    Deleted = regs.Field<string>("f_deleted"),
                    RecDate = regs.Field<DateTime>("f_rec_date"),
                    RecOperator = regs.Field<int>("f_rec_operator")
                });
        }

        public override IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>()
            {
                { "f_region_name", "Название" }
            };
        }

        public override long GetId(int index)
        {
            return Rows[index].Field<int>("f_region_id");
        }

        protected override DataTable Table
        {
            get
            {
                return RegionsWrapper.CurrentTable().Table;
            }
        }

        protected override IDictionary<string, string> GetColumns()
        {
            return new Dictionary<string, string>()
            {
                { "RegionName", "f_region_name" },
            };
        }
    }

    public class BaseModelResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
