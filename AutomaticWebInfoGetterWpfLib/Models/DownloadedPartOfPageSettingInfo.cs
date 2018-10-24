using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AutomaticWebInfoGetterWpfLib.Models
{
    class DownloadedPartOfPageSettingInfo
    {
        private string xpath;
        public string XPath
        {
            get { return xpath; }
            set { xpath = value; }
        }
        private string header;
        public string Header
        {
            get { return header; }
            set { header = value; }
        }
        private Position startPositionOfWriting;
        public Position StartPositionOfWriting
        {
            get { return startPositionOfWriting; }
            set { startPositionOfWriting = value; }
        }
        private Position currentWritingPosition;
        public Position CurrentWritingPosition
        {
            get { return currentWritingPosition; }
            set { currentWritingPosition = value; }
        }
    }
}