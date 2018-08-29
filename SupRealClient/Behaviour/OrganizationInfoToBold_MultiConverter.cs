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
	class OrganizationInfoToBold_MultiConverter : IMultiValueConverter
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
							return FontWeights.Bold;
						}
					}

					return SystemFonts.MessageFontWeight;
				}
				else
				{
					return SystemFonts.MessageFontWeight;
				}
			}

			return SystemFonts.MessageFontWeight;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
