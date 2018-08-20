using System;
using System.Data;
using SupClientConnectionLib;
using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;

namespace SupRealClient.Models
{
    /// <summary>
    /// Добавление пропуска - модель
    /// </summary>
    class AddCardModel : IAddUpdateCardModel
    {
        public Card Data { get { return new Card(); } }

        public event Action OnClose;

        public void Cancel()
        {
            OnClose?.Invoke();
        }

        public int? ChangeState()
        {
            throw new NotImplementedException();
        }

        public void Ok(Card data)
        {
            /*CardsWrapper cards = CardsWrapper.CurrentTable();
            DataRow row = cards.Table.NewRow();
            row["f_card_num"] = data.CurdNum;
            row["f_create_date"] = data.CreateDate;
            row["f_state_id"] = 1;
            row["f_card_text"] = data.NumMAFW;
            row["f_comment"] = data.Comment;
            row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
            row["f_rec_date"] = data.CreateDate;
            row["f_lost_date"] = DateTime.MinValue;
            cards.Table.Rows.Add(row);*/
            CardsWrapper.CurrentTable().AddRow(data);
            Cancel();
        }
    }
}
