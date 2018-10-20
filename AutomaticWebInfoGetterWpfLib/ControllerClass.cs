using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.Integration;
using AutomaticWebInfoGetterWpfLib.Services.Storage;
using AutomaticWebInfoGetterWpfLib.Services.WebInfoGetter;
using AutomaticWebInfoGetterWpfLib.Tools;
using AutomaticWebInfoGetterWpfLib.ViewModels;
using AutomaticWebInfoGetterWpfLib.Views;

namespace AutomaticWebInfoGetterWpfLib
{
    public class ControllerClass
    {
        IWebInfoGetter webInfoGetter;

        IStorage storage;

        public ControllerClass(IWebInfoGetter webInfoGetter)
        {
            this.webInfoGetter = webInfoGetter;
            storage = StorageGetter.Storage;
        }

        Window window = new AppView();

        public void OpenWindow()
        {
            if (!window.IsLoaded)
            {
                window = new AppView();
                window.DataContext = new ViewModelLocator().AppViewModel;
                ElementHost.EnableModelessKeyboardInterop(window);
                window.Show();
            }

            if (!window.IsVisible)
            {
                window.Show();
            }
            else
            {
                if(window.WindowState == WindowState.Minimized)
                {
                    window.WindowState = WindowState.Normal;
                }
            }
            

        }

        
    }
}
