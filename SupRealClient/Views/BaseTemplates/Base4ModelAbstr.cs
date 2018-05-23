using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using SupRealClient.EnumerationClasses;
using System.Data;
using SupRealClient.Search;
using SupRealClient.Common.Interfaces;
using SupRealClient.Common;
using SupRealClient.TabsSingleton;
using SupRealClient.Models;

namespace SupRealClient.Views
{
    public abstract class Base4ModelAbstr<T> : IBase4Model<T>, ISearchHelper
    {
        public event ModelPropertyChanged OnModelPropertyChanged;
        public event Action<object> OnClose;

        public IWindow Parent { get; set; }

        protected ObservableCollection<T> set;
        protected T currentItem;
        protected int selectedIndex;

        protected SearchResult searchResult = new SearchResult();
        public DataGridColumn CurrentColumn { get; set; }

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
        public virtual void Zones() { }
        public virtual void Watch() { }
        public virtual void RightClick() { }

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

        public virtual bool Searching(string pattern)
        {
            searchResult = new SearchResult();
            if (CurrentColumn == null || string.IsNullOrEmpty(pattern) ||
                !GetColumns().ContainsKey(CurrentColumn.SortMemberPath))
            {
                return false;
            }
            string path = GetColumns()[CurrentColumn.SortMemberPath];
            for (int i = 0; i < Rows.Length; i++)
            {
                object obj = Rows[i].Field<object>(path);
                if (CommonHelper.IsSearchConditionMatch(obj.ToString(), pattern))
                {
                    searchResult.Add(GetId(i));
                }
            }
            SetAt(searchResult.Begin());

            return searchResult.Any();
        }

        protected abstract DataTable Table { get; }

        protected virtual IDictionary<string, string> GetColumns()
        {
            return new Dictionary<string, string>();
        }

    }

    public class ZonesListModel<T> : Base4ModelAbstr<T>
        where T : Zone, new()
    {
        public ZonesListModel()
        {
            CabinetsWrapper.CurrentTable().OnChanged += Query;
            CabinetsZonesWrapper.CurrentTable().OnChanged += Query;
            ZoneTypesWrapper.CurrentTable().OnChanged += Query;
            ZonesWrapper.CurrentTable().OnChanged += Query;
            Query();
            Begin();
        }

        protected override DataTable Table
        {
            get
            { return ZonesWrapper.CurrentTable().Table; }
        }

        public override void Add()
        {
            AddUpdateZoneWindView addUpdateZoneWindView =
                new AddUpdateZoneWindView();
            addUpdateZoneWindView.Show();
        }

        public override void Update()
        {
            if (this.CurrentItem != null)
            {
                AddUpdateZoneWindView addUpdateZoneWindView =
                    new AddUpdateZoneWindView(this.CurrentItem);
                addUpdateZoneWindView.Show();
            }
        }

        protected override void DoQuery()
        {
            var zoneDoors = from cabs in CabinetsWrapper.CurrentTable().Table.AsEnumerable()
                            join cabszns in CabinetsZonesWrapper.CurrentTable().Table.AsEnumerable()
                            on cabs.Field<int>("f_cabinet_id") equals cabszns.Field<int>("f_cabinet_id")
                            where cabszns.Field<int>("f_zone_id") != 0 &&
                            CommonHelper.NotDeleted(cabs)
                            select new
                            {
                                Id = cabszns.Field<int>("f_zone_id"),
                                Door = cabs.Field<string>("f_door_num")
                            };
            var znsDoors = zoneDoors.GroupBy(x => x.Id)
                .Select(g => new
                {
                    g.Key,
                    Door = string
                .Join(", ", g.Select(x => x.Door))
                });

            Set = new ObservableCollection<T>(
                from typezns in ZoneTypesWrapper.CurrentTable().Table.AsEnumerable()
                join zns in ZonesWrapper.CurrentTable().Table.AsEnumerable()
                on typezns.Field<int>("f_zone_type_id") equals zns.Field<int>("f_zone_type_id")
                join zd in znsDoors
                 on zns.Field<int>("f_zone_id") equals zd.Key
                select new T
                {
                    Id = zns.Field<int>("f_zone_id"),
                    ZoneNum = zns.Field<int>("f_zone_num"),
                    Type = typezns.Field<string>("f_zone_type_name"),
                    Name = zns.Field<string>("f_zone_name"),
                    RelatedDoors = zd.Door
                });
        }

        protected override BaseModelResult GetResult()
        {
            return new BaseModelResult { Id = CurrentItem.Id, Name = CurrentItem.Name };
        }

        protected override IDictionary<string, string> GetColumns()
        {
            return new Dictionary<string, string>()
            {
                { "ZoneNum", "f_zone_num" },
                { "Name", "f_zone_name" },
            };
        }
    }

    public class BaseOrganizationsListModel<T> : Base4ModelAbstr<T>
        where T : Organization, new()
    {
        protected override DataTable Table
        {
            get { return OrganizationsWrapper.CurrentTable().Table; }
        }

        public BaseOrganizationsListModel()
        {
            OrganizationsWrapper.CurrentTable().OnChanged += Query;
            Query();
            Begin();
        }

        public override void Add()
        {
            var wind = new AddOrgsListView(new AddBaseOrgsListModel());
            wind.ShowDialog();
        }

        public override void Update()
        {
            OrganizationsWrapper organizations =
                OrganizationsWrapper.CurrentTable();
            DataRow row = organizations.Table.Rows.Find(
                (this.CurrentItem as Organization).Id);
            row["f_is_basic"] = "N";
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
                from orgs in Table.AsEnumerable()
                where orgs.Field<int>("f_org_id") != 0 &
                orgs.Field<string>("f_is_basic")
                .ToString().ToUpper() == "Y" &&
                CommonHelper.NotDeleted(orgs)
                select new T
                {
                    Id = orgs.Field<int>("f_org_id"),
                    Type = orgs.Field<string>("f_org_type"),
                    FullName = OrganizationsHelper.GenerateFullName(
                        orgs.Field<int>("f_org_id")),
                    Name = OrganizationsHelper.UntrimName(
                        orgs.Field<string>("f_org_name")),
                    Comment = orgs.Field<string>("f_comment")
                });
        }

