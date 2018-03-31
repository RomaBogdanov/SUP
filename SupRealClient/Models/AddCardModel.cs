using System;
using System.Data;
using SupClientConnectionLib;
using SupRealClient.Common.Data;
using SupRealClient.TabsSingleton;

namespace SupRealClient.Models
{
    /// <summary>
    /// Добавление пропуска - модель
    /// </summary>
    class AddCardModel : IAddUpdateCardModel
    {
        public CardData Data { get { return new CardData(); } }

        public event Action OnClose;

        public void Cancel()
        {
            OnClose?.Invoke();
        }

        public void ChangeState()
        {
            throw new NotImplementedException();
        }

        public void Ok(CardData data)
        {
            CardsWrapper cards = CardsWrapper.CurrentTable();
            DataRow row = cards.Table.NewRow();
            row["f_card_num"] = data.CurdNum;
            row["f_create_date"] = data.CreateDate;
            row["f_state_id"] = 1;
            row["f_card_text"] = data.NumMAFW;
            row["f_comment"] = data.Comment;
            row["f_rec_operator"] = Authorizer.AppAuthorizer.Login;
            row["f_rec_date"] = data.CreateDate;
            row["f_lost_date"] = DateTime.MinValue;
            cards.Table.Rows.Add(row);
            Cancel();
        }
    }
}
