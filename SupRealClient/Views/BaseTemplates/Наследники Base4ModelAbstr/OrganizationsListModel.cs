﻿using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using SupRealClient.TabsSingleton;
using SupRealClient.EnumerationClasses;
using System.Data;
using SupRealClient.Models;
using SupRealClient.Common;
using System.Windows;

namespace SupRealClient.Views
{
    public class OrganizationsListModel<T> : Base4ModelAbstr<T>
        where T : Organization, new()
    {
        public OrganizationsListModel()
        {
            OrganizationsWrapper.CurrentTable().OnChanged += Query;
            Query();
            Begin();
        }

        #region BtnHandlers

        public override void Add()
        {
            AddOrgsModel model =new AddOrgsModel();
            var wind = new AddUpdateOrgsView(model);
            wind.ShowDialog();

            //ViewManager.Instance.AddObject(new AddOrgsModel(), Parent);
        }

        public override void Update()
        {
            if (CurrentItem != null)
            {
                UpdateOrgsModel model = new UpdateOrgsModel(CurrentItem);
                var wind = new AddUpdateOrgsView(model);
                wind.ShowDialog();

                //ViewManager.Instance.UpdateObject(new UpdateOrgsModel(CurrentItem), Parent);
            }
        }

        public override void RightClick()
        {
            int? res = ViewManager.Instance.OpenSynonims(CurrentItem);
            if (res.HasValue)
            {
                SetAt(res.Value);
            }
        }

        #endregion

        public override bool Remove()
        {
            if ((from vis in VisitorsWrapper.CurrentTable().Table.AsEnumerable()
                 where vis.Field<int>("f_org_id") == currentItem.Id &&
                 CommonHelper.NotDeleted(vis)
                 select vis).Any())
            {
                MessageBox.Show("Организацию невозможно удалить, т.к. она связана с посетителями!");
                return false;
            }

            if ((from orgs in OrganizationsWrapper.CurrentTable().Table.AsEnumerable()
                 where orgs.Field<int>("f_syn_id") == currentItem.Id &&
                 CommonHelper.NotDeleted(orgs)
                 select orgs).Any())
            {
                MessageBox.Show("Перед удалением организации удалите её синонимы!");
                return false;
            }

            DataRow row =
                OrganizationsWrapper.CurrentTable().Table.Rows.Find(currentItem.Id);
            row["f_deleted"] = CommonHelper.BoolToString(true);

            return true;
        }

        protected override BaseModelResult GetResult()
        {
            return new BaseModelResult { Id = CurrentItem.Id, Name = CurrentItem.FullName };
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
                from orgs in OrganizationsWrapper.CurrentTable().Table.AsEnumerable()
                where orgs.Field<int>("f_org_id") != 0 &&
                CommonHelper.NotDeleted(orgs)
                select new T
                {
                    Id = orgs.Field<int>("f_org_id"),
                    Type = orgs.Field<string>("f_org_type"),
                    Name = OrganizationsHelper.UntrimName(
                        orgs.Field<string>("f_org_name")),
                    FullName = OrganizationsHelper.
                        GenerateFullName(orgs.Field<int>("f_org_id")),
                    Comment = orgs.Field<string>("f_comment"),
                    CountryId = orgs.Field<int>("f_cntr_id"),
                    Country = orgs.Field<int>("f_cntr_id") == 0 ? 
                        "" : CountriesWrapper.CurrentTable()
                        .Table.AsEnumerable().FirstOrDefault(
                        arg => arg.Field<int>("f_cntr_id") ==
                        orgs.Field<int>("f_cntr_id"))["f_cntr_name"].ToString(),
                    RegionId = orgs.Field<int>("f_region_id"),
                    Region = orgs.Field<int>("f_region_id") == 0 ? 
                        "" : RegionsWrapper.CurrentTable()
                        .Table.AsEnumerable().FirstOrDefault(
                        arg => arg.Field<int>("f_region_id") ==
                        orgs.Field<int>("f_region_id"))["f_region_name"].ToString(),
                    SynId = orgs.Field<int>("f_syn_id")
                });
        }

        public override long GetId(int index)
        {
            return Rows[index].Field<int>("f_org_id");
        }

        public override DataRow[] Rows
        {
            get
            {
                return (from orgs in Table.AsEnumerable()
                        where orgs.Field<int>("f_org_id") != 0 select orgs).
                        AsEnumerable().ToArray();
            }
        }

        public override IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>
            {
                { "Type", "Тип" },
                { "Name", "Название организации" },
                { "Comment", "Примечание" },
                { "FullName", "Основное название" },
                { "Country", "Страна" },
                { "Region", "Регион" },
            };
        }

        protected override DataTable Table
        {
            get
            {
                return OrganizationsWrapper.CurrentTable().Table;
            }
        }

        protected override IDictionary<string, string> GetColumns()
        {
            return new Dictionary<string, string>
            {
                { "Type", "f_org_type" },
                { "Name", "f_org_name" },
                { "Comment", "f_comment" },
            };
        }
    }
}
