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
    enum MarginConverterSideOptions
    {
        Left,
        Top,
        Right,
        Bottom
    }

    class MarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double obj = (double)value;
            if(parameter == null)
            {
                return new Exception("Invalid parameter passed in PropertyMarginConverter");
            }
            MarginConverterSideOptions par = (MarginConverterSideOptions)parameter;

            if (par == MarginConverterSideOptions.Left)
            {
                return new Thickness(-obj, 0, 0, 0);
            }
            else if (par == MarginConverterSideOptions.Top)
            {
                return new Thickness(0, -obj, 0, 0);
            }
            else if (par == MarginConverterSideOptions.Right)
            {
                return new Thickness(0, 0, -obj, 0);
            }
            else if (par == MarginConverterSideOptions.Bottom)
            {
                return new Thickness(0, 0, 0, -obj);
            }
            else
            {
                return new Exception("Invalid parameter passed in PropertyMarginConverter");
            }


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
