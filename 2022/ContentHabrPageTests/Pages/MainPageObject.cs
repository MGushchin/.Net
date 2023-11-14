using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentHabrPageTests.Pages
{
    public class MainPageObject
    {
        private IWebDriver _webDriver;

        private readonly By _allFlowsButton = By.XPath("//a[@href='/ru/all/']");
        private readonly By _developFlowButton = By.XPath("//a[@href='/ru/flows/develop/']");
        private readonly By _adminFlowButton = By.XPath("//a[@href='/ru/flows/admin/']");
        private readonly By _designFlowButton = By.XPath("//a[@href='/ru/flows/design/']");
        private readonly By _managementFlowButton = By.XPath("//a[@href='/ru/flows/management/']");
        private readonly By _marketingFlowButton = By.XPath("//a[@href='/ru/flows/marketing/']");
        private readonly By _popsciFlowButton = By.XPath("//a[@href='/ru/flows/popsci/']");

        private readonly By _sectionName = By.XPath("//h1[@class='tm-section-name__text']");

        private readonly By _searchButton = By.XPath("//a[@href='/ru/search/']");
        private readonly By _articleTitles = By.XPath("//a[@class='tm-article-snippet__title-link']/span");

        public MainPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public void OpenAllFlows()
        {
            _webDriver.FindElement(_allFlowsButton).Click();
        }

        public void OpenDevelopFlow()
        {
            _webDriver.FindElement(_developFlowButton).Click();
        }

        public void OpenAdminFlow()
        {
            _webDriver.FindElement(_adminFlowButton).Click();
        }

        public void OpenDesignFlow()
        {
            _webDriver.FindElement(_designFlowButton).Click();
        }

        public void OpenManagementFlow()
        {
            _webDriver.FindElement(_managementFlowButton).Click();
        }

        public void OpenMarketingFlow()
        {
            _webDriver.FindElement(_marketingFlowButton).Click();
        }

        public void OpenPopsciFlow()
        {
            _webDriver.FindElement(_popsciFlowButton).Click();
        }

        public string GetPageTitle()
        {
            WaitUntill.WaitElement(_webDriver, _sectionName);
            return _webDriver.FindElement(_sectionName).Text;
        }

        public List<string> GetArticlesTitles()
        {
            WaitUntill.WaitElement(_webDriver, _articleTitles);
            List<string> titles = new List<string>();
            ReadOnlyCollection<IWebElement> articlesTitles = _webDriver.FindElements(_articleTitles);
            foreach(IWebElement element in articlesTitles)
            {
                titles.Add(element.Text);
            }
            return titles;
        }

        public SearchPageObject OpenSearchPage()
        {
            _webDriver.FindElement(_searchButton).Click();
            return new SearchPageObject(_webDriver);
        }
    }
}
