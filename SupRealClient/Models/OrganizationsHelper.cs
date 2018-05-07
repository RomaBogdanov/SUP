using SupRealClient.TabsSingleton;
using System.Collections.Generic;
using System.Data;

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
    }
}
