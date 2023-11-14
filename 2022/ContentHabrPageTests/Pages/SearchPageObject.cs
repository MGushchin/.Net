using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentHabrPageTests.Pages
{
    public class SearchPageObject
    {
        private IWebDriver _webDriver;

        private readonly By _searchTextField = By.XPath("//input[@class='tm-input-text-decorated__input']");
        private readonly By _searchButton = By.XPath("//span[@class='tm-svg-icon__wrapper tm-search__icon']");

        private readonly By _sortButton = By.XPath("//button[@class='tm-navigation-dropdown__button tm-navigation-dropdown__button']");
        private readonly By _sortByTimeButton = By.XPath("//button[@class='tm-navigation-dropdown__option-button']");

        private readonly By _articlesList = By.XPath("//div[@class='tm-articles-list']/child::*");

        private readonly By _hubsTexts = By.XPath(".//div[@class='tm-article-snippet__hubs']//span//a//span");
        private readonly By _articleTitle = By.XPath(".//a[@class='tm-article-snippet__title-link']//span");
        private readonly By _articleReadMoreButton = By.XPath(".//a[@class='tm-article-snippet__readmore']//span");

        public SearchPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public void Search(string searchText)
        {
            WaitUntill.WaitElement(_webDriver, _searchTextField);
            _webDriver.FindElement(_searchTextField).SendKeys(TestSettings.SearchText);
            _webDriver.FindElement(_searchButton).Click();
        }

        public void SortByTime()
        {
            _webDriver.FindElement(_sortButton).Click();
            _webDriver.FindElement(_sortByTimeButton).Click();
        }

        public string GetPostHubs(int articleIndex)
        {
            string hubs = "";
            WaitUntill.WaitElement(_webDriver, _articlesList);
            ReadOnlyCollection<IWebElement> articles = _webDriver.FindElements(_articlesList);
            ReadOnlyCollection<IWebElement> hubList = articles[--articleIndex].FindElements(_hubsTexts);
            foreach (IWebElement element in hubList)
            {
                hubs += element.Text + " ";
            }
            return hubs;
        }

        public ArticlePageObject OpenArticlePage(int articleIndex)
        {
            WaitUntill.WaitElement(_webDriver, _articlesList);
            ReadOnlyCollection<IWebElement> articles = _webDriver.FindElements(_articlesList);
            articles[--articleIndex].FindElement(_articleReadMoreButton).Click();
            return new ArticlePageObject(_webDriver);
        }

        public string GetArticleTitle(int articleIndex)
        {
            WaitUntill.WaitElement(_webDriver, _articlesList);
            ReadOnlyCollection<IWebElement> articles = _webDriver.FindElements(_articlesList);
            return articles[--articleIndex].FindElement(_articleTitle).Text;
        }

        public string GetArticleReadMoreText(int articleIndex)
        {
            WaitUntill.WaitElement(_webDriver, _articlesList);
            ReadOnlyCollection<IWebElement> articles = _webDriver.FindElements(_articlesList);
            return articles[--articleIndex].FindElement(_articleReadMoreButton).Text;
        }
    }
}
