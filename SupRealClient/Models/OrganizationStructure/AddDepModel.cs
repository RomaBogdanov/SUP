using System;
using SupRealClient.Models.OrganizationStructure.Interfaces;
using System.Data;
using System.Linq;
using SupRealClient.TabsSingleton;
using SupClientConnectionLib;
using System.ComponentModel;
using SupRealClient.Common;
using System.Windows;

namespace SupRealClient.Models.OrganizationStructure
{
    class AddDepModel : IModel
    {
        public string Description { get; set; }
        public int IdEditedItem { get; set; }
        public bool Save
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event Action OnClose;

        private int organizationId;
        private int departmentId;

        public AddDepModel(int organizationId, int departmentId)
        {
            this.organizationId = organizationId;
            this.departmentId = departmentId;
            IdEditedItem = -1;
        }

        public void EditItem()
        {
            if (!(Description == "" | Description == null))
            {
                DepartmentWrapper departments = DepartmentWrapper.CurrentTable();

                var rows = (from object irow in departments.Table.Rows select irow as DataRow).ToList();

                var sameRow = rows.FirstOrDefault(
                      r =>
                          r.Field<string>("f_dep_name").ToUpper() == Description.ToUpper() &&
                          r.Field<int>("f_org_id") == organizationId);

                int Id = -1;
                if (sameRow == null)
                {
                    DataRow row = DepartmentWrapper.CurrentTable().Table.NewRow();
                    row["f_dep_name"] = Description;
                    row["f_org_id"] = organizationId;
                    row["f_parent_id"] = departmentId;
                    row["f_rec_date"] = DateTime.Now;
                    row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                    row["f_deleted"] = "N";
                    DepartmentWrapper.CurrentTable().Table.Rows.Add(row);

                    Id = row.Field<int>("f_dep_id");
                }
                else if (sameRow.Field<string>("f_deleted") == CommonHelper.BoolToString(true))
                {
                    Id = sameRow.Field<int>("f_dep_id");
                    DataRow row = departments.Table.Rows.Find(Id);
                    row.BeginEdit();
                    row["f_parent_id"] = departmentId;
                    row["f_rec_date"] = DateTime.Now;
                    row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                    row["f_deleted"] = CommonHelper.BoolToString(false);
                    row.EndEdit();                 
                }
                else
                {
                    MessageBox.Show("Такое подразделение для организации уже существует!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                IdEditedItem = Id;
                OnClose?.Invoke();
            }            
        }

        public void Cancel()
        {
            OnClose?.Invoke();
        }
    }
}
