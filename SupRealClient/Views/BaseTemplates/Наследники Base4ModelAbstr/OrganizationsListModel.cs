using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using SupRealClient.TabsSingleton;
using SupRealClient.EnumerationClasses;
using System.Data;
using SupRealClient.Models;

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
            ViewManager.Instance.AddObject(new AddOrgsModel(), Parent);
        }

        public override void Update()
        {
            if (CurrentItem != null)
            {
                ViewManager.Instance.UpdateObject(new UpdateOrgsModel(CurrentItem), Parent);
            }
        }

        public override void RightClick()
        {
            ViewManager.Instance.OpenSynonims(CurrentItem);
        }

        #endregion

        protected override BaseModelResult GetResult()
        {
            return new BaseModelResult { Id = CurrentItem.Id, Name = CurrentItem.FullName };
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
                from orgs in OrganizationsWrapper.CurrentTable().Table.AsEnumerable()
                where orgs.Field<int>("f_org_id") != 0
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
                { "f_org_type", "Тип" },
                { "f_org_name", "Название организации" },
                { "f_comment", "Примечание" },
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
