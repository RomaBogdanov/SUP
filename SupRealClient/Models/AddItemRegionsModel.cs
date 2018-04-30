using System;
using System.Data;
using SupClientConnectionLib;
using SupRealClient.Common.Data;
using SupRealClient.TabsSingleton;

namespace SupRealClient.Models
{
    /// <summary>
    /// Добавление региона - модель
    /// </summary>
    class AddItemRegionsModel : IAddItem1Model
    {
        public event Action OnClose;

        public FieldData Data { get { return new FieldData(); } }

        public void Cancel()
        {
            OnClose?.Invoke();
        }

        public void Ok(FieldData data)
        {
            RegionsWrapper regions = RegionsWrapper.CurrentTable();
            if (data.Field != "")
            {
                DataRow row = regions.Table.NewRow();
                row["f_region_name"] = data.Field;
                row["f_deleted"] = "N";
                row["f_rec_date"] = DateTime.Now;
                row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                regions.Table.Rows.Add(row);
            }
            Cancel();
        }
    }
}
