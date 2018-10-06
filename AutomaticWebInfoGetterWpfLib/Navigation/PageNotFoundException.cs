using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticWebInfoGetterWpfLib.Navigation
{
    class PageNotFoundException: Exception
    {
        public PageNotFoundException(): base("Page not found")
        {

        }

        public PageNotFoundException(string message): base(message)
        {

        }
    }
}
