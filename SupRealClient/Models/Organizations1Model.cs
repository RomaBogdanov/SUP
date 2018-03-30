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
    class Organizations1Model : IOrganizations1Model
    {
        private DataTable tabOrganizations;
        private ClientConnector tabConnector;
        private string tabName;
        private Base2ViewModel viewModel;

        public Organizations1Model(Base2ViewModel viewModel)
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
                                    FullName = orgs.Field<string>("f_full_org_name"),
                                    Name = orgs.Field<string>("f_org_name"),
                                    Comment = orgs.Field<string>("f_comment")
                                };
            this.viewModel.Organizations = organizations;
        }
    }
}
