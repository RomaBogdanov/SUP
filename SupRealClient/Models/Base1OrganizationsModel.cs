using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SupRealClient
{
    class Base1OrganizationsModel : Base1ModelAbstr
    {

        public Base1OrganizationsModel(Base1ViewModel viewModel)
        {
            this.viewModel = viewModel;
            OrganizationsWrapper countriesWrapper = OrganizationsWrapper.CurrentTable();
            table = countriesWrapper.Table;
            tabConnector = countriesWrapper.Connector;
            tabName = countriesWrapper.Table.TableName;
            countriesWrapper.OnChanged += Query;
            this.Query();
        }

        public override void Add()
        {
            AddUpdateOrgsView orgsView = new AddUpdateOrgsView(new AddOrgsModel());
            orgsView.Show();
        }

        public override void Begin()
        {
            if (this.viewModel.Set.Count() > 0)
            {
                this.viewModel.CurrentItem = this.viewModel.Set.First();
                this.viewModel.numItem =
                    (this.viewModel.CurrentItem as Organization).Id;
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
                (this.viewModel.CurrentItem as Organization).Id;
            this.viewModel.SelectedIndex = this.viewModel.Set.Count() - 1;
        }

        public override void EnterCurrentItem(object item)
        {
            this.viewModel.numItem = (item as Organization).Id;
        }

        public override void Farther()
        {
            throw new NotImplementedException();
        }

        public override void Searching(string pattern)
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            AddUpdateOrgsView orgsView = 
                new AddUpdateOrgsView(new UpdateOrgsModel(
                    (Organization)this.viewModel.CurrentItem));
            orgsView.Show();
        }

        protected override void Query()
        {
            var organizations = from orgs in table.AsEnumerable()
                                where orgs.Field<int>("f_org_id") != 0
                                select new Organization()
                                {
                                    Id = orgs.Field<int>("f_org_id"),
                                    Type = orgs.Field<string>("f_org_type"),
                                    FullName = orgs.Field<string>("f_full_org_name"),
                                    Name = orgs.Field<string>("f_org_name"),
                                    Comment = orgs.Field<string>("f_comment")
                                };
            this.viewModel.Set = organizations;
            if (viewModel.numItem == -1)
            {
                this.Begin();
            }
            else
            {
                try
                {
                    this.viewModel.CurrentItem = organizations.First(
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
