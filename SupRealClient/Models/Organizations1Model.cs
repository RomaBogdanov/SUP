using System;
using System.Linq;
using System.Data;
using SupClientConnectionLib;
using SupRealClient.TabsSingleton;
using SupRealClient.EnumerationClasses;
using SupRealClient.Common.Interfaces;

namespace SupRealClient.Models
{
    class Organizations1Model : IOrganizations1Model
    {
        private DataTable tabOrganizations;
        private ClientConnector tabConnector;
        private string tabName;
        private IBase2ViewModel viewModel;

        public Organizations1Model(IBase2ViewModel viewModel)
        {
            this.viewModel = viewModel;
            OrganizationsWrapper organizationsWrapper = OrganizationsWrapper.CurrentTable();
            tabOrganizations = organizationsWrapper.Table;
            tabConnector = organizationsWrapper.Connector;
            tabName = organizationsWrapper.Table.TableName;
            this.Query();
        }

        public void Create()
        {
            // TODO:
            throw new NotImplementedException();
        }

        public void Delete()
        {
            // TODO:
            throw new NotImplementedException();
        }

        public void Edit()
        {
            // TODO:
            throw new NotImplementedException();
        }

        private void Query()
        {
            var organizations = from orgs in tabOrganizations.AsEnumerable()
                                where orgs.Field<int>("f_org_id") != 0
                                select new Organization()
                                {
                                    Id = orgs.Field<int>("f_org_id"),
                                    Type = orgs.Field<string>("f_org_type"),
                                    FullName = OrganizationsHelper.
                                        GenerateFullName(orgs.Field<int>("f_org_id")),
                                    Name = OrganizationsHelper.UntrimName(
                                        orgs.Field<string>("f_org_name")),
                                    Comment = orgs.Field<string>("f_comment")
                                };
            this.viewModel.Organizations = organizations;
        }
    }
}
