using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using SupRealClient.TabsSingleton;
using SupRealClient.EnumerationClasses;
using System.Data;
using SupRealClient.Models;
using SupRealClient.Common;

namespace SupRealClient.Views
{
    public class DocumentsListModel<T> : Base4ModelAbstr<T>
        where T : Document, new()
    {
        public DocumentsListModel()
        {
            DocumentsWrapper.CurrentTable().OnChanged += Query;
            Query();
            Begin();
        }

        public override void Add()
        {
            ViewManager.Instance.Add(new AddItemDocumentsModel(), Parent);
        }

        public override void Update()
        {
            if (CurrentItem != null)
            {
                ViewManager.Instance.Update(new UpdateItemDocumentsModel(CurrentItem), Parent);
            }
        }

        public override bool Remove()
        {
            if ((from docs in VisitorsDocumentsWrapper.CurrentTable().Table.AsEnumerable()
                 where docs.Field<int>("f_doctype_id") == currentItem.Id &&
                 CommonHelper.NotDeleted(docs)
                 select docs).Any())
            {
                return false;
            }

            DataRow row =
                DocumentsWrapper.CurrentTable().Table.Rows.Find(currentItem.Id);
            row["f_deleted"] = CommonHelper.BoolToString(true);

            return true;
        }

        protected override BaseModelResult GetResult()
        {
            return new BaseModelResult { Id = CurrentItem.Id, Name = CurrentItem.DocName };
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
                from docs in DocumentsWrapper.CurrentTable().Table.AsEnumerable()
                where docs.Field<int>("f_doc_id") != 0 &&
                CommonHelper.NotDeleted(docs)
                select new T
                {
                    Id = docs.Field<int>("f_doc_id"),
                    DocName = docs.Field<string>("f_doc_name"),
                    Deleted = CommonHelper.StringToBool(
                        docs.Field<string>("f_deleted")),
                    RecDate = docs.Field<DateTime>("f_rec_date"),
                    RecOperator = docs.Field<int>("f_rec_operator")
                }
                );
        }

        public override IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>()
            {
                { "f_doc_name", "Название" }
            };
        }

        public override long GetId(int index)
        {
            return Rows[index].Field<int>("f_doc_id");
        }

        protected override DataTable Table
        {
            get
            {
                return DocumentsWrapper.CurrentTable().Table;
            }
        }

        protected override IDictionary<string, string> GetColumns()
        {
            return new Dictionary<string, string>()
            {
                { "DocName", "f_doc_name" },
            };
        }
    }
}
