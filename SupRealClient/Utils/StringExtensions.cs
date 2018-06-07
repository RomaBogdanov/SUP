using System;
using System.Text.RegularExpressions;

namespace SupRealClient.Utils
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmptyOrWhiteSpaces(this string strA)
        {
            return string.IsNullOrEmpty(strA) || string.IsNullOrWhiteSpace(strA);
        }

        public static bool EqualsWithoutSpacesAndCase(this string strA, string strB)
        {
            if (strA == null)
                return strB.IsNullOrEmptyOrWhiteSpaces();
            if (strB == null)
                return strA.IsNullOrEmptyOrWhiteSpaces();
            return string.Compare(Regex.Replace(strA, @"\s", ""), Regex.Replace(strB, @"\s", ""),
                       StringComparison.OrdinalIgnoreCase) == 0;
        }
    }
}