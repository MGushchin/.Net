using ContentHabrPageTests.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentHabrPageTests.Tests
{
    public class FirefoxTest: BaseTest
    {

        [OneTimeSetUp]
        public void SetUp()
        {
            _webDriver = new FirefoxDriver();
            _webDriver.Manage().Cookies.DeleteAllCookies();
            _webDriver.Navigate().GoToUrl(TestSettings.HostPrefix);
            _webDriver.Manage().Window.Maximize();
        }

    }
}
