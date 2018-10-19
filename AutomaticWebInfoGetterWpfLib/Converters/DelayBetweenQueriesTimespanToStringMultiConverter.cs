using AutomaticWebInfoGetterWpfLib.Exceptions;
using AutomaticWebInfoGetterWpfLib.Models;
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
    class DelayBetweenQueriesTimespanToStringMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if((Visibility)values[2] == Visibility.Collapsed)
            {
                return 0;
            }
            TimeSpan delayBetweenQueries = (TimeSpan)values[0];
            string selectedDelayMeasure = (string)values[1];
            if(selectedDelayMeasure == DelayMeasuresEnum.Seconds.ToString())
            {
                return delayBetweenQueries.TotalSeconds.ToString();
            }
            else if (selectedDelayMeasure == DelayMeasuresEnum.Minutes.ToString())
            {
                return delayBetweenQueries.TotalMinutes.ToString();
            }
            else if(selectedDelayMeasure == DelayMeasuresEnum.Hours.ToString())
            {
                return delayBetweenQueries.TotalHours.ToString();
            }
            else if (selectedDelayMeasure == DelayMeasuresEnum.Days.ToString())
            {
                return delayBetweenQueries.TotalDays.ToString();
            }
            else
            {
                throw new NoSuchTypeInDelayMeasuresEnum();
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
