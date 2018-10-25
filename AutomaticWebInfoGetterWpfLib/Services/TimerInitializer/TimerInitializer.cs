using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutomaticWebInfoGetterWpfLib.Models;
using AutomaticWebInfoGetterWpfLib.Services.WebInfoGetter;
using AutomaticWebInfoGetterWpfLib.Services.WriterService;

namespace AutomaticWebInfoGetterWpfLib.Services.TimerInitializer
{
    class TimerInitializer : ITimerInitializer
    {

        public void InitializeTimer(SettingsInfo settingsInfo, IWebInfoGetter webInfogetter, IWriter writer)
        {
            DateTime whenToStart = settingsInfo.TimeInfo.StartDate;
            while (whenToStart < DateTime.Now && whenToStart < settingsInfo.TimeInfo.EndDate)
            {
                whenToStart = whenToStart.Add(settingsInfo.TimeInfo.DelayBetweenQueries);
            }

            if (whenToStart > settingsInfo.TimeInfo.EndDate)
            {
                return;
            }

            settingsInfo.Timer = new Timer(o1 => Task.Run(() =>
            {
                foreach(var item in settingsInfo.SettingInfosOfDownloadedPartsOfPage)
                {
                    if (settingsInfo.SingleNode)
                    {
                        writer.WriteToExcel(webInfogetter.GetStringOfNodeByXPathFromUrl(settingsInfo.URL, item.XPath), settingsInfo, item);
                    }
                    else
                    {
                        writer.WriteToExcel(webInfogetter.GetStringsOfNodesByXPathFromUrl(settingsInfo.URL, item.XPath), settingsInfo, item);
                    }
                }
                whenToStart = whenToStart.Add(settingsInfo.TimeInfo.DelayBetweenQueries);
                if (whenToStart < settingsInfo.TimeInfo.EndDate)
                {
                    TimeSpan timeDifference = whenToStart - DateTime.Now;
                    settingsInfo.Timer.Change((int)timeDifference.TotalMilliseconds, Timeout.Infinite);
                }


            }), null, Timeout.Infinite, Timeout.Infinite);
            settingsInfo.Timer.Change((whenToStart - settingsInfo.TimeInfo.StartDate).Milliseconds, Timeout.Infinite);


        }
    }
}
