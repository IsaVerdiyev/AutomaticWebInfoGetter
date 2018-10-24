using AutomaticWebInfoGetterWpfLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AutomaticWebInfoGetterWpfLib.Services.WriterService
{
    interface IWriter
    {
        void WriteToExcel(List<String> info, SettingsInfo settingsInfo, DownloadedPartOfPageSettingInfo downloadedPart);
        void WriteToExcel(string info, SettingsInfo settingsInfo, DownloadedPartOfPageSettingInfo downloadedPart);
    }
}