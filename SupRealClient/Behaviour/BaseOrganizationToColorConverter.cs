using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace SupRealClient.Behaviour
{
	class BaseOrganizationToColorConverter : IValueConverter
	{
		SolidColorBrush standartColorBrush = new SolidColorBrush(Colors.Black);
		SolidColorBrush unstandartColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#565656"));
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is int)
			{
				if ((int)value <= 0)
				{
					return standartColorBrush;
				}
				else
				{
					return unstandartColorBrush;
				}
			}

			return standartColorBrush;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return 0;
		}
	}
}
