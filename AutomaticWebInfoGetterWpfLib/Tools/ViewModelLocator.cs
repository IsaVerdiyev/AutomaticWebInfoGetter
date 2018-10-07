using Autofac;
using Autofac.Configuration;
using AutomaticWebInfoGetterWpfLib.Navigation;
using AutomaticWebInfoGetterWpfLib.ViewModels;
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

            }catch(Exception ex)
            {
                throw;
            }
        }
    }
}
