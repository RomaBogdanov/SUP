using System;
using System.Data;
using SupClientConnectionLib;
using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;
using System.Windows;
using SupRealClient.Common;

namespace SupRealClient.Models
{
    /// <summary>
    /// Обновление региона - модель
    /// </summary>
    class UpdateItemRegionsModel : IAddUpdateRegionModel
    {
        private Region region;

        public Region Data
        {
            get
            {
                return new Region
                {
                    Id = region.Id,
                    Name = region.Name,
                    CountryId = region.CountryId,
                    Country = region.Country
                };
            }
        }

        public event Action OnClose;

        public UpdateItemRegionsModel(Region region)
        {
            this.region = region;
        }

        public void Cancel()
        {
            OnClose?.Invoke();
        }

        public void Ok(Region data)
        {
            if (string.IsNullOrEmpty(data.Name))
            {
                MessageBox.Show("Заполните поле Название");
                return;
            }

            data.CountryId = RegionsHelper.AddOrUpdateCountry(data.Country);

            RegionsWrapper regions = RegionsWrapper.CurrentTable();

            DataRow row = regions.Table.Rows.Find(region.Id);
            row.BeginEdit();
            row["f_region_name"] = data.Name;
            row["f_cntr_id"] = data.CountryId;
            row["f_rec_date"] = DateTime.Now;
            row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
            row["f_deleted"] = CommonHelper.BoolToString(false);
            row.EndEdit();
            Cancel();
        }
    }
}
