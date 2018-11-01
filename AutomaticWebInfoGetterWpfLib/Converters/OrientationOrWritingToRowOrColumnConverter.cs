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
    class OrientationOrWritingToRowOrColumnConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (var item in values)
            {
                if (item == DependencyProperty.UnsetValue || item == null)
                {
                    return "";
                }
            }

                bool isHorizontalOrientation = (bool)values[0];
                bool isGlobalStartPosition = (bool)values[1];

            if ((isGlobalStartPosition && isHorizontalOrientation) ||
                (!isGlobalStartPosition && !isHorizontalOrientation))
            {
                return "Column: ";
            }
            return "Row: ";
            
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
