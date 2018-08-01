using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace SupRealClient.Behaviour
{
	public class DocumentListElementsHeightConverter :IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values[0] is ListBox )
			{
				if ((values[0] as ListBox).Items.Count > 1)
				{
					double actualHeight = (values[0] as ListBox).ActualHeight;
					SetHeight(values.Last(), actualHeight / 2);
					return actualHeight / 2;
				}
				else
				{
					SetHeight(values.Last(), (values[0] as ListBox).ActualHeight - 6);
					return (values[0] as ListBox).ActualHeight - 6;
				}
			}
			else
			{
				SetHeight(values.Last(), 100);
				return 100;
			}
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			return null;
		}

		private void SetHeight(object textBoxControl, double heightValue)
		{
			if (textBoxControl is TextBlock && heightValue>=0)
				(textBoxControl as TextBlock).Height = heightValue;
		}
	}



	public class DocumentListElementHeightConverter : IValueConverter
	{
		public object Convert(object values, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}

		public object  ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
