using SupRealClient.TabsSingleton;
using System.Collections.Generic;
using System.Data;

namespace SupRealClient.Models
{
    public static class VisitorsHelper
    {
        public static List<string> GetPositions(string position)
        {
            var list = new List<string>();
            var visitors = VisitorsWrapper.CurrentTable();

            list.Add(position);
            foreach (DataRow row in visitors.Table.Rows)
            {
                position = row.Field<string>("f_job");
                if (!list.Contains(position) &&
                    !string.IsNullOrEmpty(position.Trim()))
                {
                    list.Add(position);
                }
            }
            list.Sort((p1, p2) => p1.CompareTo(p2));

            return list;
        }
    }
}
