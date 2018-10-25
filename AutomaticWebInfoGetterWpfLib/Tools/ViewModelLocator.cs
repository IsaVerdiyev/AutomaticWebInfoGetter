using Autofac;
using Autofac.Configuration;
using AutomaticWebInfoGetterWpfLib.Messages;
using AutomaticWebInfoGetterWpfLib.Navigation;
using AutomaticWebInfoGetterWpfLib.Services.TimerInitializer;
using AutomaticWebInfoGetterWpfLib.Services.WebInfoGetter;
using AutomaticWebInfoGetterWpfLib.Services.WriterService;
using AutomaticWebInfoGetterWpfLib.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticWebInfoGetterWpfLib.Tools
{
    class ViewModelLocator
    {
        INavigationService navigationService = new NavigationService();

        public AppViewModel AppViewModel { get; }

        public SettingsViewModel SettingsViewModel { get; }

        public AddSettingViewModel AddSettingViewModel { get; }

        public ViewModelLocator()
        {
            try
            {
                var config = new ConfigurationBuilder();
                config.AddJsonFile("autofac.json");
                var module = new ConfigurationModule(config.Build());
                var builder = new ContainerBuilder();
                builder.RegisterModule(module);
                builder.RegisterInstance(navigationService).As<INavigationService>().SingleInstance();
                builder.RegisterType<WebInfoGetterBasedOnSeleniumAndChrome>().As<IWebInfoGetter>();
                builder.RegisterType<TimerInitializer>().As<ITimerInitializer>();
                builder.RegisterType<WriterToExcel>().As<IWriter>();
                var container = builder.Build();

                using(var scope = container.BeginLifetimeScope())
                {
                    AppViewModel = scope.Resolve<AppViewModel>();
                    SettingsViewModel = scope.Resolve<SettingsViewModel>();
                    AddSettingViewModel = scope.Resolve<AddSettingViewModel>();
                }

                navigationService.AddPage(SettingsViewModel, VM.Settings);
                navigationService.AddPage(AddSettingViewModel, VM.AddSetting);

                navigationService.NavigateTo(VM.Settings);
                Messenger.Default.Send<SettingsViewModelInitializeMessage>(new SettingsViewModelInitializeMessage());

            }catch(Exception ex)
            {
                throw;
            }
        }
    }
}
