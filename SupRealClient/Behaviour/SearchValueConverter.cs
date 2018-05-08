﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace SupRealClient.Behaviour
{
    public class SearchValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType,
            object parameter, CultureInfo culture)
        {
            string cellText = values[0] == null ? string.Empty :
                values[0].ToString();
            string searchText = values[1] as string;

            if (!string.IsNullOrEmpty(searchText) &&
                !string.IsNullOrEmpty(cellText))
            {
                // TODO - плохая реализация, для названий организаций в кавычках
                return cellText.ToUpper().StartsWith(searchText.ToUpper()) ||
                    cellText.ToUpper().StartsWith("\"" + searchText.ToUpper());
            }
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
