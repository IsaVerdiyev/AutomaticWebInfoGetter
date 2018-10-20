using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticWebInfoGetterWpfLib.Services.WebInfoGetter
{
    public interface IWebInfoGetter
    {
        string GetStringOfNodeByXPathFromUrl(string url, string xpath);
        List<string> GetStringsOfNodesByXPathFromUrl(string url, string xpath);
    }
}
