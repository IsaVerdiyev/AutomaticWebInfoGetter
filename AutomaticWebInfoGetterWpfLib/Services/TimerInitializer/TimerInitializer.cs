using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using AutomaticWebInfoGetterWpfLib.Models;
using AutomaticWebInfoGetterWpfLib.Services.WebInfoGetter;
using AutomaticWebInfoGetterWpfLib.Services.WriterService;

namespace AutomaticWebInfoGetterWpfLib.Services.TimerInitializer
{
    class TimerInitializer : ITimerInitializer
    {

        public void InitializeTimer(SettingsInfo settingsInfo, IWebInfoGetter webInfogetter, IWriter writer)
        {
            if (DateTime.Now > settingsInfo.TimeInfo.EndDate && !settingsInfo.QueryOnlyOnce)
            {
                settingsInfo.TimerState = TimerStateEnum.Finished;
                return;
            }
            settingsInfo.TimerState = TimerStateEnum.Running;

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
                if (settingsInfo.QueryOnlyOnce)
                {
                    settingsInfo.TimerState = TimerStateEnum.Finished;
                    return;
                }

                RecreateTimerIfNeededOrFinishTask(settingsInfo);

            }).ContinueWith(t => {
                MessageBoxResult result =  MessageBox.Show($"Error Occured during task.\n" +
                    $"Error content\n {t.Exception.InnerExceptions.First().Message}.\n" +
                    $"Do you want to cancel task? ", "Error" , MessageBoxButton.YesNo, MessageBoxImage.Error);
                if(result == MessageBoxResult.Yes)
                {
                    settingsInfo.TimerState = TimerStateEnum.Finished;
                }
                else
                {
                    DateTime whenToStart = DateTime.Now.Add(settingsInfo.TimeInfo.DelayBetweenQueries);
                    if (whenToStart < settingsInfo.TimeInfo.EndDate)
                    {
                        SetTimer(settingsInfo, DateTime.Now, whenToStart);
                    }
                    else
                    {
                        settingsInfo.TimerState = TimerStateEnum.Finished;
                    }
                }
            },TaskContinuationOptions.OnlyOnFaulted), null, Timeout.Infinite, Timeout.Infinite);

            SetTimer(settingsInfo, DateTime.Now, settingsInfo.TimeInfo.StartDate);
        }

        public void ActivateStoppedTimer(SettingsInfo settingsInfo)
        {
            if(DateTime.Now < settingsInfo.TimeInfo.EndDate)
            {
                settingsInfo.Timer.Change(0, Timeout.Infinite);
                settingsInfo.TimerState = TimerStateEnum.Running;
            }
            else
            {
                settingsInfo.TimerState = TimerStateEnum.Finished;
            }
        }

        public void StopTimer(SettingsInfo settingsInfo)
        {
            settingsInfo.Timer.Change(Timeout.Infinite, Timeout.Infinite);
            settingsInfo.TimerState = TimerStateEnum.Stopped;
        }

        void SetTimer(SettingsInfo settingsInfo, DateTime startDate, DateTime endDate)
        {

            TimeSpan timeDifference = endDate - startDate;
            try
            {
                settingsInfo.Timer.Change((int)timeDifference.TotalMilliseconds, Timeout.Infinite);
            }
            catch (ArgumentOutOfRangeException e)
            {
                settingsInfo.Timer.Change(0, Timeout.Infinite);
            }

        }


        void RecreateTimerIfNeededOrFinishTask(SettingsInfo settingsInfo)
        {
            DateTime whenToStart = DateTime.Now.Add(settingsInfo.TimeInfo.DelayBetweenQueries);
            if (whenToStart < settingsInfo.TimeInfo.EndDate)
            {
                SetTimer(settingsInfo, DateTime.Now, whenToStart);
            }
            else
            {
                settingsInfo.TimerState = TimerStateEnum.Finished;
            }
        }
    }
}
