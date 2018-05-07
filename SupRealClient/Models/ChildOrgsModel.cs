using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using SupRealClient.TabsSingleton;
using SupRealClient.EnumerationClasses;
using SupRealClient.Common.Interfaces;
using SupRealClient.Views;

namespace SupRealClient.Models
{
    class ChildOrgsModel : Base1OrganizationsModel
    {
        public ChildOrgsModel(IBase1ViewModel viewModel, IWindow parent) : 
            base(viewModel, parent)
        {
        }

        public override void Add()
        {
            //ViewManager.Instance.AddObject(new AddChildOrgsModel(), parent);
            var wind = new AddOrgsListView(new AddChildOrgsListModel());
            wind.ShowDialog();
        }

        public override void Update()
        {
            OrganizationsWrapper organizations =
                OrganizationsWrapper.CurrentTable();
            DataRow row = organizations.Table.Rows.Find(
                (this.viewModel.CurrentItem as Organization).Id);
            row["f_has_free_access"] = "N";
        }

        protected override void Query()
        {
            var organizations = from orgs in table.AsEnumerable()
                                where orgs.Field<int>("f_org_id") != 0 &
                                orgs.Field<string>("f_has_free_access")
                                .ToString().ToUpper() == "Y"
                                select new Organization()
                                {
                                    Id = orgs.Field<int>("f_org_id"),
                                    Type = orgs.Field<string>("f_org_type"),
                                    FullName = OrganizationsHelper.GenerateFullName(
                                        orgs.Field<int>("f_org_id")),
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

        public override IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>()
            {
                { "f_org_type", "Тип" },
                { "f_full_org_name", "Основное название" }
            };
        }
    }
}
