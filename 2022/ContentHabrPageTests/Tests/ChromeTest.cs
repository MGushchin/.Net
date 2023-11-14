using ContentHabrPageTests.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ContentHabrPageTests.Tests
{
    public class ChromeTest: BaseTest
    {

        [OneTimeSetUp]
        public void SetUp()
        {
            _webDriver = new ChromeDriver();
            _webDriver.Manage().Cookies.DeleteAllCookies();
            _webDriver.Navigate().GoToUrl(TestSettings.HostPrefix);
            _webDriver.Manage().Window.Maximize();
        }

    }
}