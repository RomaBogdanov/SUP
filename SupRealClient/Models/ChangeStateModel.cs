using SupClientConnectionLib;
using SupRealClient.EnumerationClasses;
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
                    CurdNum = card.CurdNum
                };
            }
        }

        public event Action<object> OnClose;

        public ChangeStateModel(Card card)
        {
            this.card = card;
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
                row.BeginEdit();
                row["f_state_id"] = data.StateId;
                row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                row["f_rec_date"] = DateTime.Now;
                row["f_deleted"] = "N";
                row.EndEdit();
            }

            // TODO - здесь в Andover выгружается пропуск с пустым списком областей доступа
            // Data.CurdNum

            Cancel(data.StateId);
        }
    }
}
