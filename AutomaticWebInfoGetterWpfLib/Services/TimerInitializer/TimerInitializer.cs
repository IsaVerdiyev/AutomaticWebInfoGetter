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

                if (settingsInfo.HorizontalOrientationOfWritingInfo)
                {
                    var maxColumn = settingsInfo.SettingInfosOfDownloadedPartsOfPage.Max(p => p.CurrentWritingPosition?.Column);
                    foreach (var part in settingsInfo.SettingInfosOfDownloadedPartsOfPage)
                    {
                        if (part.CurrentWritingPosition != null)
                        {
                            part.CurrentWritingPosition.Column = maxColumn.Value;
                        }
                    }
                }
                else
                {
                    var maxRow = settingsInfo.SettingInfosOfDownloadedPartsOfPage.Max(p => p.CurrentWritingPosition?.Row);
                    foreach (var part in settingsInfo.SettingInfosOfDownloadedPartsOfPage)
                    {
                        if (part.CurrentWritingPosition != null)
                        {
                            part.CurrentWritingPosition.Row = maxRow.Value;
                        }
                    }
                }

                lock (webInfogetter)
                {
                    webInfogetter.LoadPage(settingsInfo.URL);
                    foreach (var item in settingsInfo.SettingInfosOfDownloadedPartsOfPage)
                    {

                        List<string> resultsFromInfo = webInfogetter.GetStringsOfNodesByXPathFromUrl(item.XPath);
                        if(resultsFromInfo.Count > 0)
                        {
                            writer.WriteToExcel(resultsFromInfo, settingsInfo, item);
                        }
                        else
                        {
                            writer.WriteToExcel("Not found", settingsInfo, item);
                        }
                    }
                }
                whenToStart = whenToStart.Add(settingsInfo.TimeInfo.DelayBetweenQueries);
                if (whenToStart < settingsInfo.TimeInfo.EndDate)
                {
                    TimeSpan timeDifference = whenToStart - DateTime.Now;
                    try
                    {
                        settingsInfo.Timer.Change((int)timeDifference.TotalMilliseconds, Timeout.Infinite);
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        settingsInfo.Timer.Change(0, Timeout.Infinite);
                    }
                }


            }), null, Timeout.Infinite, Timeout.Infinite);
            settingsInfo.Timer.Change((whenToStart - settingsInfo.TimeInfo.StartDate).Milliseconds, Timeout.Infinite);


        }
    }
}
