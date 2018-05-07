using SupClientConnectionLib;
using SupRealClient.TabsSingleton;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SupRealClient.Models
{
    public static class RegionsHelper
    {
        public static List<string> GetCountries(string country)
        {
            var list = new List<string>();
            var countries = CountriesWrapper.CurrentTable();

            list.Add(country);
            foreach (DataRow row in countries.Table.Rows)
            {
                country = row.Field<string>("f_cntr_name");
                if (!list.Contains(country) && !string.IsNullOrEmpty(country))
                {
                    list.Add(country);
                }
            }

            return list;
        }

        public static int AddOrUpdateCountry(string country)
        {
            if (string.IsNullOrEmpty(country.Trim()))
            {
                return 0;
            }

            var countries = CountriesWrapper.CurrentTable();
            countries.OnChanged += EmptyQuery;
            var rows = (from object r in countries.Table.Rows select r as DataRow).ToList();
            var row = rows.FirstOrDefault(
                r => r.Field<string>("f_cntr_name") == country);
            if (row == null)
            {
                row = countries.Table.NewRow();
                row["f_cntr_name"] = country;
                row["f_deleted"] = "N";
                row["f_rec_date"] = DateTime.Now;
                row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                countries.Table.Rows.Add(row);
            }

            countries.OnChanged -= EmptyQuery;

            return row.Field<int>("f_cntr_id");
        }

        private static void EmptyQuery()
        {
        }

    }
}
