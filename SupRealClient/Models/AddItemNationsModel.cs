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
            if (string.IsNullOrEmpty(data.Field))
            {
                MessageBox.Show("Заполните поле «Введите страну»", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            CountriesWrapper countries = CountriesWrapper.CurrentTable();

            var rows = (from object row in countries.Table.Rows select row as DataRow).ToList();

            var newNation =
                rows.FirstOrDefault(
                    r =>
                        r.Field<string>("f_cntr_name").ToUpper() == data.Field.ToUpper()) == null;

            if (newNation)
            {
                DataRow row = countries.Table.NewRow();
                row["f_cntr_name"] = data.Field;
                row["f_deleted"] = "N";
                row["f_rec_date"] = DateTime.Now;
                row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                countries.Table.Rows.Add(row);
                Cancel();
            }
            else
            {
                MessageBox.Show("Такая страна уже существует!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }            
        }
    }
}
