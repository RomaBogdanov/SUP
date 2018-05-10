﻿using System;
using System.Data;
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
                    SynId = organization.SynId
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
                    Views.OrganizationsWindView.CurrentWindow.Dispatcher.Invoke(() => { Views.OrganizationsWindView.CurrentWindow.Activate(); });
                    //Views.OrganizationsWindView.CurrentWindow.Focus();
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
            DataRow row = organizations.Table.Rows.Find(organization.Id);
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
            Cancel();
        }
    }
}
