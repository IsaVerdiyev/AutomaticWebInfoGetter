using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutomaticWebInfoGetterWpfLib.Models;
using AutomaticWebInfoGetterWpfLib.Services.WebInfoGetter;

namespace AutomaticWebInfoGetterWpfLib.Services.TimerInitializer
{
    class TimerInitializer : ITimerInitializer
    {

        public void InitializeTimer(SettingsInfo settingsInfo, IWebInfoGetter webInfogetter)
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
                if (settingsInfo.SingleNode)
                {
                    File.AppendAllText("TimerResult.txt", webInfogetter.GetStringOfNodeByXPathFromUrl(settingsInfo.URL, settingsInfo.XPath));
                }
                else
                { 
                    File.AppendAllLines("TimerResult.txt", webInfogetter.GetStringsOfNodesByXPathFromUrl(settingsInfo.URL, settingsInfo.XPath));
                }
                whenToStart = whenToStart.Add(settingsInfo.TimeInfo.DelayBetweenQueries);
                if(whenToStart < settingsInfo.TimeInfo.EndDate)
                {
                    TimeSpan timeDifference = whenToStart - DateTime.Now;
                    settingsInfo.Timer.Change((int)timeDifference.TotalMilliseconds, Timeout.Infinite);
                }
                

            }), null, Timeout.Infinite, Timeout.Infinite);
            settingsInfo.Timer.Change((whenToStart - settingsInfo.TimeInfo.StartDate).Milliseconds, Timeout.Infinite);


        }
    }
}
