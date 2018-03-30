using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SupClientConnectionLib;

namespace SupRealClient
{
    class UpdateCardModel : IAddUpdateCardModel
    {
        private AddUpdateCardViewModel viewModel;
        private Card card;

        public AddUpdateCardViewModel ViewModel
        {
            set
            {
                this.viewModel = value;
                this.viewModel.CurdNum = card.CurdNum;
                this.viewModel.Comment = card.Comment;
                this.viewModel.NumMAFW = card.NumMAFW;
                this.viewModel.CreateDate = card.ChangeDate;
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

        public void Ok()
        {
            CardsWrapper cards = CardsWrapper.CurrentTable();
            DataRow row = cards.Table.Rows.Find(card.Id);
            row["f_card_num"] = card.CurdNum;
            row["f_card_text"] = card.NumMAFW;
            row["f_comment"] = card.Comment;
            row["f_rec_operator"] = Authorizer.AppAuthorizer.Login;
            row["f_rec_date"] = card.ChangeDate;
            Cancel();
        }
    }
}
