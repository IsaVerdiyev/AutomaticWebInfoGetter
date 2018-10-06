using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticWebInfoGetterWpfLib.Services.WebInfoGetter
{
    interface IWebInfoGetter
    {
        string GetStringOfNodeByXPath(string XPath);
        List<string> GetListOfStringsOfNodesByXPath(string XPath);
    }
}
