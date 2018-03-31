using System;
using System.Data;
using SupClientConnectionLib;
using SupRealClient.EnumerationClasses;
using SupRealClient.Common.Data;
using SupRealClient.TabsSingleton;

namespace SupRealClient.Models
{
    /// <summary>
    /// Обновление пропуска - модель
    /// </summary>
    class UpdateCardModel : IAddUpdateCardModel
    {
        private Card card;

        public CardData Data
        {
            get
            {
                return new CardData
                {
                    CurdNum = card.CurdNum,
                    Comment = card.Comment,
                    NumMAFW = card.NumMAFW,
                    CreateDate = card.ChangeDate
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

        public void ChangeState()
        {
            throw new NotImplementedException();
        }

        public void Ok(CardData data)
        {
            CardsWrapper cards = CardsWrapper.CurrentTable();
            DataRow row = cards.Table.Rows.Find(card.Id);
            row["f_card_num"] = data.CurdNum;
            row["f_card_text"] = data.NumMAFW;
            row["f_comment"] = data.Comment;
            row["f_rec_operator"] = Authorizer.AppAuthorizer.Login;
            row["f_rec_date"] = data.CreateDate;
            Cancel();
        }
    }
}
