using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomaticWebInfoGetterWpfLib.Models;

namespace AutomaticWebInfoGetterWpfLib.Services.Storage
{
    class FileStorage : IStorage
    {
        private ObservableCollection<SettingsInfo> settingInfos = new ObservableCollection<SettingsInfo>();

        public ObservableCollection<SettingsInfo> SettingInfos {
            get => settingInfos;
            set => settingInfos = value;
        }

        static IStorage storage;

        public static IStorage Storage { get => storage ?? (storage = new FileStorage()); }

    }

}
