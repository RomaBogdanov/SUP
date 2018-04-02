using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using SupRealClient.TabsSingleton;
using SupRealClient.EnumerationClasses;
using SupRealClient.Common.Interfaces;

namespace SupRealClient.Models
{
    class Base1CardsModel : Base1ModelAbstr
    {
        CardsWrapper cardsWrapper = CardsWrapper.CurrentTable();
        SprCardstatesWrapper sprCardstatesWrapper = SprCardstatesWrapper.CurrentTable();
        VisitsWrapper visitsWrapper = VisitsWrapper.CurrentTable();
        NewUserWrapper newUserWrapper = NewUserWrapper.CurrentTable();
        VisitorsWrapper visitorsWrapper = VisitorsWrapper.CurrentTable();

        public Base1CardsModel(IBase1ViewModel viewModel, IWindow parent)
        {
            this.viewModel = viewModel;
            this.parent = parent;
            this.cardsWrapper.OnChanged += this.Query;
            this.sprCardstatesWrapper.OnChanged += this.Query;
            this.visitsWrapper.OnChanged += this.Query;
            this.newUserWrapper.OnChanged += this.Query;
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

        public override void Farther()
        {
            throw new NotImplementedException();
        }

        public override void Searching(string pattern)
        {
            throw new NotImplementedException();
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
                               where c.Field<int>("f_card_id") == v.Field<int>("f_card_id") &
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
                            State = s.Field<string>("f_state_text"),
                            ReceiversName = 
                                (cardsPersons.FirstOrDefault(p => 
                                p.IdCard == c.Field<int>("f_card_id"))?
                                .PersonName.ToString())
                        };
            this.viewModel.Set = cards;
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

        public override IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>();
        }

        class CardsPersons
        {
            public int IdCard { get; set; }
            public string PersonName { get; set; }
        }
    }
}
