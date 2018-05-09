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

        public static string CreateFullName(string family, string name,
            string secondName)
        {
            return family.Trim() + " " + name.Trim().Substring(0, 1) + ". " +
                secondName.Trim().Substring(0, 1) + ".";
        }

        public static bool IsSearcConditionMatch(string value, string searching)
        {
            // TODO - плохая реализация, для названий организаций в кавычках
            return value != null &&
                (value.ToUpper().StartsWith(searching.ToUpper()) ||
                value.ToUpper().StartsWith("\"" + searching.ToUpper()));
        }
    }
}
