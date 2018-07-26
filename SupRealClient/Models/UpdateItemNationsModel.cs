using System;
using System.Data;
using System.Linq;
using SupClientConnectionLib;
using SupRealClient.EnumerationClasses;
using SupRealClient.Common.Data;
using SupRealClient.TabsSingleton;
using System.Windows;

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
                if (countries.Table.AsEnumerable().FirstOrDefault(x => 
                    x["f_cntr_name"].ToString() == data.Field.ToString() &&
                    (int)x["f_cntr_id"] != nation.Id) != null)
                {
                    MessageBox.Show("Такая организация уже записана!");
                    return;
                }

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
