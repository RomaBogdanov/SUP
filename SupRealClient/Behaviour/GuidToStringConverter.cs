using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using SupRealClient.Models;

namespace SupRealClient.Behaviour
{
    public class GuidToStringConverter:IValueConverter
    {
	    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	    {
		    return   (value is Guid) ?  ImagesHelper.GetImagePath((Guid)value ) : null;

	    }

	    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ImagesHelper.LoadImage((string)value);
		}
    }
}
