﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticWebInfoGetterWpfLib.Services.WebInfoGetter
{
    public class WebInfoGetterBasedOnSeleniumAndChrome: IWebInfoGetter
    {
        IWebDriver webDriver;

        object lockObject = new object();

        string windowHandle;

        public WebInfoGetterBasedOnSeleniumAndChrome()
        {
            var chromedriverservice = ChromeDriverService.CreateDefaultService();
            chromedriverservice.HideCommandPromptWindow = true;
            ChromeOptions options = new ChromeOptions();
            options.AddArguments(new List<string>
            {
                "--window-size=1920,1080",
                "--disable-gpu",
                "--disable-extensions",
                "--proxy-server='direct://'",
                "--proxy-bypass-list=*",
                "--start-maximized",
                "--headless"
            });
            webDriver = new ChromeDriver(chromedriverservice, options);
            windowHandle = webDriver.CurrentWindowHandle;
        }

        public void LoadPage(string url)
        {
            webDriver.Url = url;
        }

        public string GetStringOfNodeByXPathFromUrl(string xpath)
        {
            IWebElement element;


            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));
            try
            {
                wait.Until(webDriver => webDriver.FindElement(By.XPath(xpath)) != null);
                element = webDriver.FindElement(By.XPath(xpath));
            }
            catch (UnhandledAlertException e)
            {
                DismissAlert();
                wait.Until(webDriver => webDriver.FindElement(By.XPath(xpath)) != null);
                element = webDriver.FindElement(By.XPath(xpath));
            }
            return element.Text;


        }

        public List<string> GetStringsOfNodesByXPathFromUrl(string xpath)
        {
            ReadOnlyCollection<IWebElement> elements;


            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));
            try
            {
                wait.Until(webDriver => webDriver.FindElements(By.XPath(xpath)).Count > 0);
                elements = webDriver.FindElements(By.XPath(xpath));
            }
            catch (UnhandledAlertException e)
            {
                DismissAlert();
                wait.Until(webDriver => webDriver.FindElements(By.XPath(xpath)).Count > 0);
                elements = webDriver.FindElements(By.XPath(xpath));

            }

            return elements.Select(e => e.Text).ToList();

        }


        void DismissAlert()
        {
            var alert = webDriver.SwitchTo().Alert();
            alert.Dismiss();
            webDriver.SwitchTo().Window(windowHandle);
           
        }

        ~WebInfoGetterBasedOnSeleniumAndChrome (){
            webDriver.Dispose();
        }
    }
}
