using OpenQA.Selenium;
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

        public WebInfoGetterBasedOnSeleniumAndChrome()
        {
            var chromedriverservice = ChromeDriverService.CreateDefaultService();
            chromedriverservice.HideCommandPromptWindow = true;
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("headless");
            webDriver = new ChromeDriver(chromedriverservice, options);
            
        }

        public string GetStringOfNodeByXPathFromUrl(string url, string xpath)
        {
            IWebElement element;

            lock (lockObject)
            {

                webDriver.Url = url;

                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));

                element = wait.Until((webdriver) => webDriver.FindElement(By.XPath(xpath)));
            }

            return element.Text;
        }

        public List<string> GetStringsOfNodesByXPathFromUrl(string url, string xpath)
        {
            ReadOnlyCollection<IWebElement> elements;
             
            lock (lockObject)
            {
                webDriver.Url = url;

                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));

                elements = wait.Until((webdriver) => webDriver.FindElements(By.XPath(xpath)));

            }
            return elements.Select(e => e.Text).ToList();
        }
    }
}
