using System;
using System.Data;
using SupClientConnectionLib;
using SupRealClient.EnumerationClasses;
using SupRealClient.Common.Data;
using SupRealClient.TabsSingleton;

namespace SupRealClient.Models
{
    /// <summary>
    /// Обновление страны - модель
    /// </summary>
    class UpdateItemNationsModel : IAddItem1Model
    {
        private Nation nation;

        public FieldData Data { get { return new FieldData { Field = nation.CountryName }; } }

        public event Action OnClose;

        public UpdateItemNationsModel(Nation nation)
        {
            this.nation = nation;
        }

        public void Cancel()
        {
            OnClose?.Invoke();
        }

        public void Ok(FieldData data)
        {
            CountriesWrapper countries = CountriesWrapper.CurrentTable();
            if (data.Field != "")
            {
                DataRow dataRow = countries.Table.Rows.Find(nation.Id);
                dataRow.BeginEdit();
                dataRow["f_cntr_name"] = data.Field;
                dataRow["f_rec_date"] = DateTime.Now;
                dataRow["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                dataRow.EndEdit();
            }
            Cancel();
        }
    }
}
