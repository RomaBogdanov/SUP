using System;
using System.Data;
using System.Linq;

namespace SupRealClient.Common
{
    public static class CommonHelper
    {
        public static string BoolToString(bool value)
        {
            return value ? "Y" : "N";
        }

        public static bool StringToBool(string value)
        {
            return value.ToUpper() == "Y";
        }

        public static bool NotDeleted(DataRow row)
        {
            return row.Field<string>("f_deleted").ToUpper() != "Y";
        }

        public static string CreateFullName(string family, string name,
            string secondName)
        {
            return family.Trim() + " " + name.Trim().Substring(0, 1) + ". " +
                secondName.Trim().Substring(0, 1) + ".";
        }

	    public static bool IsPositionCorrect(string position)
	    {
		    return position.Any(Char.IsLetter);
	    }

        public static bool IsSearchConditionMatch(string value, string searching)
        {
            // TODO - плохая реализация, для названий организаций в кавычках
            return value != null &&
                (value.ToUpper().Replace('Ё', 'Е').StartsWith(searching.ToUpper()) ||
                value.ToUpper().Replace('Ё', 'Е').StartsWith("\"" + searching.ToUpper()));
        }
    }
}
