using System;
using System.Globalization;
using System.Windows.Data;

namespace SupRealClient.Behaviour
{
    public class SupDateConverter : IValueConverter
    {
        private const string FormatString = "dd.MM.yyyy";

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value != null && (DateTime)value != DateTime.MinValue)
            {
                return ((DateTime)value).ToString(FormatString, culture);
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            try
            {
                return DateTime.ParseExact((string)value, FormatString, culture);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
    }
}
