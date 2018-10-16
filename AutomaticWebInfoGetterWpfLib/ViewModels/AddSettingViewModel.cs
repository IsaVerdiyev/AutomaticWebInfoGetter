using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticWebInfoGetterWpfLib.ViewModels
{
    class AddSettingViewModel: ViewModelBase
    {
        #region Fields and Properties
        private string url;

        public string URL
        {
            get { return url; }
            set { Set(ref url, value); }
        }

        private string xpath;

        public string XPath
        {
            get { return xpath; }
            set { Set(ref xpath, value); }
        }

        private bool isSingleNode;

        public bool IsSingleNode
        {
            get { return isSingleNode; }
            set { Set(ref isSingleNode, value); }
        }


        private TimeSpan delayBetweenQueries;

        public TimeSpan DelayBetweenQueries
        {
            get { return delayBetweenQueries; }
            set { delayBetweenQueries = value; }
        }


        private DateTime startDate;

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }


        private DateTime endDate;

        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }


        #endregion

    }
}
