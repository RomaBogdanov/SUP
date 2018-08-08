using System;
using System.Data;
using System.Linq;
using System.Windows;
using SupClientConnectionLib;
using SupRealClient.Common;
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
        
        public Organization Data => new Organization();

        public event Action OnClose;

        public void Cancel()
        {
            OnClose?.Invoke();
            if (Views.OrganizationsWindView.CurrentWindow != null)
            {
                System.Threading.Tasks.Task.Run(new Action(() =>
                {
                    System.Threading.Thread.Sleep(200);
                    Views.OrganizationsWindView.CurrentWindow.Dispatcher.Invoke(
                        () => { Views.OrganizationsWindView.CurrentWindow.Activate(); });

                }));
            }
        }
        
        public void Ok(Organization data)
        {
            if (string.IsNullOrEmpty(data.Type))
            {
                MessageBox.Show("Заполните поле Тип");
                return;
            }
            if (string.IsNullOrEmpty(OrganizationsHelper.TrimName(data.Name)))
            {
                MessageBox.Show("Заполните поле Название");
                return; 
            }

            OrganizationsWrapper organizations =
                OrganizationsWrapper.CurrentTable();

            var rows = (from object row in organizations.Table.Rows select row as DataRow).ToList();

            var sameRow = rows.FirstOrDefault(
                    r =>
                        r.Field<string>("f_org_type") == data.Type &&
                        r.Field<string>("f_org_name") ==
                            OrganizationsHelper.TrimName(data.Name) &&
                        r.Field<int>("f_cntr_id") == data.CountryId &&
                        r.Field<int>("f_region_id") == data.RegionId);
         
            if (sameRow == null)
            {
                DataRow row = organizations.Table.NewRow();
                row["f_org_type"] = data.Type;
                row["f_org_name"] = OrganizationsHelper.TrimName(data.Name);
                row["f_comment"] = data.Comment;
                //row["f_full_org_name"] = data.FullName;
                row["f_syn_id"] = data.SynId;
                row["f_region_id"] = data.RegionId;
                row["f_cntr_id"] = data.CountryId;
                row["f_rec_date"] = DateTime.Now;
                row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                row["f_has_free_access"] = CommonHelper.BoolToString(IsChild);
                row["f_is_basic"] = CommonHelper.BoolToString(IsMaster);
                row["f_deleted"] = CommonHelper.BoolToString(false);
                organizations.Table.Rows.Add(row);
                Cancel();
            }
            else if (sameRow.Field<string>("f_deleted") == CommonHelper.BoolToString(true))
            {
                //to-do доделать... если строка удалена, обратно её правильно показать
                int Id = sameRow.Field<int>("f_org_id");
                DataRow row = organizations.Table.Rows.Find(Id);
                row.BeginEdit();
                row["f_deleted"] = CommonHelper.BoolToString(false);
                row.EndEdit();
                Cancel();
            }
            else
            {
                MessageBox.Show("Такая организация уже записана!");
            }
        }
    }

    class AddChildOrgsModel: AddOrgsModel
    {
        public AddChildOrgsModel()
        {
            IsChild = true;
        }
    }

    class AddMasterOrgsModel: AddOrgsModel
    {
        public AddMasterOrgsModel()
        {
            IsMaster = true;
        }
    }
}
