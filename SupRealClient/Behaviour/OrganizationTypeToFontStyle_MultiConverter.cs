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
	public class OrganizationTypeToFontStyle_MultiConverter : IMultiValueConverter
	{
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
							return FontStyles.Normal;
						}
					}

					return FontStyles.Normal;
				}
				else
				{
					return FontStyles.Italic;
				}
			}

			return FontStyles.Normal;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
