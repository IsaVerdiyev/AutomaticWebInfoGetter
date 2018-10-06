using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AutomaticWebInfoGetterWpfLib.Tools;
using AutomaticWebInfoGetterWpfLib.ViewModels;
using AutomaticWebInfoGetterWpfLib.Views;

namespace AutomaticWebInfoGetterWpfLib
{
    public class ControllerClass
    {

        Window window = new AppView();

        public async void DoSomething()
        {
            while (true)
            {
                MessageBox.Show("Works");
                await Task.Delay(5000);
            }
        }

        public void OpenWindow()
        {
            if (!window.IsLoaded)
            {
                window = new AppView();
                window.DataContext = new ViewModelLocator().AppViewModel;
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
