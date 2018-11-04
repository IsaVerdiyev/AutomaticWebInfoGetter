using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutomaticWebInfoGetterWpfLib.Models
{
    enum TimerStateEnum{
        Running,
        Stopped,
        Finished
    }

    class SettingsInfo: ObservableObject
    {
        string url;
        public string URL { get => url; set => Set(ref url, value); }

        ActionExecutionTimeInfo timeInfo;
        public ActionExecutionTimeInfo TimeInfo { get => timeInfo; set => Set(ref timeInfo, value); }

        private Timer timer;
        public Timer Timer { get => timer; set => Set(ref timer, value); }


        private TimerStateEnum timerState;
        public TimerStateEnum TimerState { get => timerState; set => Set(ref timerState, value); }

        private bool queryOnlyOnce;
        public bool QueryOnlyOnce { get => queryOnlyOnce; set => Set(ref queryOnlyOnce, value); }

        private ObservableCollection<DownloadedPartOfPageSettingInfo> settingInfosOfDownloadedPartsOfPage;
        public ObservableCollection<DownloadedPartOfPageSettingInfo> SettingInfosOfDownloadedPartsOfPage
        {
            get => settingInfosOfDownloadedPartsOfPage;
            set => Set(ref settingInfosOfDownloadedPartsOfPage, value);
        }


        private bool horizontalOrientationOfWritingInfo;
        public bool HorizontalOrientationOfWritingInfo
        {
            get => horizontalOrientationOfWritingInfo;
            set => Set(ref horizontalOrientationOfWritingInfo, value);
        }

        private int betweenLineDistance;
        public int BetweenLineDistance { get => betweenLineDistance; set => Set(ref betweenLineDistance, value); }

        private int betweenWritingNewInfoDistance;
        public int BetweenWritingNewInfoDistance
        {
            get => betweenWritingNewInfoDistance;
            set => Set(ref betweenWritingNewInfoDistance, value);
        }

        private string nameOfFileToWriteInfo;
        public string NameOfFileToWriteInfo
        {
            get => nameOfFileToWriteInfo;
            set => Set(ref nameOfFileToWriteInfo, value);
        }


    }
}