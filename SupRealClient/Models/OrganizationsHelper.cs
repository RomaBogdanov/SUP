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
        public static List<string> GetTypes(string type)
        {
            var list = new List<string>();
            var organizations = OrganizationsWrapper.CurrentTable();

            list.Add(type);
            foreach (DataRow row in organizations.Table.Rows)
            {
                type = row.Field<string>("f_org_type");
                if (!list.Contains(type) && !string.IsNullOrEmpty(type) &&
                    type != "нет данных" &&
                    row.Field<string>("f_deleted") != CommonHelper.BoolToString(true))
                {
                    list.Add(type);
                }
            }

            return list;
        }

        public static List<string> GetNames(string name)
        {
            var list = new List<string>();
            var organizations = OrganizationsWrapper.CurrentTable();

            list.Add(TrimName(name));
            foreach (DataRow row in organizations.Table.Rows)
            {
                name = row.Field<string>("f_org_name");
                if (!list.Contains(name) &&
                    !string.IsNullOrEmpty(name) &&
                    row.Field<string>("f_deleted") != CommonHelper.BoolToString(true))
                {
                    list.Add(TrimName(name));
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
                 orgs.Field<int?>("f_syn_id") == 0 &&
                 orgs.Field<string>("f_deleted") != CommonHelper.BoolToString(true)
                 select new Organization
                 {
                     Id = orgs.Field<int>("f_org_id"),
                     Type = orgs.Field<string>("f_org_type"),
                     Name = UntrimName(orgs.Field<string>("f_org_name")),
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

        public static string GenerateFullName(int id, bool calculated = false)
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
                    Name = UntrimName(orgs.Field<string>("f_org_name")),
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
                    SynId = orgs.Field<int>("f_syn_id"),
	                IsBasic = CommonHelper.StringToBool(orgs.Field<string>("f_is_basic"))
				}).FirstOrDefault();

            return GenerateFullName(org, calculated);
        }

	    public static Organization GetOrganization(int id, bool calculated = false)
	    {
		    if (id <= 0)
		    {
			    return null;
		    }

		    var org = (from orgs in OrganizationsWrapper.CurrentTable().
				    Table.AsEnumerable()
			    where orgs.Field<int>("f_org_id") == id
			    select new Organization
			    {
				    Id = orgs.Field<int>("f_org_id"),
				    Type = orgs.Field<string>("f_org_type"),
				    Name = UntrimName(orgs.Field<string>("f_org_name")),
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
				    SynId = orgs.Field<int>("f_syn_id"),
				    IsBasic = CommonHelper.StringToBool(orgs.Field<string>("f_is_basic"))
				}).FirstOrDefault();

		    return org;
	    }

	    public static bool GetBasicParametr(int id, bool calculated = false)
	    {
		    if (id <= 0)
		    {
			    return false;
		    }

		    var org = (from orgs in OrganizationsWrapper.CurrentTable().
				    Table.AsEnumerable()
			    where orgs.Field<int>("f_org_id") == id
			    select new Organization
			    {
				    Id = orgs.Field<int>("f_org_id"),
				    Type = orgs.Field<string>("f_org_type"),
				    Name = UntrimName(orgs.Field<string>("f_org_name")),
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
				    SynId = orgs.Field<int>("f_syn_id"),

				    IsBasic = CommonHelper.StringToBool(orgs.Field<string>("f_is_basic"))
			    }).FirstOrDefault();

		    return org.IsBasic;
	    }

		public static string GenerateFullName(Organization organization)
        {
            var sb = new StringBuilder();
            sb.Append(organization.Type);
            sb.Append(" ");
            sb.Append(UntrimName(organization.Name));
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

        public static string TrimName(string name)
        {
            return name != null ? name.Trim(new[] { '\"', '\'' }) : "";
        }

        public static string UntrimName(string name)
        {
            return name != null ? "\"" + TrimName(name) + "\"" : "\"\"";
        }

        public static bool FullNameEnabled(int id)
        {
            return id == 0 ? true :
                !(from orgs in OrganizationsWrapper.CurrentTable().
                 Table.AsEnumerable()
                  where orgs.Field<int?>("f_syn_id") == id
                  select orgs.Field<int?>("f_syn_id")).Any();
        }

        public static KeyValuePair<string, Dictionary<int, string>> GetSynonims(Organization org)
        {
            string fullName = GenerateFullName(org, org.SynId == 0);
            var synonims =
                (from orgs in OrganizationsWrapper.CurrentTable().
                    Table.AsEnumerable()
                 where orgs.Field<int>("f_syn_id") ==
                 (org.SynId == 0 ? org.Id : org.SynId) &&
                 orgs.Field<int>("f_org_id") != org.Id
                 select new Organization
                 {
                     Id = orgs.Field<int>("f_org_id"),
                     Type = orgs.Field<string>("f_org_type"),
                     Name = UntrimName(orgs.Field<string>("f_org_name")),
                     CountryId = orgs.Field<int>("f_cntr_id"),
                     Country = orgs.Field<int>("f_cntr_id") == 0 ?
                            "" : CountriesWrapper.CurrentTable()
                            .Table.AsEnumerable().FirstOrDefault(
                            arg => arg.Field<int>("f_cntr_id") ==
                            orgs.Field<int>("f_cntr_id"))["f_cntr_name"].
                            ToString(),
                     RegionId = orgs.Field<int>("f_region_id"),
                     Region = orgs.Field<int>("f_region_id") == 0 ?
                            "" : RegionsWrapper.CurrentTable()
                            .Table.AsEnumerable().FirstOrDefault(
                            arg => arg.Field<int>("f_region_id") ==
                            orgs.Field<int>("f_region_id"))["f_region_name"].
                            ToString(),
                     SynId = orgs.Field<int>("f_syn_id")
                 }).ToDictionary(o => o.Id, o => GenerateFullName(o));
           
            return new KeyValuePair<string, Dictionary<int, string>>(fullName, synonims);
        }

        private static string GenerateFullName(Organization organization, bool calculated)
        {
            if (organization == null)
            {
                return "";
            }

            if (organization.SynId == 0)
            {
                return !calculated ? "" :
                    GenerateFullName(organization);
            }
            else
            {
                return GenerateFullName(organization.SynId, true);
            }
        }
    }
}
