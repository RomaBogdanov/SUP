using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using SupRealClient.TabsSingleton;
using SupRealClient.EnumerationClasses;
using SupRealClient.Common.Interfaces;

namespace SupRealClient.Models
{
    class Base1OrganizationsModel : Base1ModelAbstr
    {
        public Base1OrganizationsModel(IBase1ViewModel viewModel, IWindow parent)
        {
            this.viewModel = viewModel;
            this.parent = parent;
            OrganizationsWrapper countriesWrapper = OrganizationsWrapper.CurrentTable();
            table = countriesWrapper.Table;
            tabConnector = countriesWrapper.Connector;
            tabName = countriesWrapper.Table.TableName;
            countriesWrapper.OnChanged += Query;
            this.Query();
        }

        public override void Add()
        {
            ViewManager.Instance.AddObject(new AddOrgsModel(), parent);
        }

        public override void Begin()
        {
            if (this.viewModel.Set.Count() > 0)
            {
                this.viewModel.CurrentItem = this.viewModel.Set.First();
                this.viewModel.NumItem =
                    (this.viewModel.CurrentItem as Organization).Id;
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
                (this.viewModel.CurrentItem as Organization).Id;
            this.viewModel.SelectedIndex = this.viewModel.Set.Count() - 1;
        }

        public override void EnterCurrentItem(object item)
        {
            this.viewModel.NumItem = (item as Organization).Id;
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
            ViewManager.Instance.UpdateObject(new UpdateOrgsModel((Organization)this.viewModel.CurrentItem), parent);
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
            if (viewModel.NumItem == -1)
            {
                this.Begin();
            }
            else
            {
                try
                {
                    this.viewModel.CurrentItem = organizations.First(
                        arg => arg.Id == this.viewModel.NumItem);
                }
                catch (Exception)
                {
                    this.Begin();
                }
            }
        }

        public override DataRow[] Rows
        {
            get
            {
                return (from orgs in table.AsEnumerable() where orgs.Field<int>("f_org_id") != 0 select orgs).AsEnumerable().ToArray();
            }
        }

        public override IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>()
            {
                { "f_org_type", "Тип" },
                { "f_full_org_name", "Основное название" },
                { "f_org_name", "Название организации" },
                { "f_comment", "Примечание" },
            };
        }
    }
}
