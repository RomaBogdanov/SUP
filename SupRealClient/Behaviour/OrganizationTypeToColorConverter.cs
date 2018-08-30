using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;

namespace SupRealClient.Behaviour
{
	public class OrganizationTypeToColorConverter : IValueConverter
	{
		SolidColorBrush standartColorBrush = new SolidColorBrush(Colors.Black);
		SolidColorBrush unstandartColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#007494"));
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool)
			{
				if ((bool)value)
				{
					return unstandartColorBrush;
				}
			}

			return standartColorBrush;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is Brush)
			{
				if ((SolidColorBrush)value == unstandartColorBrush)
				{
					return true;
				}
			}

			return false;
		}
	}
}

