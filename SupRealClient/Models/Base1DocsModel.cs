using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SupClientConnectionLib;
using SupClientConnectionLib.ServiceRef;

namespace SupRealClient
{
    class Base1DocsModel : Base1ModelAbstr
    {
        

        public Base1DocsModel(Base1ViewModel viewModel)
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
            this.viewModel.numItem = (item as Document).Id;
        }

        public override void Add()
        {
            AddItem1View addItem1View = new AddItem1View(new AddItemDocumentsModel());
            addItem1View.Show();
        }

        public override void Update()
        {
            AddItem1View addItem1View = new AddItem1View(
                new UpdateItemDocumentsModel((Document)this.viewModel.CurrentItem));
            addItem1View.Show();
        }  

        public override void Begin()
        {
            if (this.viewModel.Set.Count() > 0)
            {
                this.viewModel.CurrentItem = this.viewModel.Set.First();
                this.viewModel.numItem = 
                    (this.viewModel.CurrentItem as Document).Id;
                this.viewModel.SelectedIndex = 0;
            }
            else
            {
                this.viewModel.numItem = -1;
            }
        }

        public override void End()
        {
            this.viewModel.CurrentItem = this.viewModel.Set.Last();
            this.viewModel.numItem =
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

        private void Query()
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
            if (viewModel.numItem == -1)
            {
                this.Begin();
            }
            else
            {
                try
                {
                    this.viewModel.CurrentItem = documents.First(
                        arg => arg.Id == this.viewModel.numItem);
                }
                catch (Exception)
                {
                    this.Begin();
                }
            }
        }

    }
}
