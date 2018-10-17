using AutomaticWebInfoGetterWpfLib.Messages;
using AutomaticWebInfoGetterWpfLib.Models;
using AutomaticWebInfoGetterWpfLib.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticWebInfoGetterWpfLib.ViewModels
{
    class SettingsViewModel: ViewModelBase
    {
        #region Fields and Properties

        List<SettingsInfo> settingInfos = new List<SettingsInfo>();
        public List<SettingsInfo> SettingInfos { get => settingInfos; set => Set(ref settingInfos, value); }

        SettingsInfo selectedSettingInfo;
        public SettingsInfo SelectedSettingInfo { get => selectedSettingInfo; set => Set(ref selectedSettingInfo, value); }

        #endregion

        #region Dependencies

        INavigationService navigationService;

        #endregion

        #region Messages

        AddSettingViewModelInitializeMessage addSettingViewModelInitialize = new AddSettingViewModelInitializeMessage();

        #endregion

        #region Constructor

        public SettingsViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            Messenger.Default.Register<SettingsViewModelAddSettingInfoMessage>(this, obj => SettingInfos.Add(((SettingsViewModelAddSettingInfoMessage)obj).AddedSettingInfo));
        }

        #endregion

        #region commands
        private RelayCommand<VM> addSettingCommand;

        public RelayCommand<VM> AddSettingCommand
        {
            get { return addSettingCommand ?? (addSettingCommand = new RelayCommand<VM>(par => 
            {
                navigationService.NavigateTo(par);
                Messenger.Default.Send<AddSettingViewModelInitializeMessage>(addSettingViewModelInitialize);
            })); }

        }

        #endregion
    }
}
