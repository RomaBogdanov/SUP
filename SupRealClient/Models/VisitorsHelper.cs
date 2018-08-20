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

	        TestingAndAdding(position, list);
            foreach (DataRow row in visitors.Table.Rows)
            {
                position = row.Field<string>("f_job");
	            TestingAndAdding(position, list);
            }
            list.Sort((p1, p2) => p1.CompareTo(p2));

            return list;
        }

	    private static void TestingAndAdding(string position, List<string> list)
		{
			if (!list.Contains(position) &&
			    !string.IsNullOrEmpty(position.Trim()))
			{
				if (string.IsNullOrWhiteSpace(position.Trim()))
					list.Add("-");
				else
					list.Add(position);
			}
		}

		public static string TestingPositionAnReturnCorrect(string position)
		{
			if (!string.IsNullOrEmpty(position))
			{
				string trimmedText = position.Trim();

				if (!string.IsNullOrEmpty(position.Trim()) && !string.IsNullOrWhiteSpace(position.Trim()))
					return position;
			}

			return "-";
		}

	}
}
