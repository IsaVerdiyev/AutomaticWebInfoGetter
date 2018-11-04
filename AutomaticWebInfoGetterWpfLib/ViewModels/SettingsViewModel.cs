using AutomaticWebInfoGetterWpfLib.Messages;
using AutomaticWebInfoGetterWpfLib.Models;
using AutomaticWebInfoGetterWpfLib.Navigation;
using AutomaticWebInfoGetterWpfLib.Services.Storage;
using AutomaticWebInfoGetterWpfLib.Services.TimerInitializer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
                RaisePropertyChanged(nameof(StartDate));
                RaisePropertyChanged(nameof(EndDate));
                RaisePropertyChanged(nameof(DelayBetweenQueries));
                RaisePropertyChanged(nameof(HorizontalOrientation));
                SelectedDownloadedPart = DownloadedPartOfPageSettingInfos?.First();
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

        private DownloadedPartOfPageSettingInfo selectedDownloadedPart;

        public DownloadedPartOfPageSettingInfo SelectedDownloadedPart
        {
            get { return selectedDownloadedPart; }
            set
            {
                Set(ref selectedDownloadedPart, value);
                RaisePropertyChanged(nameof(Header));
                RaisePropertyChanged(nameof(StartRow));
                RaisePropertyChanged(nameof(StartColumn));
            }
        }


        public string Header { get => SelectedDownloadedPart?.Header; }

        public int? StartRow { get => SelectedDownloadedPart?.StartPositionOfWriting.Row; }

        public int? StartColumn { get => SelectedDownloadedPart?.StartPositionOfWriting.Column; }


        public DateTime StartDate { get => SelectedSettingInfo != null ? SelectedSettingInfo.TimeInfo.StartDate : DateTime.Now; }

        public DateTime EndDate { get => SelectedSettingInfo != null ? SelectedSettingInfo.TimeInfo.EndDate : DateTime.Now.AddDays(1); }

        public TimeSpan? DelayBetweenQueries { get => SelectedSettingInfo != null ? SelectedSettingInfo.TimeInfo.DelayBetweenQueries : TimeSpan.FromMinutes(1); }

        public bool HorizontalOrientation { get => SelectedSettingInfo != null ? SelectedSettingInfo.HorizontalOrientationOfWritingInfo : false; }

        #endregion

        #region Dependencies

        INavigationService navigationService;

        ITimerInitializer timerInitializer;

        #endregion

        #region Messages

        AddSettingViewModelInitializeMessage addSettingViewModelInitialize = new AddSettingViewModelInitializeMessage();

        #endregion

        #region Constructor

        public SettingsViewModel(INavigationService navigationService, ITimerInitializer timerInitializer)
        {
            this.navigationService = navigationService;
            this.timerInitializer = timerInitializer;

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


        private RelayCommand<SettingsInfo> runStopTimerCommand;

        public RelayCommand<SettingsInfo> RunStopTimerCommand
        {
            get
            {
                return runStopTimerCommand ?? (runStopTimerCommand = new RelayCommand<SettingsInfo>((obj) =>
                {
                    SettingsInfo settingsInfo = (SettingsInfo)obj;
                    if(settingsInfo.TimerState == TimerStateEnum.Running)
                    {
                        timerInitializer.StopTimer(settingsInfo);
                    }
                    else if (settingsInfo.TimerState == TimerStateEnum.Stopped)
                    {
                        timerInitializer.ActivateStoppedTimer(settingsInfo);
                    }
                }
                , (obj) => ((SettingsInfo)obj).TimerState != TimerStateEnum.Finished));
            }
        }


        #endregion
    }
}