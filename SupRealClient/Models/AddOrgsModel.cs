using System;
using System.Data;
using SupClientConnectionLib;
using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;

namespace SupRealClient.Models
{
    /// <summary>
    /// Добавление организации - модель
    /// </summary>
    class AddOrgsModel : IAddUpdateOrgsModel
    {
        protected bool IsChild { get; set; } = false;
        protected bool IsMaster { get; set; } = false;

        public Organization Data { get { return new Organization(); } }

        public event Action OnClose;

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
                DataRow row = organizations.Table.NewRow();
                row["f_org_type"] = data.Type;
                row["f_org_name"] = data.Name;
                row["f_comment"] = data.Comment;
                row["f_full_org_name"] = data.FullName;
                row["f_rec_date"] = DateTime.Now;
                row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                row["f_has_free_access"] = IsChild ? "Y" : "N";
                row["f_is_basic"] = IsMaster ? "Y" : "N";
                organizations.Table.Rows.Add(row);
            }
            Cancel();
        }
    }

    class AddChildOrgsModel: AddOrgsModel
    {
        public AddChildOrgsModel() =>
            IsChild = true;
    }

    class AddMasterOrgsModel: AddOrgsModel
    {
        public AddMasterOrgsModel() => IsMaster = true;
    }
}
