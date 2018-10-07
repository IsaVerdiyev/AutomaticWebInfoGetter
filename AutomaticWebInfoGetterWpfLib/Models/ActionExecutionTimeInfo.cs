using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticWebInfoGetterWpfLib.Models
{
    class ActionExecutionTimeInfo
    {
        DateTime startDate;
        public DateTime StartDate { get => startDate; set => startDate = value; }

        TimeSpan actionExecutionFrequency;
        public TimeSpan ActionExecutionFrequency { get => actionExecutionFrequency; set => actionExecutionFrequency = value; }

        DateTime endDate;
        public DateTime EndDate { get => endDate; set => endDate = value; }
    
    }
}
