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
    public class NationsListModel<T> : Base4ModelAbstr<T>
        where T : EnumerationClasses.Nation, new()
    {
        public NationsListModel()
        {
            CountriesWrapper.CurrentTable().OnChanged += Query;
            Query();
            Begin();
        }

        public override void Add()
        {
            ViewManager.Instance.Add(new AddItemNationsModel(), Parent);
        }

        public override void Update()
        {
            if (CurrentItem != null)
            {
                ViewManager.Instance.Update(new UpdateItemNationsModel(CurrentItem), Parent);
            }
        }

        public override bool Remove()
        {
            if ((from orgs in OrganizationsWrapper.CurrentTable().Table.AsEnumerable()
                 where orgs.Field<int>("f_cntr_id") == currentItem.Id &&
                 CommonHelper.NotDeleted(orgs)
                 select orgs).Any())
            {
                MessageBox.Show("Страну невозможно удалить, т.к. она связана с посетителями!");
                return false;
            }

            if ((from regs in RegionsWrapper.CurrentTable().Table.AsEnumerable()
                 where regs.Field<int>("f_cntr_id") == currentItem.Id &&
                 CommonHelper.NotDeleted(regs)
                 select regs).Any())
            {
                MessageBox.Show("Страну невозможно удалить, т.к. она связана с регионом!");
                return false;
            }

            DataRow row =
                CountriesWrapper.CurrentTable().Table.Rows.Find(currentItem.Id);
            row["f_deleted"] = CommonHelper.BoolToString(true);

            return true;
        }

        protected override BaseModelResult GetResult()
        {
            return new BaseModelResult { Id = CurrentItem.Id, Name = CurrentItem.CountryName };
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
                from nats in CountriesWrapper.CurrentTable().Table.AsEnumerable()
                where nats.Field<int>("f_cntr_id") != 0 &&
                CommonHelper.NotDeleted(nats)
                select new T
                {
                    Id = nats.Field<int>("f_cntr_id"),
                    CountryName = nats.Field<string>("f_cntr_name"),
                    Deleted = CommonHelper.StringToBool(
                        nats.Field<string>("f_deleted")),
                    RecDate = nats.Field<DateTime>("f_rec_date"),
                    RecOperator = nats.Field<int>("f_rec_operator")
                }
                );
        }

        public override IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>()
            {
                { "CountryName", "Название" }
            };
        }

        public override long GetId(int index)
        {
            return Rows[index].Field<int>("f_cntr_id");
        }

        protected override DataTable Table
        {
            get
            {
                return CountriesWrapper.CurrentTable().Table;
            }
        }

        protected override IDictionary<string, string> GetColumns()
        {
            return new Dictionary<string, string>()
            {
                { "CountryName", "f_cntr_name" },
            };
        }
    }
}
