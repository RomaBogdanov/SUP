using System;
using System.Data;
using System.Linq;
using System.Windows;
using SupClientConnectionLib;
using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;
using SupRealClient.Utils;

namespace SupRealClient.Models
{
    /// <summary>
    ///     Добавление региона - модель
    /// </summary>
    internal class AddItemRegionsModel : IAddUpdateRegionModel
    {
        public Region Data => new Region();

        public event Action OnClose;

        public void Cancel()
        {
            OnClose?.Invoke();
        }

        public void Ok(Region data)
        {
            if (data.Name.IsNullOrEmptyOrWhiteSpaces())
            {
                MessageBox.Show("Заполните поле Название", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (data.Country.IsNullOrEmptyOrWhiteSpaces())
            {
                MessageBox.Show("Заполните поле Страна", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            data.CountryId = RegionsHelper.AddOrUpdateCountry(data.Country);

            var regions = RegionsWrapper.CurrentTable();
            var rows = (from object row in regions.Table.Rows select row as DataRow).ToList();

            var isNewRegion =
                rows.SingleOrDefault(
                    r =>
                        r.Field<string>("f_region_name").EqualsWithoutSpacesAndCase(data.Name) &&
                        r.Field<int>("f_cntr_id") == data.CountryId) == null;

            if (isNewRegion)
            {
                var row = regions.Table.NewRow();
                row["f_region_name"] = data.Name;
                row["f_cntr_id"] = data.CountryId;
                row["f_deleted"] = "N";
                row["f_rec_date"] = DateTime.Now;
                row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                regions.Table.Rows.Add(row);
                Cancel();
            }
            else
            {
                MessageBox.Show("Такой регион уже существует!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}