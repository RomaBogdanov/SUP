using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
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
	        if (value == null)
	        {
		        return false;
	        }
            return value.ToUpper() == "Y";
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

	    public static string Check_FamilyNamePatronymic(string text)
	    {
		    if (string.IsNullOrEmpty(text))
		    {
			    return "";
			}

		    var match = Regex.Match(text, @"^\s*([а-яёa-z|!|@|#|$|%|^|&|*|(|)|_|+|-|=|:|?|/|'|<|>|0-9]+(\s*([-–——]\s*)?[а-яёa-z|!|@|#|$|%|^|&|*|(|)|_|+|-|=|:|?|/|'|<|>|0-9]+)*)*\s*$", RegexOptions.IgnoreCase);
		    if (!match.Success)
		    {
			    return "";
		    }

		    var result = match.Groups[1].Value;
		    result = Regex.Replace(result, @"\s+", " ");
		    result = Regex.Replace(result, @"\s*[-–——]\s*", "-");
		    result = Regex.Replace(result, @"[!|@|#|$|%|^|&|*|(|)|_|+|-|=|:|?|/|'|<|>]", "");
		    result = Regex.Replace(result, @"[0-9]*", "");
			return result;
	    }

	    public static string Check_NumberDocument(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return "";
			}

			var match = Regex.Match(text, @"^\s*([\d|!|@|#|$|%|^|&|*|(|)|_|+|-|=|:|?|/|'|<|>|а-яёa-z]+(\s*([-–——]\s*)?[\d|!|@|#|$|%|^|&|*|(|)|_|+|-|=|:|?|/|'|<|>|а-яёa-z]+)*)\s*$", RegexOptions.IgnoreCase);

			if (!match.Success)
		    {
			    return "";
		    }

		    var result = match.Groups[1].Value;
		    result = Regex.Replace(result, @"\s+", " ");
		    result = Regex.Replace(result, @"\s*[-–——]\s*", " - ");
		    result = Regex.Replace(result, @"[!|@|#|$|%|^|&|*|(|)|_|+|-|=|:|?|/|'|<|>]", "");
			result = Regex.Replace(result, @"[а-яёa-z]", "");
			return result;

		}

		public static string Check_SeriaCode(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return "";
			}

			var match = Regex.Match(text, @"^\s*([\w|!|@|#|$|%|^|&|*|(|)|_|+|-|=|:|?|/|'|<|>]+(\s*([-–——]\s*)?[\w|!|@|#|$|%|^|&|*|(|)|_|+|-|=|:|?|/|'|<|>]+)*)\s*$", RegexOptions.IgnoreCase);
		    if (!match.Success)
		    {
			    return "";
		    }

		    var result = match.Groups[1].Value;
		    result = Regex.Replace(result, @"\s+", " ");
		    result = Regex.Replace(result, @"\s*[-–——]\s*", " - ");
		    result = Regex.Replace(result, @"[!|@|#|$|%|^|&|*|(|)|_|+|-|=|:|?|/|'|<|>]", "");
			return result;
	    }

	    /// <summary>
	    /// Тип документа на русском языке.
	    /// </summary>
	    /// <param name="documentType"></param>
	    /// <returns></returns>
	    public static string GetDocumentTypeInRussian(string documentType)
	    {
		    if (string.IsNullOrEmpty(documentType))
		    {
			    return null;
		    }

		    if (documentType.ToLower().Contains("epassport"))
		    {
			    return "Заграничный паспорт";
		    }

		    if (documentType.ToLower().Contains("passport"))
		    {
			    return "Паспорт";
		    }

		    if (documentType.ToLower().Contains("driving license"))
		    {
			    return "Водительское удостоверение";
		    }

		    if (documentType.ToLower().Contains("visa"))
		    {
			    return "Виза";
		    }

		    if (documentType.ToLower().Contains("snils"))
		    {
			    return "СНИЛС";
		    }

		    if (documentType.ToLower().Contains("сertificate of birth"))
		    {
			    return "Свидетельство о рождении";
		    }

		    return null;
	    }
	}
}
