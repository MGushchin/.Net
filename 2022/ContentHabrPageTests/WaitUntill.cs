using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentHabrPageTests
{
    public class WaitUntill
    {
        public static void WaitElement(IWebDriver webDriver, By locator, int seconds = 20)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(seconds));
            IWebElement myElement = wait.Until(webDriver => webDriver.FindElement(locator));
        }
    }
}
