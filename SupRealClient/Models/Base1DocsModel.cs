using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using SupRealClient.TabsSingleton;
using SupRealClient.EnumerationClasses;
using SupRealClient.Common.Interfaces;

namespace SupRealClient.Models
{
    class Base1DocsModel : Base1ModelAbstr
    {
        public Base1DocsModel(IBase1ViewModel viewModel, IWindow parent)
        {
            this.viewModel = viewModel;
            this.parent = parent;
            DocumentsWrapper documentsWrapper = DocumentsWrapper.CurrentTable();
            table = documentsWrapper.Table;
            tabConnector = documentsWrapper.Connector;
            tabName = documentsWrapper.Table.TableName;
            documentsWrapper.OnChanged += Query;
            this.Query();
        }

        public override void EnterCurrentItem(object item)
        {
            this.viewModel.NumItem = (item as Document).Id;
        }

        public override void Add()
        {
            ViewManager.Instance.Add(new AddItemDocumentsModel(), parent);
        }

        public override void Update()
        {
            ViewManager.Instance.Update(new UpdateItemDocumentsModel((Document)this.viewModel.CurrentItem), parent);
        }  

        public override void Begin()
        {
            if (this.viewModel.Set.Count() > 0)
            {
                this.viewModel.CurrentItem = this.viewModel.Set.First();
                this.viewModel.NumItem = 
                    (this.viewModel.CurrentItem as Document).Id;
                this.viewModel.SelectedIndex = 0;
            }
            else
            {
                this.viewModel.NumItem = -1;
            }
        }

        public override void End()
        {
            this.viewModel.CurrentItem = this.viewModel.Set.Last();
            this.viewModel.NumItem =
                (this.viewModel.CurrentItem as Document).Id;
            this.viewModel.SelectedIndex = this.viewModel.Set.Count() - 1;
        }

        protected override void Query()
        {
            var documents = from docs in table.AsEnumerable()
                            where docs.Field<int>("f_doc_id") != 0
                            select new Document()
                            {
                                Id = docs.Field<int>("f_doc_id"),
                                DocName = docs.Field<string>("f_doc_name"),
                                Deleted = docs.Field<string>("f_deleted"),
                                RecDate = docs.Field<DateTime>("f_rec_date"),
                                RecOperator = docs.Field<int>("f_rec_operator")
                            };
            this.viewModel.Set =
                new System.Collections.ObjectModel.ObservableCollection<object>(documents);
            if (viewModel.NumItem == -1)
            {
                this.Begin();
            }
            else
            {
                try
                {
                    this.viewModel.CurrentItem = documents.First(
                        arg => arg.Id == this.viewModel.NumItem);
                }
                catch (Exception)
                {
                    this.Begin();
                }
            }
        }

        public override IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>() { { "f_doc_name", "Название" } };
        }

        public override long GetId(int index)
        {
            return Rows[index].Field<int>("f_doc_id");
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
