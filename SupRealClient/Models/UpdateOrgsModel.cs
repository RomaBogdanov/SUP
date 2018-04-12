using System;
using System.Data;
using SupClientConnectionLib;
using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;

namespace SupRealClient.Models
{
    /// <summary>
    /// Обновление организации - модель
    /// </summary>
    class UpdateOrgsModel : IAddUpdateOrgsModel
    {
        private Organization organization;

        public Organization Data
        {
            get
            {
                return new Organization
                {
                    Type = organization.Type,
                    Name = organization.Name,
                    Comment = organization.Comment,
                    FullName = organization.FullName
                };
            }
        }

        public event Action OnClose;

        public UpdateOrgsModel(Organization organization)
        { this.organization = organization; }

        public void Cancel()
        {
            OnClose?.Invoke();
        }

        public void Ok(Organization data)
        {
            OrganizationsWrapper organizations =
                OrganizationsWrapper.CurrentTable();
            if (!(data.Type == "" | data.Name == "" | data.FullName == ""))
            {
                DataRow row = organizations.Table.Rows.Find(organization.Id);
                row["f_org_type"] = data.Type;
                row["f_org_name"] = data.Name;
                row["f_comment"] = data.Comment;
                row["f_full_org_name"] = data.FullName;
                row["f_rec_date"] = DateTime.Now;
                row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
            }
            Cancel();
        }
    }
}
