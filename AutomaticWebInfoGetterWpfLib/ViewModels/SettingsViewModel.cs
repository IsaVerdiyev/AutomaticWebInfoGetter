using AutomaticWebInfoGetterWpfLib.Models;
using AutomaticWebInfoGetterWpfLib.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticWebInfoGetterWpfLib.ViewModels
{
    class SettingsViewModel: ViewModelBase
    {
        #region Fields and Properties
        List<SettingsInfo> settingsInfos;
        public List<SettingsInfo> SettingsInfos { get => settingsInfos; set => Set(ref settingsInfos, value); }

        SettingsInfo selectedSettingInfo;
        public SettingsInfo SelectedSettingInfo { get => selectedSettingInfo; set => Set(ref selectedSettingInfo, value); }

        INavigationService navigationService;
        #endregion

        #region Constructor

        public SettingsViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        #endregion

        #region commands
        private RelayCommand<VM> addSettingCommand;

        public RelayCommand<VM> AddSettingCommand
        {
            get { return addSettingCommand ?? (addSettingCommand = new RelayCommand<VM>(par => navigationService.NavigateTo(par))); }

        }

        #endregion
    }
}