        protected override BaseModelResult GetResult()
        {
            return new BaseModelResult { Id = CurrentItem.Id, Name = CurrentItem.Name };
        }

        public override IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>
            {
                { "f_org_type", "Тип" },
                { "f_full_org_name", "Основное название" }
            };
        }
    }

    public class ChildOrganizationsListModel<T> : Base4ModelAbstr<T>
        where T : Organization, new()
    {
        protected override DataTable Table
        {
            get { return OrganizationsWrapper.CurrentTable().Table; }
        }

        public ChildOrganizationsListModel()
        {
            OrganizationsWrapper.CurrentTable().OnChanged += Query;
            Query();
            Begin();
        }

        public override void Add()
        {
            var wind = new AddOrgsListView(new AddChildOrgsListModel());
            wind.ShowDialog();
        }

        public override void Update()
        {
            OrganizationsWrapper organizations =
                OrganizationsWrapper.CurrentTable();
            DataRow row = organizations.Table.Rows.Find(
                (this.CurrentItem as Organization).Id);
            row["f_has_free_access"] = "N";
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
                from orgs in Table.AsEnumerable()
                where orgs.Field<int>("f_org_id") != 0 &
                orgs.Field<string>("f_has_free_access")
                .ToString().ToUpper() == "Y" &&
                CommonHelper.NotDeleted(orgs)
                select new T
                {
                    Id = orgs.Field<int>("f_org_id"),
                    Type = orgs.Field<string>("f_org_type"),
                    FullName = OrganizationsHelper.GenerateFullName(
                        orgs.Field<int>("f_org_id")),
                    Name = OrganizationsHelper.UntrimName(
                        orgs.Field<string>("f_org_name")),
                    Comment = orgs.Field<string>("f_comment")
                });
        }

        protected override BaseModelResult GetResult()
        {
            return new BaseModelResult { Id = CurrentItem.Id, Name = CurrentItem.Name };
        }

        public override IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>
            {
                { "f_org_type", "Тип" },
                { "f_full_org_name", "Основное название" }
            };
        }
    }

    public class CardsListModel<T> : Base4ModelAbstr<T>
        where T : Card, new()
    {
        protected override DataTable Table => throw new NotImplementedException();

        CardsWrapper cardsWrapper = CardsWrapper.CurrentTable();
        SprCardstatesWrapper sprCardstatesWrapper = SprCardstatesWrapper.CurrentTable();
        VisitsWrapper visitsWrapper = VisitsWrapper.CurrentTable();
        VisitorsWrapper visitorsWrapper = VisitorsWrapper.CurrentTable();

        public CardsListModel()
        {
            cardsWrapper.OnChanged += Query;
            sprCardstatesWrapper.OnChanged += Query;
            visitsWrapper.OnChanged += Query;
            visitorsWrapper.OnChanged += Query;
            Query();
            Begin();
        }

        public override void Add()
        {
            ViewManager.Instance.AddObject(new AddCardModel(), null);
        }

        public override void Update()
        {
            if (this.CurrentItem != null)
            {
                ViewManager.Instance.UpdateObject(new UpdateCardModel((Card)this.CurrentItem), null);
            }
        }

        protected override void DoQuery()
        {
            DateTime d = new DateTime(2000, 1, 1);
            var cardsPersons =
                from c in cardsWrapper.Table.AsEnumerable()
                from v in visitsWrapper.Table.AsEnumerable()
                from p in visitorsWrapper.Table.AsEnumerable()
                where c.Field<int>("f_card_id") != 0 &
                CommonHelper.NotDeleted(c) &
                c.Field<int>("f_card_id") == v.Field<int>("f_card_id") &
                v.Field<int>("f_visitor_id") == p.Field<int>("f_visitor_id") &
                c.Field<int>("f_state_id") == 3 &
                v.Field<int>("f_rec_operator_back") == 0
                select new CardsPersons
                {
                    IdCard = c.Field<int>("f_card_id"),
                    PersonName = p.Field<string>("f_full_name")
                };

            Set = new ObservableCollection<T>(
                from c in cardsWrapper.Table.AsEnumerable()
                join s in sprCardstatesWrapper.Table.AsEnumerable()
                on c.Field<int>("f_state_id") equals s.Field<int>("f_state_id")
                select new T
                {
                    Id = c.Field<int>("f_card_id"),
                    CurdNum = c.Field<int>("f_card_num"),
                    CreateDate = c.Field<DateTime>("f_create_date"),
                    ChangeDate = c.Field<DateTime>("f_rec_date"),
                    Comment = c.Field<string>("f_comment"),
                    Lost = c.Field<DateTime?>("f_lost_date") > d
                        ? c.Field<DateTime?>("f_lost_date") : null,
                    State = s.Field<string>("f_state_text"),
                    ReceiversName =
                        (cardsPersons.FirstOrDefault(p =>
                        p.IdCard == c.Field<int>("f_card_id"))?
                        .PersonName.ToString())
                });
        }

        protected override BaseModelResult GetResult()
        {
            return new BaseModelResult { Id = CurrentItem.Id, Name = CurrentItem.ReceiversName };
        }

        public class CardsPersons
        {
            public int IdCard { get; set; }
            public string PersonName { get; set; }
        }
    }
}
