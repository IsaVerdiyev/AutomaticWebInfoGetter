using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticWebInfoGetterWpfLib.Models
{
    enum DelayMeasuresEnum
    {
        Seconds,
        Minutes,
        Hours,
        Days
    }

    class ActionExecutionTimeInfo
    {
        DateTime startDate;
        public DateTime StartDate { get => startDate; set => startDate = value; }

        TimeSpan delayBetweenQueries;
        public TimeSpan DelayBetweenQueries { get => delayBetweenQueries; set => delayBetweenQueries = value; }

        DateTime endDate;
        public DateTime EndDate { get => endDate; set => endDate = value; }
    
    }
}
