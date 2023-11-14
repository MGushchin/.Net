using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentHabrPageTests.Pages
{
    public class ArticlePageObject
    {
        private IWebDriver _webDriver;

        private readonly By _titleText = By.XPath("//h1[@class='tm-article-snippet__title tm-article-snippet__title_h1']/span");
        private readonly By _commnetsCountText = By.XPath("//span[@class='tm-article-comments-counter-link__value']");

        public ArticlePageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public string GetTitleText()
        {
            WaitUntill.WaitElement(_webDriver, _titleText);
            return _webDriver.FindElement(_titleText).Text;
        }

        public int GetCommentsCount()
        {
            WaitUntill.WaitElement(_webDriver, _commnetsCountText);
            string countText = _webDriver.FindElement(_commnetsCountText).Text;
            int count = int.Parse(countText);
            return count;
        }
    }
}
