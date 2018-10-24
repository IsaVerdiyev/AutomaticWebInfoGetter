using AutomaticWebInfoGetterWpfLib.Messages;
using AutomaticWebInfoGetterWpfLib.Models;
using AutomaticWebInfoGetterWpfLib.Navigation;
using AutomaticWebInfoGetterWpfLib.Services.Storage;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutomaticWebInfoGetterWpfLib.ViewModels
{
    class SettingsViewModel : ViewModelBase
    {
        #region Fields and Properties

        IStorage storage = StorageGetter.Storage;


        public ObservableCollection<SettingsInfo> SettingInfos
        {
            get => storage.SettingInfos;
            set
            {
                storage.SettingInfos = value;
                RaisePropertyChanged();
            }
        }

        SettingsInfo selectedSettingInfo;
        public SettingsInfo SelectedSettingInfo
        {
            get => selectedSettingInfo;
            set
            {
                Set(ref selectedSettingInfo, value);
                RaisePropertyChanged(nameof(Url));
                RaisePropertyChanged(nameof(DownloadedPartOfPageSettingInfos));
                RaisePropertyChanged(nameof(IsSingleNode));
                RaisePropertyChanged(nameof(StartDate));
                RaisePropertyChanged(nameof(EndDate));
                RaisePropertyChanged(nameof(DelayBetweenQueries));
                RaisePropertyChanged(nameof(HorizontalOrientation));
            }
        }

        public string[] DelayMeasures { get => Enum.GetNames(typeof(DelayMeasuresEnum)); }

        private string selectedDelayMeasure;

        public string SelectedDelayMeasure
        {
            get { return selectedDelayMeasure; }
            set { Set(ref selectedDelayMeasure, value); }
        }

        private Visibility settingInfoVisibility;

        public Visibility SettingInfoVisibility
        {
            get { return settingInfoVisibility; }
            set { Set(ref settingInfoVisibility, value); }
        }

        public string Url { get => SelectedSettingInfo?.URL; }

        public ObservableCollection<DownloadedPartOfPageSettingInfo> DownloadedPartOfPageSettingInfos
        {
            get => SelectedSettingInfo?.SettingInfosOfDownloadedPartsOfPage;
        }

        public bool IsSingleNode { get => SelectedSettingInfo != null ? SelectedSettingInfo.SingleNode : false; }

        public DateTime StartDate { get => SelectedSettingInfo != null ? SelectedSettingInfo.TimeInfo.StartDate : DateTime.Now; }

        public DateTime EndDate { get => SelectedSettingInfo != null ? SelectedSettingInfo.TimeInfo.EndDate : DateTime.Now.AddDays(1); }

        public TimeSpan? DelayBetweenQueries { get => SelectedSettingInfo != null ? SelectedSettingInfo.TimeInfo.DelayBetweenQueries : TimeSpan.FromMinutes(1); }

        public bool HorizontalOrientation { get => SelectedSettingInfo != null ? SelectedSettingInfo.HorizontalOrientationOfWritingInfo : false; }

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
            Messenger.Default.Register<SettingsViewModelInitializeMessage>(this, obj => SwitchInitialStateCommand.Execute(obj));
        }

        #endregion

        #region commands
        private RelayCommand<VM> addSettingCommand;

        public RelayCommand<VM> AddSettingCommand
        {
            get
            {
                return addSettingCommand ?? (addSettingCommand = new RelayCommand<VM>(par =>
                {
                    navigationService.NavigateTo(par);
                    Messenger.Default.Send<AddSettingViewModelInitializeMessage>(addSettingViewModelInitialize);
                }));
            }
        }

        private RelayCommand switchInitialStateCommand;

        public RelayCommand SwitchInitialStateCommand

        {
            get
            {
                return switchInitialStateCommand ?? (switchInitialStateCommand = new RelayCommand(
                    () =>
                    {
                        SelectedSettingInfo = SettingInfos.FirstOrDefault();
                        SettingInfoVisibility = (SelectedSettingInfo == null) ? Visibility.Collapsed : Visibility.Visible;
                        SelectedDelayMeasure = DelayMeasures.First(i => i == DelayMeasuresEnum.Minutes.ToString());
                    }));
            }
        }


        #endregion
    }
}