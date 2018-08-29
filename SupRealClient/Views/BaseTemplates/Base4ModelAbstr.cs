using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using SupRealClient.EnumerationClasses;
using System.Data;
using System.Linq;
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
using System.Windows.Data;
using System.ComponentModel;
using System.Windows;
using SupContract;
using MessageBox = System.Windows.Forms.MessageBox;

namespace SupRealClient.Views
{
    public abstract class Base4ModelAbstr<T> : IBase4Model<T>, ISearchHelper
    {
        public event ModelPropertyChanged OnModelPropertyChanged;
        public event Action<object> OnClose;

        public Action ScrollCurrentItem { get; set; }

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
                OnModelPropertyChanged?.Invoke("SetCollection");
            }
        }

        private CollectionView setCollection = null;
        public virtual CollectionView SetCollection
        {
            get
            {
                setCollection = (CollectionView)CollectionViewSource.GetDefaultView(Set);
                if (setCollection != null && CurrentColumn != null)
                {
                    ListSortDirection oSortDirection = ListSortDirection.Ascending;
                    if (CurrentColumn.SortDirection != null)
                        oSortDirection = (ListSortDirection)CurrentColumn.SortDirection;
                    setCollection.SortDescriptions.Clear();
                    setCollection.SortDescriptions.Add(new SortDescription(CurrentColumn.SortMemberPath, oSortDirection));                    
                }
                return setCollection;
            }
        }

        public delegate void ScrollIntoViewDelegateSignature();
        public virtual ScrollIntoViewDelegateSignature ScrollIntoViewCurrentItem { get; set; }

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
        public virtual void Synonyms() { }
        public virtual void Watch() { }
        public virtual void RightClick() { }

        public virtual bool Remove()
        {
            return true;
        }

        protected abstract BaseModelResult GetResult();

        protected void Query()
        {
            ListSortDirection? memSort = CurrentColumn?.SortDirection ?? ListSortDirection.Ascending;
            int oldIndex = SelectedIndex;

            int memCount = -1;
            if (SetCollection != null)
                memCount = SetCollection.Count - 1;

            DoQuery();

            if (CurrentColumn != null)
            {
                CurrentColumn.SortDirection = memSort;
            }

            if (oldIndex >= 0 && oldIndex < SetCollection.Count - 1 && memCount == SetCollection.Count - 1)
            {
                SelectedIndex = oldIndex;
                CurrentItem = (T)SetCollection.GetItemAt(SelectedIndex);
            }
            else if (memCount != SetCollection.Count - 1 && Set.Count > 0)
            {               
                if (SetCollection.MoveCurrentTo(Set[Set.Count - 1]))
                    CurrentItem = (T)SetCollection.CurrentItem;
            }
            else if (SetCollection.Count > 0)
            {                
                CurrentItem = (T)SetCollection.GetItemAt(0);
            }
            else if (SetCollection.Count == 0)
            {
                CurrentItem = default(T);
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
                    if (SetCollection.MoveCurrentTo(Set.ElementAt(i)))
                    {
                        CurrentItem = (T)SetCollection.CurrentItem;
                        OnModelPropertyChanged?.Invoke("CurrentItem");
                        ScrollCurrentItem?.Invoke();
                        break;
                    }
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

            foreach (object element in SetCollection)
            {
                object obj = element.GetType().GetProperty(CurrentColumn.SortMemberPath)?.GetValue(element, null);                
                if (obj != null && CommonHelper.IsSearchConditionMatch(obj.ToString(), pattern))
                {
                    object idRow = element.GetType().GetProperty(@"Id")?.GetValue(element, null);
                    if (idRow is int)
                        searchResult.Add((int)idRow);
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

        public override bool Remove()
        {
            //TODO: доработать функционал проверка/и отмены удаления

            DataRow row =
                ZonesWrapper.CurrentTable().Table.Rows.Find(currentItem.Id);
            row["f_deleted"] = CommonHelper.BoolToString(true);

            return true;
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
            //AddBaseOrgsListModel model = new AddBaseOrgsListModel(null);
            //var wind = new AddOrgsListView(model);            
            //wind.ShowDialog();


            var model = new AddBaseOrganizationsListModel<Organization>(Parent);
            //var viewModel = new Base4ViewModel<Organization>()
            //{
            //    Model = model                
            //};
            //var view = new Base4OrganizationsLargeWindView()
            //{
            //    DataContext = viewModel
            //};            
            var view = new Base4OrganizationsLargeWindView(Visibility.Visible);
            (view.base4.DataContext as Base4ViewModel<Organization>).Model = model;
            //view.base4.butAdd.Visibility = Visibility.Hidden;
            //view.base4.btnEdit.Visibility = Visibility.Hidden;            
            view.Owner = Parent as Window;
            view.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            view.ShowDialog();
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

        public override bool Remove()
        {
            OrganizationsWrapper organizations =
               OrganizationsWrapper.CurrentTable();
            DataRow row = organizations.Table.Rows.Find(
                (this.CurrentItem as Organization).Id);
            row["f_is_basic"] = "N";

            return true;
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
                { "Name", "Название" },
                { "Country", "Страна" },
                { "Region", "Регион" }
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
            //var wind = new AddOrgsListView(new AddChildOrgsListModel(Parent));
            //wind.ShowDialog();

            var model = new AddChildOrganizationsListModel<Organization>(Parent);
            //var viewModel = new Base4ViewModel<Organization>()
            //{
            //    Model = model                
            //};
            //var view = new Base4OrganizationsLargeWindView()
            //{
            //    DataContext = viewModel
            //};            
            var view = new Base4OrganizationsLargeWindView(Visibility.Visible);           
            (view.base4.DataContext as Base4ViewModel<Organization>).Model = model;
            //view.base4.butAdd.Visibility = Visibility.Hidden;
            //view.base4.btnEdit.Visibility = Visibility.Hidden;
            view.Owner = Parent as Window;
            view.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            view.ShowDialog();            
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

        public override bool Remove()
        {
            OrganizationsWrapper organizations =
                OrganizationsWrapper.CurrentTable();
            DataRow row = organizations.Table.Rows.Find(
                (this.CurrentItem as Organization).Id);
            row["f_has_free_access"] = "N";

            return true;
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
                { "Name", "Название" },
                { "Country", "Страна" },
                { "Region", "Регион" }
            };
        }
    }

    public class CardsListModel<T> : Base4ModelAbstr<T>
        where T : Card, new()
    {
        protected override DataTable Table => throw new NotImplementedException();

        CardsWrapper cardsWrapper = CardsWrapper.CurrentTable();
        CardsExtWrapper cardsExtWrapper = CardsExtWrapper.CurrentTable();
        SprCardstatesWrapper sprCardstatesWrapper = SprCardstatesWrapper.CurrentTable();
        VisitsWrapper visitsWrapper = VisitsWrapper.CurrentTable();
        VisitorsWrapper visitorsWrapper = VisitorsWrapper.CurrentTable();

        public CardsListModel()
        {
            cardsWrapper.OnChanged += Query;
            cardsExtWrapper.OnChanged += Query;
            sprCardstatesWrapper.OnChanged += Query;
            visitsWrapper.OnChanged += Query;
            visitorsWrapper.OnChanged += Query;
            Query();
            Begin();
        }

        public override void Add()
        {
            if (MessageBox.Show(
                "Данные будут загружены из Andover. Старые данные будут удалены. Продолжить?",
                    "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                ClientConnector clientConnector = ClientConnector.CurrentConnector;
                if (clientConnector.ImportFromAndover(EAndoverExportItem.Personnel.ToString()))
                {
                    System.Windows.MessageBox.Show("Пропуска были загружены из Andover", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    System.Windows.MessageBox.Show("Загрузка из Andover не удалась!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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
                from ce in cardsExtWrapper.Table.AsEnumerable()
                from v in visitsWrapper.Table.AsEnumerable()
                from p in visitorsWrapper.Table.AsEnumerable()
                where c.Field<int>("f_card_id") != 0 &&
                CommonHelper.NotDeleted(ce) && CommonHelper.NotDeleted(v) &&
                CommonHelper.NotDeleted(p) &&
                c.Field<int>("f_object_id_hi") == v.Field<int>("f_card_id_hi") &&
                c.Field<int>("f_object_id_lo") == v.Field<int>("f_card_id_lo") &&
                v.Field<int>("f_visitor_id") == p.Field<int>("f_visitor_id") &&
                ce.Field<int>("f_state_id") == 3 &&
                v.Field<int>("f_rec_operator_back") == 0
                select new CardsPersons
                {
                    IdCardHi = c.Field<int>("f_object_id_hi"),
                    IdCardLo = c.Field<int>("f_object_id_lo"),
                    PersonName = p.Field<string>("f_full_name")
                };

            var cardsExt = new List<T>(
               from ce in cardsExtWrapper.Table.AsEnumerable()
               join s in sprCardstatesWrapper.Table.AsEnumerable()
               on ce.Field<int>("f_state_id") equals s.Field<int>("f_state_id")
               where ce.Field<int>("f_card_id") != 0 &&
               CommonHelper.NotDeleted(ce)
               select new T
               {
                   CardIdHi = ce.Field<int>("f_object_id_hi"),
                   CardIdLo = ce.Field<int>("f_object_id_lo"),
                   CreateDate = ce.Field<DateTime>("f_create_date"),
                   ChangeDate = ce.Field<DateTime>("f_rec_date"),
                   Comment = ce.Field<string>("f_comment"),
                   Lost = ce.Field<DateTime?>("f_lost_date") > d
                       ? ce.Field<DateTime?>("f_lost_date") : null,
                   State = s.Field<string>("f_state_text"),
                   StateId = ce.Field<int>("f_state_id"),
                   ReceiversName =
                       (cardsPersons.FirstOrDefault(p =>
                       p.IdCardHi == ce.Field<int>("f_object_id_hi") &&
                       p.IdCardLo == ce.Field<int>("f_object_id_lo"))?
                       .PersonName.ToString())
               });

            var states = new Dictionary<int, string>((
               from s in sprCardstatesWrapper.Table.AsEnumerable()
               select new
               {
                   A = s.Field<int>("f_state_id"),
                   B = s.Field<string>("f_state_text"),
               }).ToDictionary(o => o.A, o => o.B));

            Set = new ObservableCollection<T>(
                from c in cardsWrapper.Table.AsEnumerable()
                where c.Field<int>("f_card_id") != 0
                select GetCard(c, cardsExt, states));
        }

	    public override bool Remove()
	    {
		    //TODO: доработать функционал, для проверок отмены удаления

		    //DataRow row =
		    //    CardsWrapper.CurrentTable().Table.Rows.Find(currentItem.Id);
		    DataRow row =
			    CardsExtWrapper.CurrentTable().Table.Rows.Find(currentItem.Id);

		    row["f_deleted"] = CommonHelper.BoolToString(true);

		    return true;
	    }

	    protected override BaseModelResult GetResult()
        {
            return new BaseModelResult { Id = CurrentItem.Id, Name = CurrentItem.ReceiversName };
        }

        public override IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>
            {
                { "Name", "Наименование" },
                { "CurdNum", "Номер" },
                { "CreateDate", "Занесён в БД" },
                { "Comment", "Примечание" },
                { "State", "Состояние" },
                { "ReceiversName", "Кому выдан" },
                { "Lost", "Утерян" },
                { "ChangeDate", "Изменён" },
            };
        }

        private T GetCard(DataRow card, List<T> cardsExt,
            Dictionary<int, string> states)
        {
            T cardExt = cardsExt.FirstOrDefault(ce =>
                card.Field<int>("f_object_id_hi") == ce.CardIdHi &&
                card.Field<int>("f_object_id_lo") == ce.CardIdLo);
            int stateId = cardExt != null ? cardExt.StateId :
                (int)CardState.Active;

            return new T
            {
                Id = card.Field<int>("f_card_id"),
                CardIdHi = card.Field<int>("f_object_id_hi"),
                CardIdLo = card.Field<int>("f_object_id_lo"),
                Name = card.Field<string>("f_card_name"),
                CurdNum = card.Field<int>("f_card_num"),
                CreateDate = cardExt != null ?
                    cardExt.CreateDate : DateTime.MinValue,
                ChangeDate = cardExt != null ?
                    cardExt.ChangeDate : DateTime.MinValue,
                Comment = cardExt != null ?
                    cardExt.Comment : "",
                Lost = cardExt != null ?
                    cardExt.Lost : null,
                StateId = stateId,
                State = states.ContainsKey(stateId) ?
                        states[stateId] : "",
                ReceiversName = cardExt != null ?
                    cardExt.ReceiversName : ""
            };
        }

        public class CardsPersons
        {
            public int IdCardHi { get; set; }
            public int IdCardLo { get; set; }
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
			Card selectedCard = CurrentItem;

			if (selectedCard == null)
			{
				MessageBox.Show("Не выбран пропуск!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			var cardName = GetCardName(selectedCard);
			if (string.IsNullOrEmpty(cardName))
			{
				MessageBox.Show("В базе данных не найдено соответствующей карты!",
					"Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;

			}

			selectedCard.Name = cardName;

            CardsExtWrapper cards = CardsExtWrapper.CurrentTable();
            DataRow row = cards.Table.AsEnumerable().FirstOrDefault(arg =>
                arg.Field<int>("f_object_id_lo") == selectedCard.CardIdLo &&
                arg.Field<int>("f_object_id_hi") == selectedCard.CardIdHi);
            if (row == null)
            {
                row = cards.Table.NewRow();
                row["f_object_id_hi"] = selectedCard.CardIdHi;
                row["f_object_id_lo"] = selectedCard.CardIdLo;
                row["f_state_id"] = 3;
                row["f_create_date"] = DateTime.Now;
                row["f_card_text"] = "";
                row["f_comment"] = "";
                row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                row["f_rec_date"] = DateTime.Now;
                row["f_lost_date"] = DateTime.MinValue;
                row["f_last_visit_id"] = 0;
                row["f_deleted"] = "N";
                cards.Table.Rows.Add(row);
            }
            else
            {
                row.BeginEdit();
                row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                row["f_rec_date"] = DateTime.Now;
                row["f_state_id"] = 3;
                row.EndEdit();
            }

            // todo: Добавляем карту и персону в список визитов. Всё под рефакторинг!!!
            DataRow row1 = VisitsWrapper.CurrentTable().Table.NewRow();
			row1["f_card_id_hi"] = selectedCard.CardIdHi;
			row1["f_card_id_lo"] = selectedCard.CardIdLo;
			row1["f_visitor_id"] = visitorId; //todo: проставить id визитёра
			row1["f_time_out"] = DateTime.Now; //todo: пока непонятно, что за дата
			row1["f_time_in"] = DateTime.Now; //todo: пока непонятно, что за дата
			row1["f_visit_text"] = ""; //todo: пока непонятно, что за текст
			row1["f_date_from"] = DateTime.Now; //todo: пока непонятно, что за дата
			row1["f_date_to"] = DateTime.Now; //todo: пока непонятно, что за дата
			row1["f_order_id"] =
				(orders != null && orders.Count > 0)
					? orders[0].Id
					: 1; //todo: номер заявки, проставить, хотя тут непонятно, потому что карта может выставляться по нескольким заявкам.
			row1["f_orders"] = AndoverEntityListHelper.EntitiesToString(orders);
			row1["f_rec_date"] = DateTime.Now;
			row1["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
			row1["f_deleted"] = "N";
			row1["f_reason"] = "резон"; //todo: пока непонятно, что с полем делать
			row1["f_rec_operator_back"] = 0; //todo: скорее всего, оператор принявший карту обратно
			row1["f_rec_date_back"] = DateTime.MinValue; //todo: скорее всего, время возврата карты обратно
			row1["f_card_status"] = 3; // текущий статус карты
			row1["f_eff_zonen_text"] = "хм"; //todo: вообще непонятно
			VisitsWrapper.CurrentTable().Table.Rows.Add(row1);

            var schedules = new List<Schedule>(
               from r in SchedulesWrapper.CurrentTable().Table.AsEnumerable()
               where r.Field<int>("f_schedule_id") != 0 &&
               CommonHelper.NotDeleted(r)
               select new Schedule
               {
                   Id = r.Field<int>("f_schedule_id"),
                   Name = r.Field<string>("f_schedule_name"),
                   ObjectIdHi = r.Field<int>("f_object_id_hi"),
                   ObjectIdLo = r.Field<int>("f_object_id_lo")
               });

            var areas = new List<Area>(
               from r in AreasWrapper.CurrentTable().Table.AsEnumerable()
               where r.Field<int>("f_area_id") != 0 &&
               CommonHelper.NotDeleted(r)
               select new Area
               {
                   Id = r.Field<int>("f_area_id"),
                   Name = r.Field<string>("f_area_name"),
                   ObjectIdHi = r.Field<int>("f_object_id_hi"),
                   ObjectIdLo = r.Field<int>("f_object_id_lo"),
                   Schedule = ""
               });

            // TODO - новые данные для андовер
            List<Area> areasSchedules = new List<Area>();   // Список областей доступа в паре с расписанием. Для выгрузки в Andover

            List<CardArea> list = new List<CardArea>(); // Список всех областей доступа (только ID), расписание не имеет значения
			foreach (var order in orders)
			{
				var ordels = order.OrderElements.Where(arg => arg.VisitorId == visitorId);
				foreach (var orderElement in ordels)
				{
					foreach (var core in
						AndoverEntityListHelper.StringToTemplatesSchedules(orderElement.TemplateIdList))
					{
                        string schedule = "";
                        int scheduleIdHi = 0;
                        int scheduleIdLo = 0;

                        if (core.ScheduleIdHi != 0 || core.ScheduleIdLo != 0)
                        {
                            var sc = schedules.FirstOrDefault(s => s.ObjectIdHi == core.ScheduleIdHi &&
                                s.ObjectIdLo == core.ScheduleIdLo);
                            if (sc != null)
                            {
                                schedule = sc.Name;
                                scheduleIdHi = core.ScheduleIdHi;
                                scheduleIdLo = core.ScheduleIdLo;
                            }
                        }

                        DataRow template = TemplatesWrapper.CurrentTable().Table.Rows.Find(core.Id);
                        foreach (var areaIds in AndoverEntityListHelper.StringToAndoverEntityIds(
							template.Field<string>("f_template_areas")))
						{
                            var area = areas.FirstOrDefault(a => a.ObjectIdHi == areaIds.Key &&
                                a.ObjectIdLo == areaIds.Value);
                            if (area != null)
                            {
                                area = (Area)area.Clone();
                                area.Schedule = schedule;
                                area.ScheduleIdHi = scheduleIdHi;
                                area.ScheduleIdLo = scheduleIdLo;
                                if (areasSchedules.FirstOrDefault(a =>
                                    a.ObjectIdHi == area.ObjectIdHi &&
                                    a.ObjectIdLo == area.ObjectIdLo &&
                                    a.ScheduleIdHi == area.ScheduleIdHi &&
                                    a.ScheduleIdLo == area.ScheduleIdLo) == null)
                                {
                                    areasSchedules.Add(area);
                                }
                            }

                            if (list.FirstOrDefault(l => l.AreaIdHi == areaIds.Key &&
							                             l.AreaIdLo == areaIds.Value) == null)
							{
								list.Add(new CardArea
								{
									AreaIdHi = areaIds.Key,
									AreaIdLo = areaIds.Value,
									CardIdHi = (int) row1["f_card_id_hi"],
									CardIdLo = (int) row1["f_card_id_lo"]
								});
							}
						}
					}

					foreach (var core in
                        AndoverEntityListHelper.StringToAreasSchedules(orderElement.AreaIdList))
					{
                        var area = areas.FirstOrDefault(a => a.ObjectIdHi == core.ObjectIdHi &&
                               a.ObjectIdLo == core.ObjectIdLo);
                        if (area != null)
                        {
                            area = (Area)area.Clone();
                            if (core.ScheduleIdHi != 0 || core.ScheduleIdLo != 0)
                            {
                                var sc = schedules.FirstOrDefault(s => s.ObjectIdHi == core.ScheduleIdHi &&
                                    s.ObjectIdLo == core.ScheduleIdLo);
                                if (sc != null)
                                {
                                    area.Schedule = sc.Name;
                                    area.ScheduleIdHi = core.ScheduleIdHi;
                                    area.ScheduleIdLo = core.ScheduleIdLo;
                                }
                            }
                            if (areasSchedules.FirstOrDefault(a =>
                                a.ObjectIdHi == area.ObjectIdHi &&
                                a.ObjectIdLo == area.ObjectIdLo &&
                                a.ScheduleIdHi == area.ScheduleIdHi &&
                                a.ScheduleIdLo == area.ScheduleIdLo) == null)
                            {
                                areasSchedules.Add(area);
                            }
                        }

                        if (list.FirstOrDefault(l =>
							    l.AreaIdHi == core.ObjectIdHi &&
							    l.AreaIdLo == core.ObjectIdLo) == null)
						{
							list.Add(new CardArea
							{
								AreaIdHi = core.ObjectIdHi,
								AreaIdLo = core.ObjectIdLo,
								CardIdHi = (int) row1["f_card_id_hi"],
								CardIdLo = (int) row1["f_card_id_lo"]
							});
						}
					}
				}
			}

            // TODO - временно закомментировано
            // К0гда понадобятся данные таблицы vis_card_area, раскомментировать
            /*list.ForEach(arg =>
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
			});*/

            // TODO - здесь выгрузить в Andover
            // Предположительно понадобятся поля:
            // - row["f_card_num"] 
            // - список областей доступа (получить из list)
            // - список расписаний (orderElement.Schedule)
            var schedulesHash = new Dictionary<string,int>();

			foreach (var order in orders)
			{
				foreach (var orderElement in order.OrderElements)
				{
					schedulesHash[orderElement.Schedule] = orderElement.ScheduleId;
				}
			}

			var cAreaSchedules = new List<CAreaSchedule>();
			foreach (var order in orders)
			{
				foreach (var orderElement in order.OrderElements)
				{
					if (orderElement.VisitorId == visitorId)
					{
						var splitAreasId = orderElement.AreaIdList.TrimEnd(';').Split(';');
						for (var i = 0; i < splitAreasId.Length; i++)
						{
							var area = splitAreasId[i].Split(',');
							try
							{
								var area1 = new CAreaSchedule(area[0], area[1], orderElement.ScheduleId);
								area1.AreaName = AreasWrapper.CurrentTable().Table.AsEnumerable().Where(x =>
										x.Field<int>("f_object_id_hi") == area1.AreaIdHi && x.Field<int>("f_object_id_lo") == area1.AreaIdLo).First()
									.Field<string>("f_area_name");
								area1.ScheduleName = orderElement.Schedule;
								cAreaSchedules.Add(area1);
							}
							catch (Exception e)
							{
							}
						}
					}
				}
			}

			//поиск полных дублей.
			for (int i = 0; i < cAreaSchedules.Count; i++)
			{
				for (int j = 0; j < cAreaSchedules.Count; j++)
				{
					if (i != j)
					{
						if (cAreaSchedules[i].Equals(cAreaSchedules[j]) && !cAreaSchedules[i].IsDeletedFull)
						{
							cAreaSchedules[j].IsDeletedFull = true;
						}
					}
				}
			}

			//удаление полных дублей.
			var cAreaSchedulesWithoutFullDubles = new List<CAreaSchedule>();

			foreach (var area in cAreaSchedules)
			{
				if (!area.IsDeletedFull)
				{
					cAreaSchedulesWithoutFullDubles.Add(area);
				}
			}

			////поиск дублей по областям.
			//for (int i = 0; i < cAreaSchedulesWithoutFullDubles.Count; i++)
			//{
			//	for (int j = 0; j < cAreaSchedulesWithoutFullDubles.Count; j++)
			//	{
			//		if (i != j)
			//		{
			//			if (cAreaSchedulesWithoutFullDubles[i].Equals(cAreaSchedulesWithoutFullDubles[j]) && !cAreaSchedulesWithoutFullDubles[i].IsDeletedArea)
			//			{
			//				cAreaSchedulesWithoutFullDubles[j].IsDeletedArea = true;
			//			}
			//		}
			//	}
			//}

			////
			//var listcAreaSchedulesWithSameAreas = new List<CAreaSchedule>();

			//foreach (var area in cAreaSchedulesWithoutFullDubles)
			//{
			//	if (!area.IsDeletedArea)
			//	{
			//		listcAreaSchedulesWithSameAreas.Add(area);
			//	}
			//}



			//Выбор расписания.
			
			List<CAreaSchedule> listAreaScheduleDistincted = cAreaSchedulesWithoutFullDubles
				.GroupBy(x => new {x.AreaIdHi, x.AreaIdLo})
				.Select(g => g.First())
				.ToList();
			
			foreach (var area in listAreaScheduleDistincted)
			{
				var x1 = cAreaSchedulesWithoutFullDubles.Where(x => x.AreaIdHi == area.AreaIdHi && x.AreaIdLo == area.AreaIdLo)
					.Select(x => x.ScheduleName);
				area.SchedulesFromSameCAreaSchedules = x1;
			}

			var cropList  = listAreaScheduleDistincted.Where(item => item.SchedulesFromSameCAreaSchedules.Count() > 1);
			if (cropList.Any())
			{
				var scheduleChoiceWindow = new ScheduleChoiceView(cropList);
				
				if (!scheduleChoiceWindow.ShowDialog().Value)
				{
					return;
				}
			}
			
			var dataForAndover = new List<CAreaScheduleContract>();

			foreach (var areaSchedule in listAreaScheduleDistincted)
			{
				dataForAndover.Add(
					new CAreaScheduleContract
				{
					AreaName = areaSchedule.AreaName,
					ScheduleName = areaSchedule.TestString.ToString(),
					AreaIdHi = areaSchedule.AreaIdHi,
					AreaIdLo = areaSchedule.AreaIdLo,
					SelectedItemIndex = areaSchedule.SelectedItemIndex,
					SchedulesFromSameCAreaSchedules = areaSchedule.SchedulesFromSameCAreaSchedules,
					TestString = areaSchedule.TestString,
				});
			}

			//смена курсора
			Cursor.Current = Cursors.WaitCursor;
			
			var data = new AndoverExportData
			{
				Card = selectedCard.Name,
				SchedulesFromSameCAreaSchedules = dataForAndover,
				IsExtradition = true
			};

			var clientConnector = ClientConnector.CurrentConnector;

			var result = clientConnector.ExportToAndover(data);

			if (result?.Success ?? false)
			{
				Cursor.Current = Cursors.Default;
				var isExtraditionSuccess = result?.ExtraditionSuccess;

				var resultString = isExtraditionSuccess ?? false ? "Пропуск был выгружен в Andover" : 
					"Пропуск был выгружен в Andover.\nОдно или несколько расписаний (областей доступа) отсутствует в выгруженном файле";

				System.Windows.MessageBox.Show(resultString, "Информация",
					MessageBoxButton.OK, MessageBoxImage.Information);
			}
			else
			{
				Cursor.Current = Cursors.Default;
				System.Windows.MessageBox.Show("Выгрузка пропуска в Andover не удалась!", "Ошибка",
					MessageBoxButton.OK, MessageBoxImage.Error);
			}
			
			this.Close();
		}

		private List<string> GetAreasNamesByCardAreas(List<CardArea> cardAreas)
		{
			var result = new List<string>();
			var areasTable = AreasWrapper.CurrentTable();

			foreach (DataRow row in areasTable.Table.Rows)
			{
				foreach (var cardArea in cardAreas)
				{
					if ((int) row["f_object_id_hi"] == cardArea.AreaIdHi && (int) row["f_object_id_lo"] == cardArea.AreaIdLo)
					{

						var areaName = row["f_area_name"] is DBNull ? "" : (string) row["f_area_name"];
						result.Add(areaName);
					}
				}
			}

			return result;

		}

		private string GetCardName(Card card)
		{
			var cardTable = CardsWrapper.CurrentTable();
			foreach (DataRow row in cardTable.Table.Rows)
			{
				if ((int) row["f_object_id_hi"] == card.CardIdHi && (int) row["f_object_id_lo"] == card.CardIdLo)
				{
					var cardName = row["f_card_name"] is DBNull ? null : (string) row["f_card_name"];
					return cardName;
				}
			}

			return null;
		}
		
		private Schedule GetSchedule(Dictionary<string, int> schedulesHash, string selectedSchedule)
		{
			var _schedulesWrapper = SchedulesWrapper.CurrentTable();

			var schedulesList = new List<Schedule>(
				from sched in _schedulesWrapper.Table.AsEnumerable()
				where sched.Field<int>("f_schedule_id") == schedulesHash[selectedSchedule] &&
				      !string.Equals(sched.Field<string>("f_deleted").Trim().ToLower(), "y") &&
				      CommonHelper.NotDeleted(sched)
				select new Schedule
				{
					Id = sched.Field<int>("f_schedule_id"),
					Name = sched.Field<string>("f_schedule_name"),
					Path = sched.Field<string>("f_schedule_path")
				});
			return schedulesList.Count == 0 ? new Schedule() : schedulesList.First();
		}

		protected override void DoQuery()
		{
			base.DoQuery();
			Set = new ObservableCollection<T>(Set.Where(arg => arg.StateId == 1));
		}

		public override bool Remove()
		{
			//TODO: доработать функционал, для проверок отмены удаления

			//DataRow row =
			//    CardsWrapper.CurrentTable().Table.Rows.Find(currentItem.Id);
			//row["f_deleted"] = CommonHelper.BoolToString(true);

			return true;
		}
	}

	public class CardsIssuedListModel<T> : CardsListModel<T>
        where T : Card, new()
    {
        protected override void DoQuery()
        {
            base.DoQuery();
            Set = new ObservableCollection<T>(Set.Where(arg => arg.StateId == 3));
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
            if (MessageBox.Show(
                "Данные будут загружены из Andover. Старые данные будут удалены. Продолжить?",
                "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
	            ClientConnector clientConnector = ClientConnector.CurrentConnector;
	            if (clientConnector.ImportFromAndover(EAndoverExportItem.Areas.ToString()))
	            {
		            System.Windows.MessageBox.Show("Области доступа были загружены из Andover", "Информация",
			            MessageBoxButton.OK, MessageBoxImage.Information);
	            }
	            else
	            {
		            System.Windows.MessageBox.Show("Загрузка областей доступа из Andover не удалась!", "Ошибка",
			            MessageBoxButton.OK, MessageBoxImage.Error);
	            }
			}
        }

        public override void Update()
        {
            AddUpdateAbstrModel model = new UpdateAreaModel(CurrentItem);
            AddUpdateBaseViewModel viewModel = new AddUpdateBaseViewModel
            {
                Model = model,
                Title = @"Редактирование области доступа"
            };
            AddUpdateAreaWindView view = new AddUpdateAreaWindView();
            view.DataContext = viewModel;
            model.OnClose += view.Handling_OnClose;
            view.ShowDialog();
            object res = view.WindowResult;
        }

        protected override void DoQuery()
        {
            var descriptions = new List<T>(
                from areasext in AreasExtWrapper.CurrentTable().Table.AsEnumerable()
                where !string.IsNullOrEmpty(areasext.Field<string>("f_description"))
                select new T
                {
                    ObjectIdHi = areasext.Field<int>("f_object_id_hi"),
                    ObjectIdLo = areasext.Field<int>("f_object_id_lo"),
                    Descript = areasext.Field<string>("f_description")
                });

            Set = new ObservableCollection<T>(
                from areas in Table.AsEnumerable()
                where areas.Field<int>("f_area_id") != 0 &&
                CommonHelper.NotDeleted(areas)
                select new T
                {
                    Id = areas.Field<int>("f_area_id"),
                    Name = areas.Field<string>("f_area_name"),
                    ObjectIdHi = areas.Field<int>("f_object_id_hi"),
                    ObjectIdLo = areas.Field<int>("f_object_id_lo"),
                    Descript = null
                });

            foreach (var desc in descriptions)
            {
                var row = Set.FirstOrDefault(r => r.ObjectIdHi == desc.ObjectIdHi &&
                    r.ObjectIdLo == desc.ObjectIdLo);
                if (row != null)
                {
                    row.Descript = desc.Descript;
                }
            }
        }

        public override bool Remove()
        {
            //TODO: доработать функционал, для проверок отмены удаления

            DataRow row =
                AreasWrapper.CurrentTable().Table.Rows.Find(currentItem.Id);
            row["f_deleted"] = CommonHelper.BoolToString(true);

            return true;
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
                Model = model,
                Title = @"Добавить связь"
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
                Model = model,
                Title = @"Редактирование связи"
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

        public override bool Remove()
        {
            //TODO: доработать функционал, для проверок отмены удаления

            DataRow row =
                AreasSpacesWrapper.CurrentTable().Table.Rows.Find(currentItem.Id);
            row["f_deleted"] = CommonHelper.BoolToString(true);

            return true;
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
            if (MessageBox.Show(
		"Данные будут загружены из Andover. Старые данные будут удалены. Продолжить?",
                "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
	            ClientConnector clientConnector = ClientConnector.CurrentConnector;
	            if (clientConnector.ImportFromAndover(EAndoverExportItem.Doors.ToString()))
	            {
		            System.Windows.MessageBox.Show("Точки доступа были загружены из Andover", "Информация",
			            MessageBoxButton.OK, MessageBoxImage.Information);
	            }
	            else
	            {
		            System.Windows.MessageBox.Show("Загрузка точек доступа из Andover не удалась!", "Ошибка",
			            MessageBoxButton.OK, MessageBoxImage.Error);
	            }
			}
        }

        public override void Update()
        {
            AddUpdateAbstrModel model = new UpdateAccessPointModel(CurrentItem);
            AddUpdateBaseViewModel viewModel = new AddUpdateBaseViewModel
            {
                Model = model,
                Title = @"Редактирование точки доступа"
            };
            AddUpdateAccessPointWindView view = new AddUpdateAccessPointWindView();
            view.DataContext = viewModel;
            model.OnClose += view.Handling_OnClose;
            view.ShowDialog();
            object res = view.WindowResult;
        }

        protected override void DoQuery()
        {
            var descriptions = new List<T>(
                from accpntext in AccessPointsExtWrapper.CurrentTable().Table.AsEnumerable()
                where !string.IsNullOrEmpty(accpntext.Field<string>("f_description"))
                select new T
                {
                    ObjectIdHi = accpntext.Field<int>("f_object_id_hi"),
                    ObjectIdLo = accpntext.Field<int>("f_object_id_lo"),
                    Descript = accpntext.Field<string>("f_description")
                });

            Set = new ObservableCollection<T>(
                from accpnt in Table.AsEnumerable()
                where accpnt.Field<int>("f_access_point_id") != 0 &&
                CommonHelper.NotDeleted(accpnt)
                select new T
                {
                    Id = accpnt.Field<int>("f_access_point_id"),
                    Name = accpnt.Field<string>("f_access_point_name"),
                    SpaceIn = accpnt.Field<string>("f_access_point_space_in"),
                    SpaceOut = accpnt.Field<string>("f_access_point_space_out"),
                    ObjectIdHi = accpnt.Field<int>("f_object_id_hi"),
                    ObjectIdLo = accpnt.Field<int>("f_object_id_lo"),
                    Descript = null
                });

            foreach (var desc in descriptions)
            {
                var row = Set.FirstOrDefault(r => r.ObjectIdHi == desc.ObjectIdHi &&
                    r.ObjectIdLo == desc.ObjectIdLo);
                if (row != null)
                {
                    row.Descript = desc.Descript;
                }
            }
        }

        public override bool Remove()
        {
            //TODO: доработать функционал, для проверок отмены удаления

            DataRow row =
                AccessPointsWrapper.CurrentTable().Table.Rows.Find(currentItem.Id);
            row["f_deleted"] = CommonHelper.BoolToString(true);

            return true;
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
                Model = model,
                Title = @"Добавить ключ"
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

        public override bool Remove()
        {
            //TODO: доработать функционал, для проверок отмены удаления

            DataRow row =
                KeysWrapper.CurrentTable().Table.Rows.Find(currentItem.Id);
            row["f_deleted"] = CommonHelper.BoolToString(true);

            return true;
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
		    if (MessageBox.Show(
				"Данные будут загружены из Andover. Старые данные будут удалены. Продолжить?",
			        "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
		    {
			    ClientConnector clientConnector = ClientConnector.CurrentConnector;
			    if (clientConnector.ImportFromAndover(EAndoverExportItem.Schedules.ToString()))
			    {
				    System.Windows.MessageBox.Show("Расписания были загружены из Andover", "Информация",
					    MessageBoxButton.OK, MessageBoxImage.Information);
			    }
			    else
			    {
				    System.Windows.MessageBox.Show("Загрузка расписаний из Andover не удалась!", "Ошибка",
					    MessageBoxButton.OK, MessageBoxImage.Error);
			    }
			}
	    }

	    public override void Update()
        {
            AddUpdateAbstrModel model = new UpdateScheduleModel(CurrentItem);
            AddUpdateBaseViewModel viewModel = new AddUpdateBaseViewModel
            {
                Model = model,
                Title = @"Редактирование расписания"
            };
            AddUpdateScheduleWindView view = new AddUpdateScheduleWindView();
            view.DataContext = viewModel;
            model.OnClose += view.Handling_OnClose;
            view.ShowDialog();
            object res = view.WindowResult;
        }

        protected override void DoQuery()
        {
            var descriptions = new List<T>(
                from schdext in SchedulesExtWrapper.CurrentTable().Table.AsEnumerable()
                where !string.IsNullOrEmpty(schdext.Field<string>("f_description"))
                select new T
                {
                    ObjectIdHi = schdext.Field<int>("f_object_id_hi"),
                    ObjectIdLo = schdext.Field<int>("f_object_id_lo"),
                    Descript = schdext.Field<string>("f_description")
                });

            Set = new ObservableCollection<T>(
                from schd in Table.AsEnumerable()
                where schd.Field<int>("f_schedule_id") != 0 &&
                CommonHelper.NotDeleted(schd)
                select new T
                {
                    Id = schd.Field<int>("f_schedule_id"),
                    Name = schd.Field<string>("f_schedule_name"),
                    ObjectIdHi = schd.Field<int>("f_object_id_hi"),
                    ObjectIdLo = schd.Field<int>("f_object_id_lo"),
                    Descript = null
                });

            foreach (var desc in descriptions)
            {
                var row = Set.FirstOrDefault(r => r.ObjectIdHi == desc.ObjectIdHi &&
                    r.ObjectIdLo == desc.ObjectIdLo);
                if (row != null)
                {
                    row.Descript = desc.Descript;
                }
            }
        }

        public override bool Remove()
        {
            //TODO: доработать функционал, для проверок отмены удаления

            DataRow row =
                SchedulesWrapper.CurrentTable().Table.Rows.Find(currentItem.Id);
            row["f_deleted"] = CommonHelper.BoolToString(true);

            return true;
        }

        protected override BaseModelResult GetResult()
        {
            return new BaseModelResult
            {
                Id = CurrentItem.Id,
                IdHi = CurrentItem.ObjectIdHi,
                IdLo = CurrentItem.ObjectIdLo,
                Name = CurrentItem.Name
            };
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
                Model = model,
                Title = @"Добавить уровень доступа"
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
                Model = model,
                Title = @"Редактирование уровня доступа"
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

        public override bool Remove()
        {
            //TODO: доработать функционал, для проверок отмены удаления

            DataRow row =
                AccessLevelWrapper.CurrentTable().Table.Rows.Find(currentItem.Id);
            row["f_deleted"] = CommonHelper.BoolToString(true);

            return true;
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
                Model = model,
                Title = @"Добавить транспорт"
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

        public override bool Remove()
        {
            //TODO: доработать функционал, для проверок отмены удаления

            DataRow row =
                CarsWrapper.CurrentTable().Table.Rows.Find(currentItem.Id);
            row["f_deleted"] = CommonHelper.BoolToString(true);

            return true;
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

        public override bool Remove()
        {
            //TODO: доработать функционал, для проверок отмены удаления

            DataRow row =
                EquipmentWrapper.CurrentTable().Table.Rows.Find(currentItem.Id);
            row["f_deleted"] = CommonHelper.BoolToString(true);

            return true;
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

        public override bool Remove()
        {
            //TODO: доработать функционал, для проверок отмены удаления

            DataRow row =
                KeyCasesWrapper.CurrentTable().Table.Rows.Find(currentItem.Id);
            row["f_deleted"] = CommonHelper.BoolToString(true);

            return true;
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

        public override bool Remove()
        {
            //TODO: доработать функционал, для проверок отмены удаления

            DataRow row =
                KeyHoldersWrapper.CurrentTable().Table.Rows.Find(currentItem.Id);
            row["f_deleted"] = CommonHelper.BoolToString(true);

            return true;
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

    public class TemplatesListModel<T> : Base4ModelAbstr<T>
        where T : Template, new()
    {
        protected override DataTable Table
        {
            get { return TemplatesWrapper.CurrentTable().Table; }
        }

        public TemplatesListModel()
        {
            TemplatesWrapper.CurrentTable().OnChanged += Query;
            Query();
            Begin();
        }

        public override void Add()
        {
            AddUpdateAbstrModel model = new AddTemplateModel();
            AddUpdateBaseViewModel viewModel = new AddUpdateTemplateViewModel
            {
                Model = model
            };
            AddUpdateTemplateView view = new AddUpdateTemplateView();
            view.DataContext = viewModel;
            model.OnClose += view.Handling_OnClose;
            view.ShowDialog();
            object res = view.WindowResult;
        }

        public override void Update()
        {
            AddUpdateAbstrModel model = new UpdateTemplateModel(CurrentItem);
            AddUpdateBaseViewModel viewModel = new AddUpdateTemplateViewModel
            {
                Model = model
            };
            AddUpdateTemplateView view = new AddUpdateTemplateView();
            view.DataContext = viewModel;
            model.OnClose += view.Handling_OnClose;
            view.ShowDialog();
            object res = view.WindowResult;
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
                from template in Table.AsEnumerable()
                where template.Field<int>("f_template_id") != 0
                select new T
                {
                    Id = template.Field<int>("f_template_id"),
                    Name = template.Field<string>("f_template_name"),
                    Type = template.Field<int>("f_template_type").ToString(),
                    Descript = template.Field<string>("f_template_description"),
                    AreaIdList = template.Field<string>("f_template_areas"),
                });
        }

        public override bool Remove()
        {
            //TODO: доработать функционал, для проверок отмены удаления

            DataRow row = Table.Rows.Find(currentItem.Id);
            row["f_deleted"] = CommonHelper.BoolToString(true);

            return true;
        }

        protected override BaseModelResult GetResult()
        {
            return new BaseModelResult
            {
                Id = CurrentItem.Id,
                Name = CurrentItem.Name
            };
        }
    }
}
