using System;
using System.Data;
using System.Linq;
using System.Windows;
using SupClientConnectionLib;
using SupRealClient.Common;
using SupRealClient.Common.Data;
using SupRealClient.Common.Interfaces;
using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;
using SupRealClient.Views;

namespace SupRealClient.Models
{
    /// <summary>
    /// Добавление страны - модель
    /// </summary>
    class AddItemNationsModel : IAddItem1Model
    {
        IWindow parent;

        public AddItemNationsModel(IWindow parent = null)
        {
            this.parent = parent;
        }

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
                MessageBox.Show("Заполните поле - 'Введите страну'");
                return;
            }

            CountriesWrapper countries = CountriesWrapper.CurrentTable();

            var rows = (from object row in countries.Table.Rows select row as DataRow).ToList();


            var sameRow = rows.FirstOrDefault(
                    r =>
                        r.Field<string>("f_cntr_name").ToUpper() == data.Field.ToUpper());


            if (sameRow == null)
            {
                DataRow row = countries.Table.NewRow();
                row["f_cntr_name"] = data.Field;
                row["f_deleted"] = "N";
                row["f_rec_date"] = DateTime.Now;
                row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                countries.Table.Rows.Add(row);
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
                MessageBox.Show("Такая страна уже записана!");
            }            
        }
    }
}
