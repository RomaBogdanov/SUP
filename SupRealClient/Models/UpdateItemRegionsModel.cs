using System;
using System.Data;
using SupClientConnectionLib;
using SupRealClient.EnumerationClasses;
using SupRealClient.Common.Data;
using SupRealClient.TabsSingleton;

namespace SupRealClient.Models
{
    /// <summary>
    /// Обновление региона - модель
    /// </summary>
    class UpdateItemRegionsModel : IAddItem1Model
    {
        private Region region;

        public FieldData Data
        {
            get { return new FieldData { Field = region.RegionName }; }
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

        public void Ok(FieldData data)
        {
            RegionsWrapper regions = RegionsWrapper.CurrentTable();
            if (data.Field != "")
            {
                DataRow dataRow = regions.Table.Rows.Find(region.Id);
                dataRow["f_region_name"] = data.Field;
                dataRow["f_rec_date"] = DateTime.Now;
                dataRow["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
            }
            Cancel();
        }
    }
}
