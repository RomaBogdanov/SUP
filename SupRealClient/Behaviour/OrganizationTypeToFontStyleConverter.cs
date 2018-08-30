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
	public class OrganizationTypeToFontStyleConverter : IValueConverter
	{
		//FontStyle="Italic"
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool)
			{
				if ((bool)value)
				{
					return FontStyles.Italic;
				}
			}

			return FontStyles.Normal;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
