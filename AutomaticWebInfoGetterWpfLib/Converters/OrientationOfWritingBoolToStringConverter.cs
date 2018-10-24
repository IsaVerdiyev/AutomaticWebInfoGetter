using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
namespace AutomaticWebInfoGetterWpfLib.Converters
{
    class OrientationOfWritingBoolToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool HorizontalOrientation = (bool)value;
            if (HorizontalOrientation)
            {
                return "Horizontal writing of info";
            }
            else
            {
                return "Vertical writing of info";
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}