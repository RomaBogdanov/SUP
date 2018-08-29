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
                    Id = organization.Id,
                    Type = organization.Type,
                    Name = organization.Name,
                    Comment = organization.Comment,
                    FullName = OrganizationsHelper.
                        GenerateFullName(organization.Id),
                    CountryId = organization.CountryId,
                    Country = organization.Country,
                    RegionId = organization.RegionId,
                    Region = organization.Region,
                    SynId = organization.SynId,
					IsBasic=organization.IsBasic
                };
            }
        }

        public event Action OnClose;

        public UpdateOrgsModel(Organization organization)
        { this.organization = organization; }

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
            Logger.Log.Debug($"Попытка редактирования организации");
            if (string.IsNullOrEmpty(data.Type))
            {
                MessageBox.Show("Заполните поле «Тип»", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (string.IsNullOrEmpty(OrganizationsHelper.TrimName(data.Name)))
            {
                MessageBox.Show("Заполните поле «Название»", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            OrganizationsWrapper organizations =
                 OrganizationsWrapper.CurrentTable();

            var rows = (from object row in organizations.Table.Rows select row as DataRow).ToList();


            var sameRow = rows.FirstOrDefault(
                   r =>
                       r.Field<int>("f_org_id") != data.Id &&
                        r.Field<string>("f_org_type") == data.Type &&
                        r.Field<string>("f_org_name") ==
                            OrganizationsHelper.TrimName(data.Name) &&                       
                        r.Field<int>("f_cntr_id") == data.CountryId &&
                        r.Field<int>("f_region_id") == data.RegionId);

            if (sameRow == null)
            {                
                DataRow row = organizations.Table.Rows.Find(organization.Id);
                row.BeginEdit();
                row["f_org_type"] = data.Type;
                row["f_org_name"] = OrganizationsHelper.TrimName(data.Name);
                row["f_comment"] = data.Comment;
                //row["f_full_org_name"] = data.FullName;
                row["f_syn_id"] = data.SynId;
                row["f_region_id"] = data.RegionId;
                row["f_cntr_id"] = data.CountryId;
                row["f_rec_date"] = DateTime.Now;
                row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                row["f_deleted"] = CommonHelper.BoolToString(false);
                row.EndEdit();
                foreach (DataRow row2 in from orgs in
                        OrganizationsWrapper.CurrentTable().
                        Table.AsEnumerable()
                                         where orgs.Field<int?>("f_syn_id") == data.Id
                                         select orgs)
                {
                    row2.BeginEdit();
                    row2["f_region_id"] = data.RegionId;
                    row2["f_cntr_id"] = data.CountryId;
                    row2.EndEdit();
                }
                Cancel();
            }
            //else if (sameRow.Field<string>("f_deleted") == CommonHelper.BoolToString(true))
            //{
            //    int Id = sameRow.Field<int>("f_org_id");
            //    DataRow row = organizations.Table.Rows.Find(Id);
            //    row.BeginEdit();
            //    row["f_deleted"] = CommonHelper.BoolToString(false);
            //    row.EndEdit();
                
            //    DataRow oldRow = organizations.Table.Rows.Find(organization.Id);
            //    oldRow.BeginEdit();
            //    oldRow["f_deleted"] = CommonHelper.BoolToString(true);
            //    oldRow.EndEdit();

            //    Cancel();
            //}
            else
            {
                MessageBox.Show("Такая организация уже существует!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
