using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutomaticWebInfoGetterWpfLib.Models
{
    class SettingsInfo
    {
        string url;
        public string URL { get => url; set => url = value; }

        bool singleNode;
        public bool SingleNode { get => singleNode; set => singleNode = value; }

        ActionExecutionTimeInfo timeInfo;
        public ActionExecutionTimeInfo TimeInfo { get => timeInfo; set => timeInfo = value; }

        private Timer timer;
        public Timer Timer { get => timer; set => timer = value; }


        private ObservableCollection<DownloadedPartOfPageSettingInfo> settingInfosOfDownloadedPartsOfPage;
        public ObservableCollection<DownloadedPartOfPageSettingInfo> SettingInfosOfDownloadedPartsOfPage
        {
            get => settingInfosOfDownloadedPartsOfPage;
            set => settingInfosOfDownloadedPartsOfPage = value;
        }


        private bool horizontalOrientationOfWritingInfo;
        public bool HorizontalOrientationOfWritingInfo
        {
            get => horizontalOrientationOfWritingInfo;
            set => horizontalOrientationOfWritingInfo = value;
        }

        private int betweenLineDistance;
        public int BetweenLineDistance { get => betweenLineDistance; set => betweenLineDistance = value; }

        private int betweenWritingNewInfoDistance;
        public int BetweenWritingNewInfoDistance
        {
            get => betweenWritingNewInfoDistance;
            set => betweenWritingNewInfoDistance = value;
        }

        private string nameOfFileToWriteInfo;
        public string NameOfFileToWriteInfo
        {
            get => nameOfFileToWriteInfo;
            set => nameOfFileToWriteInfo = value;
        }


    }
}