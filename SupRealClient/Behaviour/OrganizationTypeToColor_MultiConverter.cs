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
	public class OrganizationInfoToColor_MultiConverter : IMultiValueConverter
	{
		//SolidColorBrush BaseOrganization_ColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9e8e00"));
		SolidColorBrush BaseOrganization_ColorBrush = new SolidColorBrush(Colors.Black);
		//SolidColorBrush NotBaseOrganization_ColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00a600"));
		SolidColorBrush GoldColor_ColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9e8e00"));
		SolidColorBrush SynOrganization_ColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8E8E8E"));

		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			int? synId = (int?)values[0];
			bool? isBasic = (bool?)values[1];

			if (synId is int)
			{
				if (synId.Value <= 0)
				{
					if (isBasic is bool)
					{
						if (isBasic.Value)
						{
							//return BaseOrganization_ColorBrush;
							return BaseOrganization_ColorBrush;
						}
					}

					return BaseOrganization_ColorBrush;
				}
				else
				{
					return SynOrganization_ColorBrush;
				}
			}

			return SynOrganization_ColorBrush;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
