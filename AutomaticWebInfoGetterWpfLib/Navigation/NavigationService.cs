using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace AutomaticWebInfoGetterWpfLib.Navigation
{
    class NavigationService : INavigationService
    {

        private Dictionary<VM, ViewModelBase> pages = new Dictionary<VM, ViewModelBase>();
        private Stack<VM> history = new Stack<VM>();

        public ViewModelBase Current { get; set; }

        public void AddPage(ViewModelBase page, VM name)
        {
            if (pages.ContainsKey(name))
                pages[name] = page;
            else
                pages.Add(name, page);
        }

        public void ClearHistory()
        {
            history.Clear();
        }

        public void GoBack()
        {
            if(history.Count > 0)
            {
                Current = pages[history.Pop()];
            }
        }

        public void NavigateTo(VM name)
        {
            try
            {
                Current = pages[name];
                history.Push(name);
                Messenger.Default.Send(Current);
            }
            catch(Exception ex)
            {
                throw new PageNotFoundException($"Page {name} not found. Error occured in NavigateTo function");
            }
        }

        public void RemovePage(VM name)
        {
            try
            {
                pages.Remove(name);
            }
            catch(Exception ex)
            {
                throw new PageNotFoundException($"Page {name} not found. Error occured in RemovePage function");
            }
        }
    }
}
