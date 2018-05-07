using SupRealClient.Common;
using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SupRealClient.Models
{
    public static class OrganizationsHelper
    {
        public static List<string> GetTypes()
        {
            var list = new List<string>();
            var organizations = OrganizationsWrapper.CurrentTable();

            foreach (DataRow row in organizations.Table.Rows)
            {
                if (!list.Contains(row.ItemArray[2].ToString()) &&
                    !string.IsNullOrEmpty(row.ItemArray[2].ToString()) &&
                    row.ItemArray[2].ToString() != "нет данных")
                {
                    list.Add(row.ItemArray[2].ToString());
                }
            }

            return list;
        }

        public static List<string> GetDescriptions()
        {
            var list = new List<string>();
            var organizations = OrganizationsWrapper.CurrentTable();

            foreach (DataRow row in organizations.Table.Rows)
            {
                if (!list.Contains(row.ItemArray[3].ToString()) &&
                    !string.IsNullOrEmpty(row.ItemArray[3].ToString()))
                {
                    list.Add(row.ItemArray[3].ToString());
                }
            }

            return list;
        }

        public static List<Organization> GetFullNameOrganizations(int id)
        {
            return new List<Organization>
                (from orgs in OrganizationsWrapper.CurrentTable().Table.AsEnumerable()
                 where orgs.Field<int>("f_org_id") != 0 &&
                 orgs.Field<int>("f_org_id") != id &&
                 !CommonHelper.StringToBool(orgs.Field<string>("f_is_basic")) &&
                 orgs.Field<int?>("f_syn_id") == 0
                 select new Organization
                 {
                     Id = orgs.Field<int>("f_org_id"),
                     Type = orgs.Field<string>("f_org_type"),
                     Name = orgs.Field<string>("f_org_name"),
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
                     SynId = 0
                 });
        }

        public static string GenerateFullName(int id)
        {
            if (id <= 0)
            {
                return "";
            }

            var org = (from orgs in OrganizationsWrapper.CurrentTable().
                Table.AsEnumerable()
                where orgs.Field<int>("f_org_id") == id
                select new Organization
                {
                    Id = orgs.Field<int>("f_org_id"),
                    Type = orgs.Field<string>("f_org_type"),
                    Name = orgs.Field<string>("f_org_name"),
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
                }).FirstOrDefault();

            return GenerateFullName(org);
        }

        private static string GenerateFullName(Organization organization)
        {
            if (organization == null)
            {
                return "";
            }

            if (organization.SynId == 0)
            {
                var sb = new StringBuilder();
                sb.Append(organization.Type);
                sb.Append(" ");
                sb.Append(organization.Name);
                if (organization.CountryId > 0)
                {
                    sb.Append(", ");
                    sb.Append(organization.Country);
                }
                if (organization.RegionId > 0)
                {
                    sb.Append(", ");
                    sb.Append(organization.Region);
                }

                return sb.ToString();
            }
            else
            {
                return GenerateFullName(organization.SynId);
            }
        }

        public static bool FullNameEnabled(int id)
        {
            return id == 0 ? true :
                !(from orgs in OrganizationsWrapper.CurrentTable().
                 Table.AsEnumerable()
                 where orgs.Field<int?>("f_syn_id") == id
                 select orgs.Field<int?>("f_syn_id")).Any();
        }
    }
}
