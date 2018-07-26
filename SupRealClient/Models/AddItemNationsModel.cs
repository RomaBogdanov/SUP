using System;
using System.Data;
using System.Linq;
using System.Windows;
using SupClientConnectionLib;
using SupRealClient.Common.Data;
using SupRealClient.TabsSingleton;

namespace SupRealClient.Models
{
    /// <summary>
    /// Добавление страны - модель
    /// </summary>
    class AddItemNationsModel : IAddItem1Model
    {
        public event Action OnClose;

        public FieldData Data { get { return new FieldData(); } }

        public void Cancel()
        {
            OnClose?.Invoke();
        }

        public void Ok(FieldData data)
        {
            CountriesWrapper countries = CountriesWrapper.CurrentTable();
            if (countries.Table.AsEnumerable().FirstOrDefault(x=> x["f_cntr_name"].ToString() == data.Field.ToString()) != null)
            {
                MessageBox.Show("Такая организация уже записана!");
                return;
            }
            if (data.Field != "")
            {
                DataRow row = countries.Table.NewRow();
                row["f_cntr_name"] = data.Field;
                row["f_deleted"] = "N";
                row["f_rec_date"] = DateTime.Now;
                row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                countries.Table.Rows.Add(row);
            }
            Cancel();
        }
    }
}
