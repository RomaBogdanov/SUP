using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using SupRealClient.Annotations;
using SupRealClient.TabsSingleton;
using SupRealClient.EnumerationClasses;
using System.Data;

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
        public T currentItem;
        public int selectedIndex;

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
        public ICommand Close { get; set; }

        public T CurrentItem
        {
            get
            {
                return Model != null ? Model.CurrentItem : default(T);
            }
            set
            {
                if (Model != null)
                {
                    Model.CurrentItem = value;
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
                _model = value;
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
        private void CloseCom() { this.Model.Close(); }

        private void Reset()
        {
            SelectedIndex = Model.SelectedIndex;
            CurrentItem = Model.CurrentItem;
        }
    }

    public interface IBase4Model<T>
    {
        ObservableCollection<T> Set { get; set; }
        T CurrentItem { get; set; }
        int SelectedIndex { get; set; }

        void Add();
        void Begin();
        void Close();
        void End();
        void Farther();
        void Next();
        void Prev();
        void Search();
        void Update();
    }

    public abstract class Base4ModelAbstr<T> : IBase4Model<T>
    {
        protected ObservableCollection<T> set;
        protected T currentItem;
        protected int selectedIndex;

        public virtual ObservableCollection<T> Set
        {
            get { return set; }
            set { set = value; }
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
        public abstract void Farther();
        public abstract void Search();
        public abstract void Update();
        public abstract void Close();

        protected abstract void Query();
    }

    public class OrganizationsListModel<T> : Base4ModelAbstr<T>
        where T : Organization, new()
    {
        public OrganizationsListModel()
        {
            OrganizationsWrapper.CurrentTable().OnChanged += Query;
            Query();
        }

        #region BtnHandlers

        public override void Add()
        {
            throw new NotImplementedException();
        }
        
        public override void Close()
        {
            throw new NotImplementedException();
        }

        public override void Farther()
        {
            throw new NotImplementedException();
        }
        
        public override void Search()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        #endregion

        protected override void Query()
        {
            Set = new ObservableCollection<T>(
                from orgs in OrganizationsWrapper.CurrentTable().Table.AsEnumerable()
                where orgs.Field<int>("f_org_id") != 0
                select new T
                {
                    Id = orgs.Field<int>("f_org_id"),
                    Type = orgs.Field<string>("f_org_type"),
                    FullName = orgs.Field<string>("f_full_org_name"),
                    Name = orgs.Field<string>("f_org_name"),
                    Comment = orgs.Field<string>("f_comment")
                });
            if (Set.Count > 0)
            {
                CurrentItem = Set[0];
            }
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
        }

        #region BtnHandlers

        public override void Add()
        {
            Visitor.AddVisitorView wind = new Visitor.AddVisitorView();
            wind.Show();
        }
        
        public override void Close()
        {
            System.Windows.Forms.MessageBox.Show("Close");
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

        protected override void Query()
        {
            set = new ObservableCollection<T>(
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
            if (Set.Count > 0)
            {
                CurrentItem = Set[0];
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
        }

        public override void Add()
        {
            throw new NotImplementedException();
        }

        public override void Close()
        {
            throw new NotImplementedException();
        }

        public override void Farther()
        {
            throw new NotImplementedException();
        }

        public override void Search()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        protected override void Query()
        {
            Set = new ObservableCollection<T>(
    from nats in CountriesWrapper.CurrentTable().Table.AsEnumerable()
    select new T
    {
        Id = nats.Field<int>("f_cntr_id"),
        CountryName = nats.Field<string>("f_cntr_name"),
        Deleted = nats.Field<string>("f_deleted"),
        RecDate = nats.Field<DateTime>("f_rec_date"),
        RecOperator = nats.Field<int>("f_rec_operator")
    }
    );
            if (Set.Count > 0)
            {
                CurrentItem = Set[0];
            }
        }
    }

    public class CabinetsListModel<T> : Base4ModelAbstr<T>
        where T : Cabinet, new()
    {
        public CabinetsListModel()
        {
            CabinetsWrapper.CurrentTable().OnChanged += Query;
            Query();
        }

        public override void Add()
        {
            throw new NotImplementedException();
        }

        public override void Close()
        {
            throw new NotImplementedException();
        }

        public override void Farther()
        {
            throw new NotImplementedException();
        }

        public override void Search()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        protected override void Query()
        {
            Set = new ObservableCollection<T>(
    from cabs in CabinetsWrapper.CurrentTable().Table.AsEnumerable()
    select new T
    {
        Id = cabs.Field<int>("f_cabinet_id"),
        CabNum = cabs.Field<string>("f_cabinet_num"),
        Descript = cabs.Field<string>("f_cabinet_desc"),
        DoorNum = cabs.Field<string>("f_door_num")
    }
    );
            if (Set.Count > 0)
            {
                CurrentItem = Set[0];
            }
        }
    }

    public class DocumentsListModel<T> : Base4ModelAbstr<T>
        where T : Document, new()
    {
        public DocumentsListModel()
        {
            DocumentsWrapper.CurrentTable().OnChanged += Query;
            Query();
        }

        public override void Add()
        {
            throw new NotImplementedException();
        }

        public override void Close()
        {
            throw new NotImplementedException();
        }

        public override void Farther()
        {
            throw new NotImplementedException();
        }

        public override void Search()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        protected override void Query()
        {
            Set = new ObservableCollection<T>(
    from docs in DocumentsWrapper.CurrentTable().Table.AsEnumerable()
    select new T
    {
        Id = docs.Field<int>("f_doc_id"),
        DocName = docs.Field<string>("f_doc_name"),
        Deleted = docs.Field<string>("f_deleted"),
        RecDate = docs.Field<DateTime>("f_rec_date"),
        RecOperator = docs.Field<int>("f_rec_operator")
    }
    );
            if (Set.Count > 0)
            {
                CurrentItem = Set[0];
            }
        }
    }
}
