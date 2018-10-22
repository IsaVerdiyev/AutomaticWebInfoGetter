using AutomaticWebInfoGetterWpfLib.Models;
using AutomaticWebInfoGetterWpfLib.Services.WebInfoGetter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutomaticWebInfoGetterWpfLib.Services.TimerInitializer
{
    interface ITimerInitializer
    {
        void InitializeTimer(SettingsInfo settingsInfo, IWebInfoGetter webInfogetter);
    }
}
