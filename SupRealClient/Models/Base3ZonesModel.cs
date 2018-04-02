using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupRealClient.Common.Interfaces;
using System.Data;
using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;

namespace SupRealClient.Models
{
    class Base3ZonesModel : Base3ModelAbstr
    {
        public Base3ZonesModel(IBase1ViewModel viewModel)
        {
            this.viewModel = viewModel;
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
            ViewManager.Instance.Add(new AddItemDocumentsModel());
        }

        public override void Update()
        {
            ViewManager.Instance.Update(new UpdateItemDocumentsModel((Document)this.viewModel.CurrentItem));
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

        public override void Farther()
        {
            //throw new NotImplementedException();
        }

        public override void Searching(string pattern)
        {
            var indSet = this.viewModel.Set
                .Select((arg, index) =>
                new { index, at = (arg as Document).DocName.StartsWith(pattern) });
            this.viewModel.SelectedIndex =
                indSet.FirstOrDefault(arg1 => arg1.at == true) != null ?
                indSet.FirstOrDefault(arg1 => arg1.at == true).index :
                this.viewModel.SelectedIndex;
        }

        protected override void Query()
        {
            var documents = from docs in table.AsEnumerable()
                            select new Document()
                            {
                                Id = docs.Field<int>("f_doc_id"),
                                DocName = docs.Field<string>("f_doc_name"),
                                Deleted = docs.Field<string>("f_deleted"),
                                RecDate = docs.Field<DateTime>("f_rec_date"),
                                RecOperator = docs.Field<int>("f_rec_operator")
                            };
            this.viewModel.Set = documents;
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

        public override void Watch()
        {
            throw new NotImplementedException();
        }
    }
}
