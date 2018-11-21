using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AutomaticWebInfoGetterWpfLib.Models
{
    class DownloadedPartOfPageSettingInfo: ObservableObject
    {
        private string xpath;
        public string XPath { get => xpath; set => Set(ref xpath, value); }

        private string header;
        public string Header { get => header; set => Set(ref header, value); }

        private Position startPositionOfWriting;
        public Position StartPositionOfWriting { get => startPositionOfWriting; set => Set(ref startPositionOfWriting, value); }

        private Position currentWritingPosition;
        public Position CurrentWritingPosition { get => currentWritingPosition; set => Set(ref currentWritingPosition, value); }


        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(Header))
            {
                return XPath;
            }
            else
            {
                return $"{XPath} - {Header}";
            }
        }
    }
}