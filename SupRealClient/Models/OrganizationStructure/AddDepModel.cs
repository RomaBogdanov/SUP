using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupRealClient.Models.OrganizationStructure.Interfaces;
using System.Data;
using SupRealClient.TabsSingleton;
using SupClientConnectionLib;
using System.ComponentModel;

namespace SupRealClient.Models.OrganizationStructure
{
    class AddDepModel : IModel
    {
        public string Description { get; set; }
        public bool Save
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event Action OnClose;

        private int organizationId;

        public AddDepModel(int organizationId)
        {
            this.organizationId = organizationId;
        }

        public void EditItem()
        {
            if (!(Description == "" | Description == null))
            {
                DataRow row = DepartmentWrapper.CurrentTable().Table.NewRow();
                row["f_dep_name"] = Description;
                row["f_org_id"] = organizationId;
                row["f_rec_date"] = DateTime.Now;
                row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                DepartmentWrapper.CurrentTable().Table.Rows.Add(row);
            }
            OnClose?.Invoke();
        }

        public void Cancel()
        {
            OnClose?.Invoke();
        }
    }
}
