using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticWebInfoGetterWpfLib.Models
{
    class SettingsInfo
    {
        string url;
        public string URL { get => url; set => url = value; }

        string xpath;
        public string XPath { get => xpath; set => xpath = value; }

        bool singleNode;
        public bool SingleNode { get => singleNode; set => singleNode = value; }

        ActionExecutionTimeInfo timeInfo;
        public ActionExecutionTimeInfo TimeInfo { get => timeInfo; set => timeInfo = value; }
    }
}
