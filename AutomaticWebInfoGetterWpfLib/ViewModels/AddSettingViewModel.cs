using AutomaticWebInfoGetterWpfLib.Exceptions;
using AutomaticWebInfoGetterWpfLib.Messages;
using AutomaticWebInfoGetterWpfLib.Models;
using AutomaticWebInfoGetterWpfLib.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticWebInfoGetterWpfLib.ViewModels
{

    class AddSettingViewModel: ViewModelBase
    {
        #region Fields and Properties


        private string url;

        public string URL
        {
            get { return url; }
            set {
                Set(ref url, value);
                AddSettingInfoCommand.RaiseCanExecuteChanged();
            }
        }

        private string xpath;

        public string XPath
        {
            get { return xpath; }
            set {
                Set(ref xpath, value);
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
                if(SelectedDelayMeasure == DelayMeasuresEnum.Seconds.ToString())
                {
                    return TimeSpan.FromSeconds(NumericRepresentationOfDelay);
                }
                else if(SelectedDelayMeasure == DelayMeasuresEnum.Minutes.ToString())
                {
                    return TimeSpan.FromSeconds(NumericRepresentationOfDelay * 60);
                }
                else if(SelectedDelayMeasure == DelayMeasuresEnum.Hours.ToString())
                {
                    return TimeSpan.FromSeconds(numericRepresentationOfDelay * 60 * 60);
                }
                else if(SelectedDelayMeasure == DelayMeasuresEnum.Days.ToString())
                {
                    return TimeSpan.FromSeconds(numericRepresentationOfDelay * 60 * 60 * 24);
                }
                else
                {
                    throw new NoSuchTypeInDelayMeasuresEnum();
                }
            }
        }



        private string selectedDelayMeasure ;

        public string SelectedDelayMeasure
        {
            get { return selectedDelayMeasure; }
            set {
                Set(ref selectedDelayMeasure, value);
                RaisePropertyChanged(nameof(DelayBetweenQueries));
            }
        }



        private double numericRepresentationOfDelay;

        public double NumericRepresentationOfDelay
        {
            get { return numericRepresentationOfDelay; }
            set {
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

        #endregion

        #region Messages

        SettingsViewModelAddSettingInfoMessage addSettingInfoMessage = new SettingsViewModelAddSettingInfoMessage();

        SettingsViewModelInitializeMessage settingsViewModelInitializeMessage = new SettingsViewModelInitializeMessage();
        #endregion

        #region ctor

        public AddSettingViewModel (INavigationService navigationService)
	    {
            this.navigationService = navigationService;
            Messenger.Default.Register<AddSettingViewModelInitializeMessage>(this, obj => SwitchInitialStateCommand.Execute(obj));
	    }

        #endregion

        #region Commands

        private RelayCommand switchInitialStateCommand;

        public RelayCommand SwitchInitialStateCommand
        {
            get {
                return switchInitialStateCommand ?? (new RelayCommand(
                    () =>
                    {
                        URL = "";
                        XPath = "";
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
            get {
                return cancelCommand ?? (cancelCommand = new RelayCommand(() => ReturnBack()));
            }
        }

        private RelayCommand addSettingInfoCommand;

        public RelayCommand AddSettingInfoCommand
        {
            get {
                return addSettingInfoCommand ?? (addSettingInfoCommand = new RelayCommand(() =>
                {
                    SettingsInfo addedSettingInfo = new SettingsInfo
                    {
                        URL = URL,
                        XPath = XPath.Replace('"', '\''),
                        SingleNode = IsSingleNode,
                        TimeInfo = new ActionExecutionTimeInfo
                        {
                            StartDate = StartDate,
                            EndDate = EndDate,
                            DelayBetweenQueries = DelayBetweenQueries
                        }
                    };

                    addSettingInfoMessage.AddedSettingInfo = addedSettingInfo;
                    Messenger.Default.Send<SettingsViewModelAddSettingInfoMessage>(addSettingInfoMessage);
                    ReturnBack();
                }
                ,() => !string.IsNullOrWhiteSpace(URL) && !string.IsNullOrWhiteSpace(XPath)
                ));
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
