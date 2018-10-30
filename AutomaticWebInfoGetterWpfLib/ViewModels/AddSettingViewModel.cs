
using AutomaticWebInfoGetterWpfLib.Exceptions;
using AutomaticWebInfoGetterWpfLib.Messages;
using AutomaticWebInfoGetterWpfLib.Models;
using AutomaticWebInfoGetterWpfLib.Navigation;
using AutomaticWebInfoGetterWpfLib.Services.TimerInitializer;
using AutomaticWebInfoGetterWpfLib.Services.WebInfoGetter;
using AutomaticWebInfoGetterWpfLib.Services.WriterService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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

    class AddSettingViewModel : ViewModelBase
    {
        #region Fields and Properties


        private string url;
        public string URL
        {
            get { return url; }
            set
            {
                Set(ref url, value);
                AddSettingInfoCommand.RaiseCanExecuteChanged();
            }
        }

        private Visibility addDownloadedPartInfoVisibility;
        public Visibility AddDownloadedPartInfoVisibility
        {
            get { return addDownloadedPartInfoVisibility; }
            set
            {
                Set(ref addDownloadedPartInfoVisibility, value);
                MakeVisibileAddSettingInfoOfDownloadedPartCommand.RaiseCanExecuteChanged();
            }
        }


        private string xpath;
        public string XPath
        {
            get => xpath;
            set
            {
                Set(ref xpath, value);
                AddSettingInfoOfDownloadedPartInCollectionCommand.RaiseCanExecuteChanged();
            }
        }

        private string header;
        public string Header
        {
            get { return header; }
            set => Set(ref header, value);
        }

        private int row;
        public int Row
        {
            get { return row; }
            set => Set(ref row, value);
        }

        private int column;
        public int Column
        {
            get { return column; }
            set => Set(ref column, value);
        }



        private ObservableCollection<DownloadedPartOfPageSettingInfo> settingInfosOfDownloadedPartsOfPage = new ObservableCollection<DownloadedPartOfPageSettingInfo>();
        public ObservableCollection<DownloadedPartOfPageSettingInfo> SettingInfosOfDownloadedPartsOfPage
        {
            get => settingInfosOfDownloadedPartsOfPage;
            set => Set(ref settingInfosOfDownloadedPartsOfPage, value);
        }

        private int distanceBetweenLines;
        public int DistanceBetweenLines
        {
            get { return distanceBetweenLines; }
            set => Set(ref distanceBetweenLines, value);
        }

        private int betweenWritingNewInfoDistance;
        public int BetweenWritingNewInfoDistance
        {
            get { return betweenWritingNewInfoDistance; }
            set => Set(ref betweenWritingNewInfoDistance, value);
        }

        private bool horizontalOrientationOfWriting;
        public bool HorizontalOrientationOfWriting
        {
            get { return horizontalOrientationOfWriting; }
            set => Set(ref horizontalOrientationOfWriting, value);
        }

        private bool startAllPartsAtSamePosition;
        public bool StartAllPartsAtSamePosition
        {
            get { return startAllPartsAtSamePosition; }
            set => Set(ref startAllPartsAtSamePosition, value);
        }


        private string fileNameToWriteInfo;
        public string FileNameToWriteInfo
        {
            get { return fileNameToWriteInfo; }
            set
            {
                Set(ref fileNameToWriteInfo, value);
                AddSettingInfoCommand.RaiseCanExecuteChanged();
            }
        }



        private bool isSingleNode;
        public bool IsSingleNode
        {
            get { return isSingleNode; }
            set { Set(ref isSingleNode, value); }
        }


        private TimeSpan delayBetweenQueries;
        public TimeSpan DelayBetweenQueries
        {
            get
            {
                if (SelectedDelayMeasure == DelayMeasuresEnum.Seconds.ToString())
                {
                    return TimeSpan.FromSeconds(NumericRepresentationOfDelay);
                }
                else if (SelectedDelayMeasure == DelayMeasuresEnum.Minutes.ToString())
                {
                    return TimeSpan.FromSeconds(NumericRepresentationOfDelay * 60);
                }
                else if (SelectedDelayMeasure == DelayMeasuresEnum.Hours.ToString())
                {
                    return TimeSpan.FromSeconds(numericRepresentationOfDelay * 60 * 60);
                }
                else if (SelectedDelayMeasure == DelayMeasuresEnum.Days.ToString())
                {
                    return TimeSpan.FromSeconds(numericRepresentationOfDelay * 60 * 60 * 24);
                }
                else
                {
                    throw new NoSuchTypeInDelayMeasuresEnum();
                }
            }
        }



        private string selectedDelayMeasure;
        public string SelectedDelayMeasure
        {
            get { return selectedDelayMeasure; }
            set
            {
                Set(ref selectedDelayMeasure, value);
                RaisePropertyChanged(nameof(DelayBetweenQueries));
            }
        }



        private double numericRepresentationOfDelay;
        public double NumericRepresentationOfDelay
        {
            get { return numericRepresentationOfDelay; }
            set
            {
                Set(ref numericRepresentationOfDelay, value);
                RaisePropertyChanged(nameof(DelayBetweenQueries));
            }
        }



        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }



        public string[] DelayMeasures { get => Enum.GetNames(typeof(DelayMeasuresEnum)); }

        #endregion

        #region Dependencies

        INavigationService navigationService;

        IWebInfoGetter webInfoGetter;

        ITimerInitializer timerInitializer;

        IWriter writer;

        #endregion

        #region Messages

        SettingsViewModelAddSettingInfoMessage addSettingInfoMessage = new SettingsViewModelAddSettingInfoMessage();

        SettingsViewModelInitializeMessage settingsViewModelInitializeMessage = new SettingsViewModelInitializeMessage();
        #endregion

        #region ctor

        public AddSettingViewModel(INavigationService navigationService, IWebInfoGetter webInfoGetter, ITimerInitializer timerInitializer, IWriter writer)
        {
            this.navigationService = navigationService;
            this.webInfoGetter = webInfoGetter;
            this.timerInitializer = timerInitializer;
            this.writer = writer;
            Messenger.Default.Register<AddSettingViewModelInitializeMessage>(this, obj => SwitchInitialStateCommand.Execute(obj));
        }

        #endregion

        #region Commands

        private RelayCommand switchInitialStateCommand;

        public RelayCommand SwitchInitialStateCommand
        {
            get
            {
                return switchInitialStateCommand ?? (new RelayCommand(
                    () =>
                    {
                        URL = "";
                        XPath = "";
                        AddDownloadedPartInfoVisibility = Visibility.Collapsed;
                        SettingInfosOfDownloadedPartsOfPage.Clear();
                        DistanceBetweenLines = 1;
                        BetweenWritingNewInfoDistance = 2;
                        HorizontalOrientationOfWriting = false;
                        FileNameToWriteInfo = "";
                        Row = 1;
                        Column = 1;
                        Header = "";
                        StartAllPartsAtSamePosition = true;
                        IsSingleNode = false;
                        SelectedDelayMeasure = DelayMeasures.First(i => i == DelayMeasuresEnum.Seconds.ToString());
                        NumericRepresentationOfDelay = 1;
                        StartDate = DateTime.Now;
                        EndDate = DateTime.Now.AddDays(7);
                    }
                ));
            }
        }


        private RelayCommand cancelCommand;

        public RelayCommand CancelCommand
        {
            get
            {
                return cancelCommand ?? (cancelCommand = new RelayCommand(() => ReturnBack()));
            }
        }

        private RelayCommand addSettingInfoCommand;

        public RelayCommand AddSettingInfoCommand
        {
            get
            {
                return addSettingInfoCommand ?? (addSettingInfoCommand = new RelayCommand(() =>
                {
                    SettingsInfo addedSettingInfo = new SettingsInfo
                    {
                        URL = URL,
                        SettingInfosOfDownloadedPartsOfPage = new ObservableCollection<DownloadedPartOfPageSettingInfo>(SettingInfosOfDownloadedPartsOfPage),
                        SingleNode = IsSingleNode,
                        BetweenLineDistance = DistanceBetweenLines,
                        BetweenWritingNewInfoDistance = BetweenWritingNewInfoDistance,
                        HorizontalOrientationOfWritingInfo = HorizontalOrientationOfWriting,
                        NameOfFileToWriteInfo = FileNameToWriteInfo,
                         StartAllPartsAtSamePosition = StartAllPartsAtSamePosition,
                        TimeInfo = new ActionExecutionTimeInfo
                        {
                            StartDate = StartDate,
                            EndDate = EndDate,
                            DelayBetweenQueries = DelayBetweenQueries
                        }
                    };
                    timerInitializer.InitializeTimer(addedSettingInfo, webInfoGetter, writer);
                    addSettingInfoMessage.AddedSettingInfo = addedSettingInfo;
                    Messenger.Default.Send<SettingsViewModelAddSettingInfoMessage>(addSettingInfoMessage);
                    ReturnBack();
                }
                , () => !string.IsNullOrWhiteSpace(URL) &&
                         SettingInfosOfDownloadedPartsOfPage.Count > 0 &&
                         !string.IsNullOrWhiteSpace(FileNameToWriteInfo)
                ));
            }
        }

        private RelayCommand makeVisibileAddSettingInfoOfDownloadedPartCommand;

        public RelayCommand MakeVisibileAddSettingInfoOfDownloadedPartCommand
        {
            get
            {
                return makeVisibileAddSettingInfoOfDownloadedPartCommand ?? (makeVisibileAddSettingInfoOfDownloadedPartCommand =
                    new RelayCommand(
                        () => AddDownloadedPartInfoVisibility = Visibility.Visible,
                        () => AddDownloadedPartInfoVisibility != Visibility.Visible));
            }
        }


        private RelayCommand hideAddSettingInfoOfDownloadedPartCommand;

        public RelayCommand HideAddSettingInfoOfDownloadedPartCommand
        {
            get
            {
                return hideAddSettingInfoOfDownloadedPartCommand ?? (hideAddSettingInfoOfDownloadedPartCommand =
                    new RelayCommand(
                        () =>
                        {
                            XPath = "";
                            Row = 1;
                            Column = 1;
                            Header = "";
                            AddDownloadedPartInfoVisibility = Visibility.Collapsed;
                        }));
            }
        }


        private RelayCommand addSettingInfoOfDownloadedPartInCollectionCommand;

        public RelayCommand AddSettingInfoOfDownloadedPartInCollectionCommand
        {
            get
            {
                return addSettingInfoOfDownloadedPartInCollectionCommand ?? (addSettingInfoOfDownloadedPartInCollectionCommand =
                    new RelayCommand(
                        () =>
                        {
                            SettingInfosOfDownloadedPartsOfPage.Add(new DownloadedPartOfPageSettingInfo
                            {
                                StartPositionOfWriting = new Position
                                {
                                    Row = Row,
                                    Column = Column
                                },
                                Header = Header,
                                XPath = XPath
                            });
                            HideAddSettingInfoOfDownloadedPartCommand.Execute(null);
                        },
                        () => !string.IsNullOrWhiteSpace(XPath)));
            }
        }



        #endregion

        #region private functions

        void ReturnBack()
        {
            Messenger.Default.Send<SettingsViewModelInitializeMessage>(settingsViewModelInitializeMessage);
            navigationService.GoBack();
        }

        #endregion
    }
}