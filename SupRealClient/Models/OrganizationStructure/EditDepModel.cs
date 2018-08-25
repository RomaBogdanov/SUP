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
using SupRealClient.Common;
using System.Windows;

namespace SupRealClient.Models.OrganizationStructure
{
    class EditDepModel : IModel
    {
        public string Description { get; set; }
        public string FullDescription { get; set; }
        public bool Save
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event Action OnClose;

        private int organizationId;
        private int departmentId;

        public EditDepModel(int organizationId, int departmentId)
        {
            this.organizationId = organizationId;
            this.departmentId = departmentId;        
        }

        public void EditItem()
        {
            if (!(string.IsNullOrEmpty(Description) || string.IsNullOrEmpty(FullDescription)))
            {
                DepartmentWrapper departments = DepartmentWrapper.CurrentTable();

                var rows = (from object irow in departments.Table.Rows select irow as DataRow).ToList();

                var sameRow = rows.FirstOrDefault(
                      r =>
                          r.Field<int>("f_dep_id") != departmentId &&
                          r.Field<int>("f_org_id") == organizationId &&
                          (r.Field<string>("f_dep_name").ToUpper() == FullDescription.ToUpper() ||
                          r.Field<string>("f_short_dep_name").ToUpper() == Description.ToUpper()));

                if (sameRow == null)
                {
                    DataRow row = DepartmentWrapper.CurrentTable()
                        .Table.Rows.Find(departmentId);
                    row.BeginEdit();
                    row["f_short_dep_name"] = Description;
                    row["f_dep_name"] = FullDescription;
                    row["f_rec_date"] = DateTime.Now;
                    row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                    row.EndEdit();

                    Cancel();
                }
                else if (sameRow.Field<string>("f_deleted") == CommonHelper.BoolToString(true))
                {
                    DataRow row = DepartmentWrapper.CurrentTable()
                     .Table.Rows.Find(departmentId);                   

                    int Id = sameRow.Field<int>("f_dep_id");
                    DataRow deletedRow = departments.Table.Rows.Find(Id);
                    deletedRow.BeginEdit();
                    deletedRow["f_short_dep_name"] = row.Field<string>("f_short_dep_name");
                    deletedRow["f_dep_name"] = row.Field<string>("f_dep_name");
                    deletedRow["f_rec_date"] = DateTime.Now;
                    deletedRow["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                   
                    row.BeginEdit();
                    row["f_short_dep_name"] = Description;
                    row["f_dep_name"] = FullDescription;
                    row["f_rec_date"] = DateTime.Now;
                    row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                    
                    row.EndEdit();
                    deletedRow.EndEdit();

                    Cancel();
                }
                else
                {
                    MessageBox.Show("Такое подразделение для организации уже существует!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }                
            }            
        }

        public void Cancel()
        {
            OnClose?.Invoke();
        }
    }
}
