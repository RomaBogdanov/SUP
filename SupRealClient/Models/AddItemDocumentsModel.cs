using System;
using System.Data;
using SupClientConnectionLib;
using SupRealClient.Common.Data;
using SupRealClient.TabsSingleton;

namespace SupRealClient.Models
{
    /// <summary>
    /// Добавление документа - модель
    /// </summary>
    public class AddItemDocumentsModel : IAddItem1Model
    {
        public event Action OnClose;

        public FieldData Data { get { return new FieldData(); } }

        public void Ok(FieldData data)
        {
            DocumentsWrapper documents = DocumentsWrapper.CurrentTable();
            if (data.Field != "")
            {
                DataRow row = documents.Table.NewRow();
                row["f_doc_name"] = data.Field;
                row["f_deleted"] = "N";
                row["f_rec_date"] = DateTime.Now;
                row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
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
