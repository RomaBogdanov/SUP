using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using SupRealClient.TabsSingleton;
using System.Data;
using SupRealClient.Models;
using SupRealClient.Common;
using System.Windows;

namespace SupRealClient.Views
{
    public class RegionsListModel<T> : Base4ModelAbstr<T>
        where T : EnumerationClasses.Region, new()
    {
        private int? countryId = null;

        public RegionsListModel()
        {
            RegionsWrapper.CurrentTable().OnChanged += Query;
            Query();
            Begin();
        }

        public override void Add()
        {
            ViewManager.Instance.AddObject(new AddItemRegionsModel(), Parent);
        }

        public override void Update()
        {
            if (CurrentItem != null)
            {
                ViewManager.Instance.UpdateObject(new UpdateItemRegionsModel(CurrentItem), Parent);
            }
        }

        public void SetCountry(int countryId)
        {
            this.countryId = countryId;
            Query();
            Begin();
        }

        public override bool Remove()
        {
            if ((from orgs in OrganizationsWrapper.CurrentTable().Table.AsEnumerable()
                where orgs.Field<int>("f_region_id") == currentItem.Id &&
                CommonHelper.NotDeleted(orgs)
                select orgs).Any())
            {
                MessageBox.Show("Регион невозможно удалить, т.к. он связан с организациями!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            DataRow row =
                RegionsWrapper.CurrentTable().Table.Rows.Find(currentItem.Id);
            row["f_deleted"] = CommonHelper.BoolToString(true);

            return true;
        }

        protected override BaseModelResult GetResult()
        {
            return new BaseModelResult { Id = CurrentItem.Id, Name = CurrentItem.Name };
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
                from regs in RegionsWrapper.CurrentTable().Table.AsEnumerable()
                where regs.Field<int>("f_region_id") != 0 &&
                CommonHelper.NotDeleted(regs) &&
                (countryId.HasValue ?
                regs.Field<int>("f_cntr_id") == countryId.Value : true)
                select new T
                {
                    Id = regs.Field<int>("f_region_id"),
                    Name = regs.Field<string>("f_region_name"),
                    CountryId = regs.Field<int>("f_cntr_id"),
                    Country = regs.Field<int>("f_cntr_id") == 0 ?
                        "" : CountriesWrapper.CurrentTable()
                        .Table.AsEnumerable().FirstOrDefault(
                        arg => arg.Field<int>("f_cntr_id") ==
                        regs.Field<int>("f_cntr_id"))["f_cntr_name"].ToString(),
                    Deleted = CommonHelper.StringToBool(
                        regs.Field<string>("f_deleted")),
                    RecDate = regs.Field<DateTime>("f_rec_date"),
                    RecOperator = regs.Field<int>("f_rec_operator")
                });
        }

        public override IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>()
            {
                { "Name", "Название" },
                { "Country", "Страна" }
            };
        }

        public override long GetId(int index)
        {
            return Rows[index].Field<int>("f_region_id");
        }

        public override DataRow[] Rows
        {
            get
            {
                return (from regs in RegionsWrapper.CurrentTable().Table.AsEnumerable()
                        where regs.Field<int>("f_region_id") != 0 &&
                        (countryId.HasValue ?
                        regs.Field<int>("f_cntr_id") == countryId.Value : true)
                        select regs).
                        AsEnumerable().ToArray();
            }
        }

        protected override DataTable Table
        {
            get
            {
                return RegionsWrapper.CurrentTable().Table;
            }
        }

        protected override IDictionary<string, string> GetColumns()
        {
            return new Dictionary<string, string>()
            {
                { "Name", "f_region_name" },
            };
        }
    }
}
