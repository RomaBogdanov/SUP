using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using SupRealClient.TabsSingleton;
using SupRealClient.EnumerationClasses;
using SupRealClient.Common.Interfaces;
using SupRealClient.Common;

namespace SupRealClient.Models
{
    class Base1CardsModel : Base1ModelAbstr
    {
        CardsWrapper cardsWrapper = CardsWrapper.CurrentTable();
        SprCardstatesWrapper sprCardstatesWrapper = SprCardstatesWrapper.CurrentTable();
        VisitsWrapper visitsWrapper = VisitsWrapper.CurrentTable();
        VisitorsWrapper visitorsWrapper = VisitorsWrapper.CurrentTable();

        private DataRow[] rows;

        public Base1CardsModel(IBase1ViewModel viewModel, IWindow parent)
        {
            this.viewModel = viewModel;
            this.parent = parent;
            //CardsWrapper documentsWrapper = CardsWrapper.CurrentTable();
            //table = documentsWrapper.Table;
            this.cardsWrapper.OnChanged += this.Query;
            this.sprCardstatesWrapper.OnChanged += this.Query;
            this.visitsWrapper.OnChanged += this.Query;
            this.visitorsWrapper.OnChanged += this.Query;
            this.Query();
        }

        public override void Add()
        {
            ViewManager.Instance.AddObject(new AddCardModel(), parent);
        }

        public override void Begin()
        {
            if (this.viewModel.Set.Count() > 0)
            {
                this.viewModel.CurrentItem = this.viewModel.Set.First();
                this.viewModel.NumItem =
                    (this.viewModel.CurrentItem as Card).Id;
                this.viewModel.SelectedIndex = 0;
            }
            else
            {
                this.viewModel.NumItem = -1;
            }
        }

        public override void End()
        {
            this.viewModel.CurrentItem = this.viewModel.Set.Last();
            this.viewModel.NumItem =
                (this.viewModel.CurrentItem as Card).Id;
            this.viewModel.SelectedIndex = this.viewModel.Set.Count() - 1;
        }

        public override void EnterCurrentItem(object item)
        {
            this.viewModel.NumItem = (item as Card).Id;
        }

        public override void Update()
        {
            ViewManager.Instance.UpdateObject(new UpdateCardModel((Card)this.viewModel.CurrentItem), parent);
        }

        protected override void Query()
        {
            DateTime d = new DateTime(2000, 1, 1);
            var cardsPersons = from c in cardsWrapper.Table.AsEnumerable()
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

            var cards = from c in cardsWrapper.Table.AsEnumerable()
                        join s in sprCardstatesWrapper.Table.AsEnumerable()
                        on c.Field<int>("f_state_id") equals s.Field<int>("f_state_id")
                        select new Card()
                        {
                            Id = c.Field<int>("f_card_id"),
                            CurdNum = c.Field<int>("f_card_num"),
                            CreateDate = c.Field<DateTime>("f_create_date"),
                            ChangeDate = c.Field<DateTime>("f_rec_date"),
                            Comment = c.Field<string>("f_comment"),
                            Lost = c.Field<DateTime?>("f_lost_date") > d
                                ? c.Field<DateTime?>("f_lost_date") : null,
                            StateId = c.Field<int>("f_state_id"),
                            State = s.Field<string>("f_state_text"),
                            ReceiversName = 
                                (cardsPersons.FirstOrDefault(p => 
                                p.IdCard == c.Field<int>("f_card_id"))?
                                .PersonName.ToString())
                        };
            this.viewModel.Set =
                new System.Collections.ObjectModel.ObservableCollection<object>(cards);
            if (viewModel.NumItem == -1)
            {
                this.Begin();
            }
            else
            {
                try
                {
                    this.viewModel.CurrentItem = cards.First(
                        arg => arg.Id == this.viewModel.NumItem);
                }
                catch (Exception)
                {
                    this.Begin();
                }
            }
        }

        // TODO - переделать без повторов кода
        public override DataRow[] Rows
        {
            get
            {
                return (from c in cardsWrapper.Table.AsEnumerable()
                        join s in sprCardstatesWrapper.Table.AsEnumerable()
                        on c.Field<int>("f_state_id") equals s.Field<int>("f_state_id")
                        select c).AsEnumerable().ToArray();
            }
        }

        public override IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>()
            {
                { "f_card_num", "Пропуск" },
                { "f_comment", "Примечание" },
                //{ "f_state_text", "Состояние" },
            };
        }

        public override long GetId(int index)
        {
            return Rows[index].Field<int>("f_card_id");
        }

        protected override IDictionary<string, string> GetColumns()
        {
            return new Dictionary<string, string>()
            {
                { "CurdNum", "f_card_num" },
                { "CreateDate", "f_create_date"},
                { "NumMAFW", "f_card_text" },
                { "Comment", "f_comment" },
                { "State", "f_state_id" },
                { "Lost", "f_lost_date" },
                { "ChangeDate", "f_rec_date" },
            };
        }

        public class CardsPersons
        {
            public int IdCard { get; set; }
            public string PersonName { get; set; }
        }
    }
}
