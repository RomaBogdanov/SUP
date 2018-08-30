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
		SolidColorBrush BaseOrganization_ColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00825b"));
		SolidColorBrush NotBaseOrganization_ColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#998800"));
		SolidColorBrush SynOrganization_ColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#949494"));

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
							return BaseOrganization_ColorBrush;
						}
					}

					return NotBaseOrganization_ColorBrush;
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
