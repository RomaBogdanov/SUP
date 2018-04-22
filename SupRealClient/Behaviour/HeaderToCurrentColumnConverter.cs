using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace SupRealClient.Behaviour
{
    // TODO - потом реализовать по-другому
    public class HeaderToCurrentColumnConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var columnHeader = values[0] as DataGridColumnHeader;
            var dataGrid = values[1] as DataGrid;
            if (columnHeader != null && dataGrid != null)
            {
                dataGrid.CurrentColumn = columnHeader.Column;
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
