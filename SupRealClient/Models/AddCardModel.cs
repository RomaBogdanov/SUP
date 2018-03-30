using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SupClientConnectionLib;

namespace SupRealClient
{
    class AddCardModel : IAddUpdateCardModel
    {
        private AddUpdateCardViewModel viewModel;

        public AddUpdateCardViewModel ViewModel
        { set => this.viewModel = value; }

        public event Action OnClose;

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
            DataRow row = cards.Table.NewRow();
            row["f_card_num"] = this.viewModel.CurdNum;
            row["f_create_date"] = this.viewModel.CreateDate;
            row["f_state_id"] = 1;
            row["f_card_text"] = this.viewModel.NumMAFW;
            row["f_comment"] = this.viewModel.Comment;
            row["f_rec_operator"] = Authorizer.AppAuthorizer.Login;
            row["f_rec_date"] = this.viewModel.CreateDate;
            row["f_lost_date"] = DateTime.MinValue;
            cards.Table.Rows.Add(row);
            Cancel();
        }
    }
}
