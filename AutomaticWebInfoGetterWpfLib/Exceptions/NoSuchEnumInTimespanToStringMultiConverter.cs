using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticWebInfoGetterWpfLib.Exceptions
{
    class NoSuchTypeInDelayMeasuresEnum: Exception
    {
        public NoSuchTypeInDelayMeasuresEnum()
        {

        }

        public NoSuchTypeInDelayMeasuresEnum(string message): base(message)
        {

        }
    }
}
