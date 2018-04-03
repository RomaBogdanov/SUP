using System;
using System.Data;
using SupClientConnectionLib;
using SupRealClient.EnumerationClasses;
using SupRealClient.Common.Data;
using SupRealClient.TabsSingleton;

namespace SupRealClient.Models
{
    /// <summary>
    /// Обновление документа - модель
    /// </summary>
    class UpdateItemDocumentsModel : IAddItem1Model
    {
        private Document document;

        public FieldData Data { get { return new FieldData { Field = document.DocName }; } }

        public event Action OnClose;

        public UpdateItemDocumentsModel(Document document)
        {
            this.document = document;
        }

        public void Ok(FieldData data)
        {
            DocumentsWrapper documents = DocumentsWrapper.CurrentTable();
            if (data.Field != "")
            {
                DataRow dataRow = documents.Table.Rows.Find(document.Id);
                dataRow["f_doc_name"] = data.Field;
                dataRow["f_rec_date"] = DateTime.Now;
                dataRow["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
            }
            Cancel();
        }

        public void Cancel()
        {
            OnClose?.Invoke();
        }
    }
}
