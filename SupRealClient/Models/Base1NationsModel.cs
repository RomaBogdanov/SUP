using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SupRealClient
{
    class Base1NationsModel : Base1ModelAbstr
    {
        public Base1NationsModel(Base1ViewModel viewModel)
        {
            this.viewModel = viewModel;
            CountriesWrapper countriesWrapper = CountriesWrapper.CurrentTable();
            table = countriesWrapper.Table;
            tabConnector = countriesWrapper.Connector;
            tabName = countriesWrapper.Table.TableName;
            countriesWrapper.OnChanged += Query;
            this.Query();
        }

        public override void Add()
        {
            AddItem1View addItem1View = new AddItem1View(new AddItemNationsModel());
            addItem1View.Show();
        }

        public override void Begin()
        {
            if (this.viewModel.Set.Count() > 0)
            {
                this.viewModel.CurrentItem = this.viewModel.Set.First();
                this.viewModel.numItem =
                    (this.viewModel.CurrentItem as Nation).Id;
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
                (this.viewModel.CurrentItem as Nation).Id;
            this.viewModel.SelectedIndex = this.viewModel.Set.Count() - 1;
        }

        public override void EnterCurrentItem(object item)
        {
            this.viewModel.numItem = (item as Nation).Id;
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

        public override void Update()
        {
            AddItem1View addItem1View = new AddItem1View(
                new UpdateItemNationsModel((Nation)this.viewModel.CurrentItem));
            addItem1View.Show();
        }

        private void Query()
        {
            var nations = from nats in table.AsEnumerable()
                            select new Nation()
                            {
                                Id = nats.Field<int>("f_cntr_id"),
                                CountryName = nats.Field<string>("f_cntr_name"),
                                Deleted = nats.Field<string>("f_deleted"),
                                RecDate = nats.Field<DateTime>("f_rec_date"),
                                RecOperator = nats.Field<int>("f_rec_operator")
                            };
            this.viewModel.Set = nations;
            if (viewModel.numItem == -1)
            {
                this.Begin();
            }
            else
            {
                try
                {
                    this.viewModel.CurrentItem = nations.First(
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
