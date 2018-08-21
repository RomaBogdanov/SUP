using SupClientConnectionLib;
using SupRealClient.EnumerationClasses;
using SupRealClient.Models.Helpers;
using SupRealClient.TabsSingleton;
using System;
using System.Data;

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
                    CurdNum = card.CurdNum,
                    Lost = card.Lost
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
            Cancel(ChangeStateHelper.ChangeState(data));
        }
    }
}
