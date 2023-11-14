using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentHabrPageTests.Tests
{
    public class BaseTest
    {
        protected IWebDriver _webDriver;

        [OneTimeTearDown]
        public void TearDown()
        {
            _webDriver.Quit();
        }
    }
}
