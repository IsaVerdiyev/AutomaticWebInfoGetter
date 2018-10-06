using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticWebInfoGetterWpfLib.ViewModels
{
    class SettingsViewModel: ViewModelBase
    {
        string text;
        public string Text
        {
            get => text;
            set => Set(ref text, value);
        }

        public SettingsViewModel()
        {
            Text = "Hello world";
        }
    }
}
