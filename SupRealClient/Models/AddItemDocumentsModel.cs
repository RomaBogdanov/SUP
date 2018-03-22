using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SupClientConnectionLib;

namespace SupRealClient
{
    public class AddItemDocumentsModel : IAddItem1Model
    {
        public event Action OnClose;
        private AddItem1ViewModel viewModel;

        public AddItem1ViewModel ViewModel
        {
            set { this.viewModel = value; }
        }

        public void Ok()
        {
            DocumentsWrapper documents = DocumentsWrapper.CurrentTable();
            if (this.viewModel.Field != "")
            {
                DataRow row = documents.Table.NewRow();
                row["f_doc_name"] = this.viewModel.Field;
                row["f_deleted"] = "N";
                row["f_rec_date"] = DateTime.Now;
                row["f_rec_operator"] = Authorizer.AppAuthorizer.Login;
                documents.Table.Rows.Add(row);
            }
            Cancel();
        }

        public void Cancel()
        {
            OnClose?.Invoke();
        }
    }
}
