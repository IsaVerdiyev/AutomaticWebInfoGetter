using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace AutomaticWebInfoGetterWpfLib.Converters
{
    class MarginToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Thickness thickness = (Thickness)value;
            MarginConverterSideOptions marginSide = (MarginConverterSideOptions)parameter;

            if(marginSide == MarginConverterSideOptions.Left)
            {
                return thickness.Left;
            }
            else if (marginSide == MarginConverterSideOptions.Top)
            {
                return thickness.Top;
            }
            else if (marginSide == MarginConverterSideOptions.Right)
            {
                return thickness.Right;
            }
            else if (marginSide == MarginConverterSideOptions.Bottom)
            {
                return thickness.Bottom;
            }
            else
            {
                return new Exception("Invalid parameter passed in MarginToDoubleConverter");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
