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
			if(!string.IsNullOrEmpty(secondName) && !string.IsNullOrWhiteSpace(secondName))
				return family.Trim() + " " + name.Trim().Substring(0, 1) + ". " +
                secondName.Trim().Substring(0, 1) + ".";
			else
				return family.Trim() + " " + name.Trim().Substring(0, 1) ;
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

			var match = Regex.Match(text, @"^\s*([\w|\W|\d]+(\s*([-–——-]\s*)?[\w|\W|\d]+)*)*\s*$", RegexOptions.IgnoreCase);
			if (!match.Success)
		    {
			    return "";
		    }

		    var result = match.Groups[1].Value;
		    result = Regex.Replace(result, @"\s+", " ");
			result = Regex.Replace(result, @"[^\s-\w]", "");
			result = Regex.Replace(result, @"[\d]*", "");
			result = Regex.Replace(result, @"(\s*[-–——-]\s*)+", "-");





			if (string.IsNullOrEmpty(result))
			{
				return "";
			}

			match = Regex.Match(result, @"^\s*([\w|\W|\d]+(\s*([-–——-]\s*)?[\w|\W|\d]+)*)*\s*$", RegexOptions.IgnoreCase);
			if (!match.Success)
			{
				return "";
			}

			return match.Groups[1].Value;
	    }

	    public static string Check_NumberDocument(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return "";
			}
			string str = "";
			var match = Regex.Match(text, @"^\s*([\w|\W|\d]+(\s*([-–——-]\s*)?[\w|\W|\d]+)*)\s*$", RegexOptions.IgnoreCase);

			if (!match.Success)
		    {
			    return "";
		    }

		    var result = match.Groups[1].Value;
		    result = Regex.Replace(result, @"\s+", " ");
			////result = Regex.Replace(result, @"\s*[-–——]\s*", " - ");
			//result = Regex.Replace(result, @"[!|@|#|$|%|^|&|*|(|)|_|+|-|=|:|?|/|'|<|>|,|;]", "");
			result = Regex.Replace(result, @"[^\s-\d]", "");
			//result = Regex.Replace(result, @"[а-яёa-z]", "");
			result = Regex.Replace(result, @"(\s*[-–——-]\s*)+", "-");






			if (string.IsNullOrEmpty(result))
			{
				return "";
			}

			match = Regex.Match(result, @"^\s*([\w|\W|\d]+(\s*([-–——-]\s*)?[\w|\W|\d]+)*)\s*$", RegexOptions.IgnoreCase);

			if (!match.Success)
			{
				return "";
			}

			return  match.Groups[1].Value;

		}

		public static string Check_SeriaCode(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return "";
			}

			var match = Regex.Match(text, @"^\s*([\w|\W|\d]+(\s*([-–——-]\s*)?[\w|\W|\d]+)*)\s*$", RegexOptions.IgnoreCase);
		    if (!match.Success)
		    {
			    return "";
		    }

		    var result = match.Groups[1].Value;
		    result = Regex.Replace(result, @"\s+", " ");
			////result = Regex.Replace(result, @"\s*[-–——]\s*", " - ");
			//result = Regex.Replace(result, @"[!|@|#|$|%|^|&|*|(|)|_|+|-|=|:|?|/|'|<|>|,|;]", "");
			result = Regex.Replace(result, @"[^\s-\w]", "");
			result = Regex.Replace(result, @"(\s*[-–——-]\s*)+", "-");





			if (string.IsNullOrEmpty(result))
			{
				return "";
			}

			//match = Regex.Match(result, @"^\s*([\w|!|@|#|$|%|^|&|*|(|)|_|+|-|=|:|?|/|'|<|>|,|;]+(\s*([-–——]\s*)?[\w|!|@|#|$|%|^|&|*|(|)|_|+|-|=|:|?|/|'|<|>|,|;]+)*)\s*$", RegexOptions.IgnoreCase);
			match = Regex.Match(result, @"^\s*([\w|\W|\d]+(\s*([-–——-]\s*)?[\w|\W|\d]+)*)\s*$", RegexOptions.IgnoreCase);
			if (!match.Success)
			{
				return "";
			}

			return match.Groups[1].Value;
		}

		public static string Check_Position(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return "-";
			}

			//var match = Regex.Match(text, @"^\s*([\w|!|@|#|$|%|^|&|*|(|)|_|+|-|=|:|?|/|'|<|>|,|;]+(\s*([-–——]\s*)?[\w|!|@|#|$|%|^|&|*|(|)|_|+|-|=|:|?|/|'|<|>|,|;]+)*)\s*$", RegexOptions.IgnoreCase);		
			var match = Regex.Match(text, @"^\s*([\w|\W|\d]+(\s*([-–——-]\s*)?[\w|\W|\d]+)*)\s*$", RegexOptions.IgnoreCase);
			if (!match.Success)
			{
				return "-";
			}

			var result = match.Groups[1].Value;
			result = Regex.Replace(result, @"\s+", "");
			//result = Regex.Replace(result, @"\s*[-–——]\s*", "-");
			result = Regex.Replace(result, @"(\s*[-–——-]\s*)+", "-");
			result = Regex.Replace(result, @"[^\s-\w]", "");







			if (string.IsNullOrEmpty(result))
			{
				return "-";
			}

			//match = Regex.Match(result, @"^\s*([\w|!|@|#|$|%|^|&|*|(|)|_|+|-|=|:|?|/|'|<|>|,|;]+(\s*([-–——]\s*)?[\w|!|@|#|$|%|^|&|*|(|)|_|+|-|=|:|?|/|'|<|>|,|;]+)*)\s*$", RegexOptions.IgnoreCase);
			match = Regex.Match(result, @"^\s*([\w|\W|\d]+(\s*([-–——-]\s*)?[\w|\W|\d]+)*)\s*$", RegexOptions.IgnoreCase);
			if (!match.Success)
			{
				return "-";
			}

			return match.Groups[1].Value;
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
