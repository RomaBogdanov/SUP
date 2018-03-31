using System;
using System.Data;
using SupClientConnectionLib;
using SupRealClient.Common.Data;
using SupRealClient.TabsSingleton;

namespace SupRealClient.Models
{
    /// <summary>
    /// Добавление организации - модель
    /// </summary>
    class AddOrgsModel : IAddUpdateOrgsModel
    {
        public OrganizationData Data { get { return new OrganizationData(); } }

        public event Action OnClose;

        public void Cancel()
        {
            OnClose?.Invoke();
        }

        public void Ok(OrganizationData data)
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
                row["f_rec_operator"] = Authorizer.AppAuthorizer.Login;
                organizations.Table.Rows.Add(row);
            }
            Cancel();
        }
    }
}
