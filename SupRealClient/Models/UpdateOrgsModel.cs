using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SupClientConnectionLib;

namespace SupRealClient
{
    class UpdateOrgsModel : IAddUpdateOrgsModel
    {
        private AddUpdateOrgsViewModel viewModel;
        private Organization organization;

        public AddUpdateOrgsViewModel ViewModel
        {
            set
            {
                this.viewModel = value;
                this.viewModel.Type = organization.Type;
                this.viewModel.Name = organization.Name;
                this.viewModel.Comment = organization.Comment;
                this.viewModel.FullName = organization.FullName;
            }
        }

        public event Action OnClose;

        public UpdateOrgsModel(Organization organization)
        { this.organization = organization; }

        public void Cancel()
        {
            OnClose?.Invoke();
        }

        public void Ok()
        {
            OrganizationsWrapper organizations =
                OrganizationsWrapper.CurrentTable();
            if (!(this.viewModel.Type == "" | this.viewModel.Name == "" |
                this.viewModel.FullName == ""))
            {
                DataRow row = organizations.Table.Rows.Find(organization.Id);
                row["f_org_type"] = this.viewModel.Type;
                row["f_org_name"] = this.viewModel.Name;
                row["f_comment"] = this.viewModel.Comment;
                row["f_full_org_name"] = this.viewModel.FullName;
                row["f_rec_date"] = DateTime.Now;
                row["f_rec_operator"] = Authorizer.AppAuthorizer.Login;
            }
            Cancel();
        }
    }
}
