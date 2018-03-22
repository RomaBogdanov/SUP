using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SupClientConnectionLib;

namespace SupRealClient
{
    class UpdateItemDocumentsModel : IAddItem1Model
    {
        private AddItem1ViewModel viewModel;
        private Document document;

        public AddItem1ViewModel ViewModel
        { set
            {
                this.viewModel = value;
                this.viewModel.Field = document.DocName;
            }
        }

        public event Action OnClose;

        public UpdateItemDocumentsModel(Document document)
        {
            this.document = document;
        }

        public void Ok()
        {
            DocumentsWrapper documents = DocumentsWrapper.CurrentTable();
            if (this.viewModel.Field != "")
            {
                DataRow dataRow = documents.Table.Rows.Find(document.Id);
                dataRow["f_doc_name"] = this.viewModel.Field;
                dataRow["f_rec_date"] = DateTime.Now;
                dataRow["f_rec_operator"] = Authorizer.AppAuthorizer.Login;
            }
            Cancel();
        }

        public void Cancel()
        {
            OnClose?.Invoke();
        }
    }
}
