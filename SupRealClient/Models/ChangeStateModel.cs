using SupClientConnectionLib;
using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;
using System;
using System.Data;
using System.Linq;

namespace SupRealClient.Models
{
    public class ChangeStateModel
    {
        private Card card;

        public Card Data
        {
            get
            {
                return new Card
                {
                    CardIdHi = card.CardIdHi,
                    CardIdLo = card.CardIdLo,
                    StateId = card.StateId,
                    State = card.State,
                    Name = card.Name,
                    CurdNum = card.CurdNum
                };
            }
        }

        public event Action<object> OnClose;

        public ChangeStateModel(Card card)
        {
            this.card = card;
        }

        public void Remove(int cardIdHi, int cardIdLo)
        {
            CardsExtWrapper cards = CardsExtWrapper.CurrentTable();
            DataRow row = null;
            foreach (DataRow r in cards.Table.Rows)
            {
                if (r.Field<int>("f_object_id_hi") == cardIdHi &&
                    r.Field<int>("f_object_id_lo") == cardIdLo)
                {
                    row = r;
                }
            }
            if (row == null)
            {
                row = cards.Table.NewRow();
                row["f_object_id_hi"] = cardIdHi;
                row["f_object_id_lo"] = cardIdLo;
                row["f_state_id"] = (int)CardState.Active;
                row["f_comment"] = "";
                row["f_create_date"] = DateTime.MinValue;
                row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                row["f_rec_date"] = DateTime.Now;
                row["f_lost_date"] = DateTime.MinValue;
                row["f_last_visit_id"] = 0;
                row["f_deleted"] = "Y";
                cards.Table.Rows.Add(row);
            }
            else
            {
                row.BeginEdit();
                row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                row["f_rec_date"] = DateTime.Now;
                row["f_deleted"] = "Y";
                row.EndEdit();
            }

            Cancel(-1);
        }

        public void Cancel(int stateId)
        {
            OnClose?.Invoke(stateId);
        }

        public void Ok(Card data)
        {
            CardsExtWrapper cards = CardsExtWrapper.CurrentTable();
            DataRow row = null;
            foreach (DataRow r in cards.Table.Rows)
            {
                if (r.Field<int>("f_object_id_hi") == data.CardIdHi &&
                    r.Field<int>("f_object_id_lo") == data.CardIdLo)
                {
                    row = r;
                }
            }
            int prevStateId = -1;
            if (row == null)
            {
                row = cards.Table.NewRow();
                row["f_object_id_hi"] = data.CardIdHi;
                row["f_object_id_lo"] = data.CardIdLo;
                row["f_state_id"] = data.StateId;
                row["f_create_date"] = data.CreateDate;
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
                prevStateId = row.Field<int>("f_state_id");
                row.BeginEdit();
                row["f_state_id"] = data.StateId;
                row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                row["f_rec_date"] = DateTime.Now;
                row["f_deleted"] = "N";
                row.EndEdit();
            }

            if (data.StateId == (int)CardState.Active && prevStateId == (int)CardState.Issued)
            {
                ReturnCard(data);
            }

            foreach (DataRow r in CardAreaWrapper.CurrentTable().Table.Rows)
            {
                if (r.Field<string>("f_deleted") == "N" &&
                    r.Field<int>("f_card_id_hi") == data.CardIdHi &&
                    r.Field<int>("f_card_id_lo") == data.CardIdLo)
                {
                    r.BeginEdit();
                    r["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                    r["f_rec_date"] = DateTime.Now;
                    r["f_deleted"] = "Y";
                    r.EndEdit();
                }
            }

            // TODO - здесь в Andover выгружается пропуск с пустым списком областей доступа
            // Data.CurdNum

            Cancel(data.StateId);
        }

        private void ReturnCard(Card data)
        {
            foreach (DataRow r in VisitsWrapper.CurrentTable().Table.Rows)
            {
                if (r.Field<string>("f_deleted") == "N" &&
                    r.Field<int>("f_card_id_hi") == data.CardIdHi &&
                    r.Field<int>("f_card_id_lo") == data.CardIdLo)
                {
                    r.BeginEdit();
                    r["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                    r["f_rec_date"] = DateTime.Now;
                    r["f_deleted"] = "Y";
                    r.EndEdit();
                }
            }
        }
    }
}
