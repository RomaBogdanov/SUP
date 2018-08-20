﻿using System;
using System.Data;
using SupClientConnectionLib;
using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;
using SupRealClient.Views;

namespace SupRealClient.Models
{
    /// <summary>
    /// Обновление пропуска - модель
    /// </summary>
    class UpdateCardModel : IAddUpdateCardModel
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
                    CurdNum = card.CurdNum,
                    Comment = card.Comment,
                    Name = card.Name,
                    CreateDate = card.ChangeDate,
                    StateId = card.StateId,
                    State = card.State
                };
            }
        }

        public event Action OnClose;

        public UpdateCardModel(Card card)
        {
            this.card = card;
        }

        public void Cancel()
        {
            OnClose?.Invoke();
        }

        public int? ChangeState()
        {
            var view = new ChangeStateView(new ChangeStateModel(card));
            view.ShowDialog();
            return view.WindowResult as int?;
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
                row["f_comment"] = data.Comment;
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
                row["f_comment"] = data.Comment;
                row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                row["f_rec_date"] = DateTime.Now;
                row.EndEdit();
            }
            Cancel();
        }
    }
}
