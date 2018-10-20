using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticWebInfoGetterWpfLib.Services.Storage
{
    class StorageGetter
    {
        public static IStorage Storage { get => FileStorage.Storage; }
    }
}
