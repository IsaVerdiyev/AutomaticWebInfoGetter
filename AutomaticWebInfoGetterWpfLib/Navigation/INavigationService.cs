using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticWebInfoGetterWpfLib.Navigation
{
    interface INavigationService
    {
        ViewModelBase Current { get; set; }
        void NavigateTo(VM name);
        void GoBack();
        void ClearHistory();
        void AddPage(ViewModelBase page, VM name);
        void RemovePage(VM name);
    }
}
