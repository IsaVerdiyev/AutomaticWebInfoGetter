﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticWebInfoGetterWpfLib.Services.WebInfoGetter
{
    public interface IWebInfoGetter
    {
        string GetStringOfNodeByXPathFromUrl(string xpath);
        List<string> GetStringsOfNodesByXPathFromUrl(string xpath);
        void LoadPage(string url);
    }
}
