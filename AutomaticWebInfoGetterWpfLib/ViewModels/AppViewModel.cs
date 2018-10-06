using AutomaticWebInfoGetterWpfLib.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticWebInfoGetterWpfLib.ViewModels
{
    class AppViewModel: ViewModelBase
    {
        #region Properties
        ViewModelBase currentControl;

        public ViewModelBase CurrentControl
        {
            get => currentControl;
            set => Set(ref currentControl, value);
        }
        #endregion

        #region Dependencies
        INavigationService navigationService;
        #endregion 

        #region Consturctor
        public AppViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            Messenger.Default.Register<ViewModelBase>(this, param => CurrentControl = param);
        }
        #endregion


    }
}
