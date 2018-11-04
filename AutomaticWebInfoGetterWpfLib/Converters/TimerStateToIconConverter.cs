using AutomaticWebInfoGetterWpfLib.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AutomaticWebInfoGetterWpfLib.Converters
{
    class TimerStateToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimerStateEnum timerState = (TimerStateEnum)value;
            if(timerState == TimerStateEnum.Running)
            {
                return "॥";
            }
            else if(timerState == TimerStateEnum.Stopped)
            {
                return "►";
            }
            else
            {
                return "►";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
