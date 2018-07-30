using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using SupRealClient.EnumerationClasses;
using System.Data;
using System.Windows.Forms;
using SupClientConnectionLib;
using SupRealClient.Andover;
using SupRealClient.Search;
using SupRealClient.Common.Interfaces;
using SupRealClient.Common;
using SupRealClient.TabsSingleton;
using SupRealClient.Models;
using SupRealClient.Models.AddUpdateModel;
using SupRealClient.ViewModels.AddUpdateViewModel;
using SupRealClient.Views.AddUpdateView;
using System.Collections;

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
        public virtual void Ok()
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

        public virtual bool Remove()
        {
            return true;
        }

        protected abstract BaseModelResult GetResult();

        protected void Query()
        {
            int oldIndex = SelectedIndex;

            int memCount = -1;
            if (Set != null)
                memCount = Set.Count - 1;

            DoQuery();

            if (oldIndex >= 0 && oldIndex < Set.Count - 1 && memCount == Set.Count - 1)
            {
                SelectedIndex = oldIndex;
                CurrentItem = Set[SelectedIndex];
            }
            else if (memCount != Set.Count - 1)
            {                
                CurrentItem = Set[Set.Count - 1];
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
            if (CurrentColumn == null || string.IsNullOrEmpty(pattern))
            {
                return false;
            }

            System.ComponentModel.ICollectionView iSource = System.Windows.Data.CollectionViewSource.GetDefaultView(Set);
            object sortedRows = iSource?.GetType()
                            .GetProperty(@"InternalList", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?
                            .GetValue(iSource, null);

            if (sortedRows != null)
            {
                IEnumerable enumerable = sortedRows as IEnumerable;
                if (enumerable != null)
                {
                    foreach (object element in enumerable)
                    {
                        object obj = element.GetType().GetProperty(CurrentColumn.SortMemberPath)?.GetValue(element, null);
                        if (obj != null && CommonHelper.IsSearchConditionMatch(obj.ToString(), pattern))
                        {
                            object idRow = element.GetType().GetProperty(@"Id")?.GetValue(element, null);
                            if (idRow is int)
                                searchResult.Add((int)idRow);
                        }                     
                    }
                }
            }          
            SetAt(searchResult.Begin());

            return searchResult.Any();
        }

        static object GetPropValue(object src, string propName)
        {
            object oVal = null;
            try
            {
                oVal = src.GetType().GetProperty(propName).GetValue(src, null);
            }
            catch { }

            return oVal;
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
            AddBaseOrgsListModel model = new AddBaseOrgsListModel(null);
            var wind = new AddOrgsListView(model);
            
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
                    Comment = orgs.Field<string>("f_comment"),
                    CountryId = orgs.Field<int>("f_cntr_id"),
                    Country = orgs.Field<int>("f_cntr_id") == 0 ?
                        "" : CountriesWrapper.CurrentTable()
                        .Table.AsEnumerable().FirstOrDefault(
                        arg => arg.Field<int>("f_cntr_id") ==
                        orgs.Field<int>("f_cntr_id"))["f_cntr_name"].ToString(),
                    RegionId = orgs.Field<int>("f_region_id"),
                    Region = orgs.Field<int>("f_region_id") == 0 ?
                        "" : RegionsWrapper.CurrentTable()
                        .Table.AsEnumerable().FirstOrDefault(
                        arg => arg.Field<int>("f_region_id") ==
                        orgs.Field<int>("f_region_id"))["f_region_name"].ToString()
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
                { "Type", "Тип" },
                { "Name", "Название" }
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
            var wind = new AddOrgsListView(new AddChildOrgsListModel(null));
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
                    Comment = orgs.Field<string>("f_comment"),
                    CountryId = orgs.Field<int>("f_cntr_id"),
                    Country = orgs.Field<int>("f_cntr_id") == 0 ?
                        "" : CountriesWrapper.CurrentTable()
                        .Table.AsEnumerable().FirstOrDefault(
                        arg => arg.Field<int>("f_cntr_id") ==
                        orgs.Field<int>("f_cntr_id"))["f_cntr_name"].ToString(),
                    RegionId = orgs.Field<int>("f_region_id"),
                    Region = orgs.Field<int>("f_region_id") == 0 ?
                        "" : RegionsWrapper.CurrentTable()
                        .Table.AsEnumerable().FirstOrDefault(
                        arg => arg.Field<int>("f_region_id") ==
                        orgs.Field<int>("f_region_id"))["f_region_name"].ToString()
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
                { "Type", "Тип" },
                { "Name", "Название" }
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
                where c.Field<int>("f_card_id") != 0 &&
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
                where c.Field<int>("f_card_id") != 0 &&
                CommonHelper.NotDeleted(c)
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

        public override IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>
            {
                { "CurdNum", "Пропуск" },
                { "CreateDate", "Занесён в БД" },
                { "NumMAFW", "№ MAFW" },
                { "Comment", "Примечание" },
                { "State", "Состояние" },
                { "ReceiversName", "Кому выдан" },
                { "Lost", "Утерян" },
                { "ChangeDate", "Изменён" },
            };
        }

        public class CardsPersons
        {
            public int IdCard { get; set; }
            public string PersonName { get; set; }
        }
    }

    public class CardsActiveListModel<T> : CardsListModel<T>
        where T : Card, new()
    {
        private int visitorId;
        private ObservableCollection<Order> orders;

        public CardsActiveListModel(int visitorId, ObservableCollection<Order> orders) : base()
        {
            this.visitorId = visitorId;
            this.orders = orders;
        }

        public override void Ok()
        {
            //base.Ok();
            // todo: обязательно просмотреть ситуацию стандартного использования данной кнопки.
            //MessageBox.Show("Test");
            CardsWrapper cards = CardsWrapper.CurrentTable();
            DataRow row = cards.Table.Rows.Find(CurrentItem.Id);
            row.BeginEdit();
            row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
            row["f_rec_date"] = DateTime.Now;
            row["f_state_id"] = 3;
            row.EndEdit();

            // todo: Добавляем карту и персону в список визитов. Всё под рефакторинг!!!
            DataRow row1 = VisitsWrapper.CurrentTable().Table.NewRow();
            row1["f_card_id"] = CurrentItem.Id;
            row1["f_visitor_id"] = visitorId; //todo: проставить id визитёра
            row1["f_time_out"] = DateTime.Now; //todo: пока непонятно, что за дата
            row1["f_time_in"] = DateTime.Now; //todo: пока непонятно, что за дата
            row1["f_visit_text"] = "текст"; //todo: пока непонятно, что за текст
            row1["f_date_from"] = DateTime.Now; //todo: пока непонятно, что за дата
            row1["f_date_to"] = DateTime.Now; //todo: пока непонятно, что за дата
            row1["f_order_id"] = 1; //todo: номер заявки, проставить, хотя тут непонятно, потому что карта может выставляться по нескольким заявкам.
            row1["f_rec_date"] = DateTime.Now;
            row1["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
            row1["f_reason"] = "резон"; //todo: пока непонятно, что с полем делать
            row1["f_rec_operator_back"] = 0; //todo: скорее всего, оператор принявший карту обратно
            row1["f_rec_date_back"] = DateTime.MinValue; //todo: скорее всего, время возврата карты обратно
            row1["f_card_status"] = 3; // текущий статус карты
            row1["f_eff_zonen_text"] = "хм"; //todo: вообще непонятно
            VisitsWrapper.CurrentTable().Table.Rows.Add(row1);

            List<CardArea> list = new List<CardArea>();

            // создаём связь: карта - зоны доступа
            foreach (var order in orders)
            {
                var ordels = order.OrderElements.Where(arg => arg.VisitorId == visitorId);
                foreach (var orderElement in ordels)
                {
                    foreach (var orderElementArea in orderElement.Areas)
                    {
                        list.Add(new CardArea
                        {
                            AreaIdHi = orderElementArea.ObjectIdHi,
                            AreaIdLo = orderElementArea.ObjectIdLo,
                            CardIdHi = (int)row["f_card_id_hi"],
                            CardIdLo = (int)row["f_card_id_lo"]
                        }); 
                    }
                }
            }
            list.ForEach(arg =>
            {
                DataRow r = CardAreaWrapper.CurrentTable().Table.NewRow();
                r["f_card_id_hi"] = arg.CardIdHi;
                r["f_card_id_lo"] = arg.CardIdLo;
                r["f_area_id_hi"] = arg.AreaIdHi;
                r["f_area_id_lo"] = arg.AreaIdLo;
                r["f_deleted"] = "N";
                r["f_rec_date"] = DateTime.Now;
                r["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                CardAreaWrapper.CurrentTable().Table.Rows.Add(r);
            });

            Close();
            /*
            // работа с Андовер
            ObservableCollection<Card> set;
            ObservableCollection<AndoverTestViewModel.AccessPointEx> zones;

            zones = new ObservableCollection<AndoverTestViewModel.AccessPointEx>(
                from accpnt in AccessPointsWrapper.CurrentTable().Table.AsEnumerable()
                where accpnt.Field<int>("f_access_point_id") != 0 &&
                      CommonHelper.NotDeleted(accpnt)
                select new AndoverTestViewModel.AccessPointEx
                {
                    Id = accpnt.Field<int>("f_access_point_id"),
                    Name = accpnt.Field<string>("f_access_point_name"),
                    Descript = accpnt.Field<string>("f_access_point_description"),
                    SpaceIn = accpnt.Field<string>("f_access_point_space_in"),
                    SpaceOut = accpnt.Field<string>("f_access_point_space_out"),
                    Path = accpnt.Field<string>("f_access_point_path"),
                });*/
            

            /*CardsWrapper cards = CardsWrapper.CurrentTable();
            DataRow row = cards.Table.Rows.Find(card.Id);
            row.BeginEdit();
            row["f_card_num"] = data.CurdNum;
            row["f_card_text"] = data.NumMAFW;
            row["f_comment"] = data.Comment;
            row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
            row["f_rec_date"] = data.CreateDate;
            row.EndEdit();
            Cancel();*/
        }

        protected override void DoQuery()
        {
            base.DoQuery();
            Set = new ObservableCollection<T>(Set.Where(arg => arg.State.ToUpper() == "АКТИВЕН"));
        }
    }

    public class AreasListModel<T> : Base4ModelAbstr<T>
        where T : Area, new()
    {
        protected override DataTable Table
        { get { return AreasWrapper.CurrentTable().Table; } }

        public AreasListModel()
        {
            AreasWrapper.CurrentTable().OnChanged += Query;
            Query();
            Begin();
        }

        public override void Add()
        {
            AddUpdateAbstrModel model = new AddAreaModel();
            AddUpdateBaseViewModel viewModel = new AddUpdateBaseViewModel
            {
                Model = model
            };
            AddUpdateAreaWindView view = new AddUpdateAreaWindView();
            view.DataContext = viewModel;
            model.OnClose += view.Handling_OnClose;
            view.ShowDialog();
            object res = view.WindowResult;
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
                from areas in Table.AsEnumerable()
                where areas.Field<int>("f_area_id") != 0 &&
                CommonHelper.NotDeleted(areas)
                select new T
                {
                    Id = areas.Field<int>("f_area_id"),
                    Name = areas.Field<string>("f_area_name"),
                    Descript = areas.Field<string>("f_area_descript")
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
                { "Name", "Название" },
                { "Descript", "Описание" }
            };
        }
    }

    public class AreaSpacesListModel<T> : Base4ModelAbstr<T>
    where T : AreaSpace, new()
    {
        protected override DataTable Table
        { get { return AreasSpacesWrapper.CurrentTable().Table; } }

        public AreaSpacesListModel()
        {
            AreasSpacesWrapper.CurrentTable().OnChanged += Query;
            Query();
            Begin();
        }

        public override void Add()
        {
            AddUpdateAbstrModel model = new AddAreaSpaceModel();
            AddUpdateBaseViewModel viewModel = new AddUpdateAreaSpaceViewModel
            {
                Model = model
            };
            AddUpdateAreaSpaceWindView view = new AddUpdateAreaSpaceWindView();
            view.DataContext = viewModel;
            model.OnClose += view.Handling_OnClose;
            view.ShowDialog();
            object res = view.WindowResult;
        }

        public override void Update()
        {
            AddUpdateAbstrModel model = new UpdateAreaSpaceModel(CurrentItem);
            AddUpdateBaseViewModel viewModel = new AddUpdateAreaSpaceViewModel
            {
                Model = model
            };
            AddUpdateAreaSpaceWindView view = new AddUpdateAreaSpaceWindView();
            view.DataContext = viewModel;
            model.OnClose += view.Handling_OnClose;
            view.ShowDialog();
            object res = view.WindowResult;
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
           from arsp in Table.AsEnumerable()
           where arsp.Field<int>("f_area_space_id") != 0
           select new T
           {
               Id = arsp.Field<int>("f_area_space_id"),
               AreaIdHi = arsp.Field<int>("f_area_id_hi"),
               AreaIdLo = arsp.Field<int>("f_area_id_lo"),
               Area = (arsp.Field<int>("f_area_id_hi") == 0 &&
                    arsp.Field<int>("f_area_id_lo") == 0) ?
                    "" : AreasWrapper.CurrentTable()
                    .Table.AsEnumerable().FirstOrDefault(
                    arg => arg.Field<int>("f_object_id_hi") ==
                    arsp.Field<int>("f_area_id_hi") &&
                    arg.Field<int>("f_object_id_lo") ==
                    arsp.Field<int>("f_area_id_lo")
                    )["f_area_name"].ToString(),
               SpaceId = arsp.Field<int>("f_space_id"),
               Space = arsp.Field<int>("f_space_id") == 0 ?
                    "" : SpacesWrapper.CurrentTable()
                    .Table.AsEnumerable().FirstOrDefault(
                    arg => arg.Field<int>("f_space_id") ==
                    arsp.Field<int>("f_space_id"))["f_num_real"].ToString(),
           });
        }

        protected override BaseModelResult GetResult()
        {
            return new BaseModelResult { Id = CurrentItem.Id, Name = CurrentItem.Id.ToString() };
        }
    }

    public class AccessPointsListModel<T> : Base4ModelAbstr<T>
    where T : AccessPoint, new()
    {
        protected override DataTable Table
        { get { return AccessPointsWrapper.CurrentTable().Table; } }

        public AccessPointsListModel()
        {
            AccessPointsWrapper.CurrentTable().OnChanged += Query;
            Query();
            Begin();
        }

        public override void Add()
        {
            AddUpdateAbstrModel model = new AddAccessPointModel();
            AddUpdateBaseViewModel viewModel = new AddUpdateBaseViewModel
            {
                Model = model
            };
            AddUpdateAccessPointWindView view = new AddUpdateAccessPointWindView();
            view.DataContext = viewModel;
            model.OnClose += view.Handling_OnClose;
            view.ShowDialog();
            object res = view.WindowResult;
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
                from accpnt in Table.AsEnumerable()
                where accpnt.Field<int>("f_access_point_id") != 0 &&
                CommonHelper.NotDeleted(accpnt)
                select new T
                {
                    Id = accpnt.Field<int>("f_access_point_id"),
                    Name = accpnt.Field<string>("f_access_point_name"),
                    Descript = accpnt.Field<string>("f_access_point_description"),
                    SpaceIn = accpnt.Field<string>("f_access_point_space_in"),
                    SpaceOut = accpnt.Field<string>("f_access_point_space_out")
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
                { "Name", "Название" },
                { "Descript", "Описание" },
                { "SpaceIn", "Внутреннее помещение" },
                { "SpaceOut", "Внешнее помещение" }
            };
        }
    }

    public class RealKeysListModel<T> : Base4ModelAbstr<T>
    where T : RealKey, new()
    {
        protected override DataTable Table
        { get { return KeysWrapper.CurrentTable().Table; } }

        public RealKeysListModel()
        {
            KeysWrapper.CurrentTable().OnChanged += Query;
            Query();
            Begin();
        }

        public override void Add()
        {
            AddUpdateAbstrModel model = new AddRealKeyModel();
            AddUpdateBaseViewModel viewModel = new AddUpdateBaseViewModel
            {
                Model = model
            };
            AddUpdateKeyWindView view = new AddUpdateKeyWindView();
            view.DataContext = viewModel;
            model.OnClose += view.Handling_OnClose;
            view.ShowDialog();
            object res = view.WindowResult;
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
           from keys in Table.AsEnumerable()
           where keys.Field<int>("f_key_id") != 0
           select new T
           {
               Id = keys.Field<int>("f_key_id"),
               Name = keys.Field<string>("f_key_name"),
               Descript = keys.Field<string>("f_key_description")
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
                { "Name", "Название" },
                { "Descript", "Описание" },
                { "DoorId", "Дверь" },
                { "KeyHolderId", "Ключница" },
                { "KeyCaseId", "Пенал" }
            };
        }
    }

    public class SchedulesListModel<T> : Base4ModelAbstr<T>
    where T : Schedule, new()
    {
        protected override DataTable Table
        { get { return SchedulesWrapper.CurrentTable().Table; } }

        public SchedulesListModel()
        {
            SchedulesWrapper.CurrentTable().OnChanged += Query;
            Query();
            Begin();
        }

        public override void Add()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
                from schd in Table.AsEnumerable()
                where schd.Field<int>("f_schedule_id") != 0 &&
                CommonHelper.NotDeleted(schd)
                select new T
                {
                    Id = schd.Field<int>("f_schedule_id"),
                    Name = schd.Field<string>("f_schedule_name"),
                    Descript = schd.Field<string>("f_schedule_description")
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
                { "Name", "Название" },
                { "Descript", "Описание" }
            };
        }
    }

    public class AccessLevelsListModel<T> : Base4ModelAbstr<T>
    where T : AccessLevel, new()
    {
        protected override DataTable Table
        { get { return AccessLevelWrapper.CurrentTable().Table; } }

        public AccessLevelsListModel()
        {
            AccessLevelWrapper.CurrentTable().OnChanged += Query;
            Query();
            Begin();
        }

        public override void Add()
        {
            AddUpdateAbstrModel model = new AddAccessLevelModel();
            AddUpdateBaseViewModel viewModel = new AddUpdateAccessLevelViewModel
            {
                Model = model
            };
            AddUpdateAccessLevelWindView view = new AddUpdateAccessLevelWindView();
            view.DataContext = viewModel;
            model.OnClose += view.Handling_OnClose;
            view.ShowDialog();
            object res = view.WindowResult;
        }

        public override void Update()
        {
            AddUpdateAbstrModel model = new UpdateAccessLevelModel(CurrentItem);
            AddUpdateBaseViewModel viewModel = new AddUpdateAccessLevelViewModel
            {
                Model = model
            };
            AddUpdateAccessLevelWindView view = new AddUpdateAccessLevelWindView();
            view.DataContext = viewModel;
            model.OnClose += view.Handling_OnClose;
            view.ShowDialog();
            object res = view.WindowResult;
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
           from acclev in Table.AsEnumerable()
           where acclev.Field<int>("f_access_level_id") != 0
           select new T
           {
               Id = acclev.Field<int>("f_access_level_id"),
               Name = acclev.Field<string>("f_level_name"),
               AreaIdHi = acclev.Field<int>("f_area_id_hi"),
               AreaIdLo = acclev.Field<int>("f_area_id_lo"),
               Area = (acclev.Field<int>("f_area_id_hi") == 0 &&
                    acclev.Field<int>("f_area_id_lo") == 0) ?
                    "" : AreasWrapper.CurrentTable()
                    .Table.AsEnumerable().FirstOrDefault(
                    arg => arg.Field<int>("f_object_id_hi") ==
                    acclev.Field<int>("f_area_id_hi") &&
                    arg.Field<int>("f_object_id_lo") ==
                    acclev.Field<int>("f_area_id_lo")
                    )["f_area_name"].ToString(),
               ScheduleIdHi = acclev.Field<int>("f_schedule_id_hi"),
               ScheduleIdLo = acclev.Field<int>("f_schedule_id_lo"),
               Schedule = (acclev.Field<int>("f_schedule_id_hi") == 0 &&
                    acclev.Field<int>("f_schedule_id_lo") == 0) ?
                    "" : SchedulesWrapper.CurrentTable()
                    .Table.AsEnumerable().FirstOrDefault(
                    arg => arg.Field<int>("f_object_id_hi") ==
                    acclev.Field<int>("f_schedule_id_hi") &&
                    arg.Field<int>("f_object_id_lo") ==
                    acclev.Field<int>("f_schedule_id_lo")
                    )["f_schedule_name"].ToString(),
               AccessLevelNote = acclev.Field<string>("f_access_level_note")
           });
        }

        protected override BaseModelResult GetResult()
        {
            return new BaseModelResult { Id = CurrentItem.Id, Name = CurrentItem.Id.ToString() };
        }

        public override IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>
            {
                { "Name", "Название" },
                { "AccessLevelNote", "Описание уровня доступа" }
            };
        }
    }

    public class CarsListModel<T> : Base4ModelAbstr<T>
    where T : Car, new()
    {
        protected override DataTable Table
        { get { return CarsWrapper.CurrentTable().Table; } }

        public CarsListModel()
        {
            CarsWrapper.CurrentTable().OnChanged += Query;
            Query();
            Begin();
        }

        public override void Add()
        {
            AddUpdateAbstrModel model = new AddCarModel();
            AddUpdateBaseViewModel viewModel = new AddUpdateBaseViewModel
            {
                Model = model
            };
            AddUpdateCarWindView view = new AddUpdateCarWindView();
            view.DataContext = viewModel;
            model.OnClose += view.Handling_OnClose;
            view.ShowDialog();
            object res = view.WindowResult;
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
           from cars in Table.AsEnumerable()
           where cars.Field<int>("f_car_id") != 0
           select new T
           {
               Id = cars.Field<int>("f_car_id"),
               CarMark = cars.Field<string>("f_car_mark"),
               CarNumber = cars.Field<string>("f_car_number"),
               OrgId = cars.Field<int>("f_org_id"),
               VisitorId = cars.Field<int>("f_visitor_id"),
               Color = cars.Field<string>("f_color")
           });
        }

        protected override BaseModelResult GetResult()
        {
            return new BaseModelResult { Id = CurrentItem.Id, Name = CurrentItem.CarMark };
        }

        public override IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>
            {
                { "CarMark", "Марка" },
                { "CarNumber", "Гос.номер" },
                { "VisitorId", "Заявитель" },
                { "OrgId", "Организация" },
                { "Color", "Цвет" }
            };
        }
    }

    public class EquipmentsListModel<T> : Base4ModelAbstr<T>
    where T : Equipment, new()
    {
        protected override DataTable Table
        { get { return EquipmentWrapper.CurrentTable().Table; } }

        public EquipmentsListModel()
        {
            EquipmentWrapper.CurrentTable().OnChanged += Query;
            Query();
            Begin();
        }

        public override void Add()
        {
            AddUpdateAbstrModel model = new AddEquipmentModel();
            AddUpdateBaseViewModel viewModel = new AddUpdateBaseViewModel
            {
                Model = model
            };
            AddUpdateEquipmentWindView view = new AddUpdateEquipmentWindView();
            view.DataContext = viewModel;
            model.OnClose += view.Handling_OnClose;
            view.ShowDialog();
            object res = view.WindowResult;
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
           from equips in Table.AsEnumerable()
           where equips.Field<int>("f_equip_id") != 0
           select new T
           {
               Id = equips.Field<int>("f_equip_id"),
               Name = equips.Field<string>("f_equip_name"),
               Count = equips.Field<int>("f_equip_count"),
               EquipNum = equips.Field<string>("f_equip_num"),
               Direct = equips.Field<string>("f_direct"),
               From = equips.Field<DateTime>("f_from"),
               To = equips.Field<DateTime>("f_to"),
               OrgId = equips.Field<int>("f_org_id"),
               VisId = equips.Field<int>("f_visitor_id")
           });
        }

        protected override BaseModelResult GetResult()
        {
            return new BaseModelResult { Id = CurrentItem.Id, Name = CurrentItem.Name };
        }

        
    }

    public class KeyCasesListModel<T> : Base4ModelAbstr<T>
        where T : KeyCase, new()
    {
        protected override DataTable Table
        { get { return KeyCasesWrapper.CurrentTable().Table; } }

        public KeyCasesListModel()
        {
            KeyCasesWrapper.CurrentTable().OnChanged += Query;
            Query();
            Begin();
        }

        public override void Add()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
from keyCase in Table.AsEnumerable()
where keyCase.Field<int>("f_key_case_id") != 0
select new T
{
   Id = keyCase.Field<int>("f_key_case_id"),
   InnerCode = keyCase.Field<string>("f_inner_code"),
   KeyHolder = keyCase.Field<string>("f_key_holder_num"),
   CellNum = keyCase.Field<int>("f_cell_num"),
   Descript = keyCase.Field<string>("f_descript")
});
        }

        protected override BaseModelResult GetResult()
        {
            return new BaseModelResult { Id = CurrentItem.Id, Name = CurrentItem.InnerCode };
        }
    }

    public class KeyHoldersListModel<T> : Base4ModelAbstr<T>
        where T : KeyHolder, new()
    {
        protected override DataTable Table
        { get { return KeyHoldersWrapper.CurrentTable().Table; } }

        public KeyHoldersListModel()
        {
            KeyHoldersWrapper.CurrentTable().OnChanged += Query;
            Query();
            Begin();
        }

        public override void Add()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
from keyHolder in Table.AsEnumerable()
where keyHolder.Field<int>("f_key_holder_id") != 0
select new T
{
    Id = keyHolder.Field<int>("f_key_holder_id"),
    KeyHolderNum = keyHolder.Field<string>("f_key_holder_num"),
    Descript = keyHolder.Field<string>("f_descript"),
    Count = keyHolder.Field<int>("f_count")
});
        }

        protected override BaseModelResult GetResult()
        {
            return new BaseModelResult
            {
                Id = CurrentItem.Id,
                Name = CurrentItem.KeyHolderNum
            };
        }
    }
}
