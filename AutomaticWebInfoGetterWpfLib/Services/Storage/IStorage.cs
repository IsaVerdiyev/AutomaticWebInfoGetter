﻿using AutomaticWebInfoGetterWpfLib.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticWebInfoGetterWpfLib.Services.Storage
{
    interface IStorage
    {
        ObservableCollection<SettingsInfo> SettingInfos { get; set; }
    }
}
