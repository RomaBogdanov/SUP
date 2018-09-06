using System;
using System.Data;
using System.Linq;
using System.Windows;
using SupClientConnectionLib;
using SupRealClient.EnumerationClasses;
using SupRealClient.Common.Data;
using SupRealClient.TabsSingleton;
using SupRealClient.Common;
using SupRealClient.Common.Interfaces;
using SupRealClient.Views;

namespace SupRealClient.Models
{
    /// <summary>
    /// Обновление страны - модель
    /// </summary>
    class UpdateItemNationsModel : IAddItem1Model
    {
        private Nation nation;
        IWindow parent;

        public FieldData Data { get { return new FieldData { Field = nation.CountryName }; } }

        public event Action OnClose;

        public UpdateItemNationsModel(Nation nation, IWindow parent = null)
        {
            this.nation = nation;
            this.parent = parent;
        }

        public void Cancel()
        {
            OnClose?.Invoke();
        }

        public void Ok(FieldData data)
        {
            Logger.Log.Debug($"Попытка редактирования страны");
            if (string.IsNullOrEmpty(data.Field))
            {
                MessageBox.Show("Заполните поле - 'Редактирование страны'");
                return;
            }

            CountriesWrapper countries = CountriesWrapper.CurrentTable();

            var rows = (from object row in countries.Table.Rows select row as DataRow).ToList();

            var sameRow = rows.FirstOrDefault(
                  r =>
                      r.Field<string>("f_cntr_name").ToUpper() == data.Field.ToUpper() &&
                      r.Field<int>("f_cntr_id") != nation.Id);                        

            if (sameRow == null)
            {
                DataRow dataRow = countries.Table.Rows.Find(nation.Id);
                dataRow.BeginEdit();
                dataRow["f_cntr_name"] = data.Field;
                dataRow["f_rec_date"] = DateTime.Now;
                dataRow["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                dataRow.EndEdit();
                Cancel();
            }
            else if (sameRow.Field<string>("f_deleted") == CommonHelper.BoolToString(true))
            {
                int Id = sameRow.Field<int>("f_cntr_id");
                DataRow row = countries.Table.Rows.Find(Id);
                row.BeginEdit();
                row["f_deleted"] = CommonHelper.BoolToString(false);
                row.EndEdit();

                Cancel();

                DataRow oldRow = countries.Table.Rows.Find(nation.Id);
                oldRow.BeginEdit();
                oldRow["f_deleted"] = CommonHelper.BoolToString(true);
                oldRow.EndEdit();

                Base4ViewModel<Nation> NationViewModel = (parent as Base4NationsWindView)?.base4.DataContext as Base4ViewModel<Nation>;
                if (NationViewModel != null)
                {
                    NationViewModel.CurrentItem = NationViewModel.Set.FirstOrDefault(
                                                                    r =>
                                                                        r.Id == Id);
                }
            }
            else
            {
                MessageBox.Show("Такая страна уже записана!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
