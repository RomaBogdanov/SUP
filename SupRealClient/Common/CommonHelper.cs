using System;
using System.Data;
using System.Linq;
using SupRealClient.EnumerationClasses;

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

	    public static string CardStateToSting(CardState state)
	    {
		    switch (state)
		    {
			    case CardState.Unknown:
				    return  "Неизвестно";
			    case CardState.Active:
					return "Активен";
			    case CardState.Inactive:
					return "Неактивен";
			    case CardState.Issued:
				    return "Выдан";
			    case CardState.Lost:
				    return "Утерян";
			    case CardState.Returnded:
					return "Возвращен";
			    default:
				    return "Неизвестно";
		    }
		}

        public static bool NotDeleted(DataRow row)
        {
	        //return row.Field<string>("f_deleted").ToUpper() != "Y";
	        object value = row.Field<string>("f_deleted");
	        return value != null && value is string ? ((string)value).ToUpper() != "Y" : true;
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
