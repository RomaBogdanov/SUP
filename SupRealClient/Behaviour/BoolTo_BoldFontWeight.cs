using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SupRealClient.Behaviour
{
	public class BoolTo_BoldFontWeight:IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool)
			{
				if ((bool) value)
				{
					return FontWeights.Bold;
				}
			}

			return SystemFonts.MessageFontWeight;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is System.Windows.FontWeight)
			{
				if ((System.Windows.FontWeight)value== FontWeights.Bold)
				{
					return true;
				}
			}

			return false;
		}
	}
}
