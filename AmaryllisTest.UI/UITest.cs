using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace AmaryllisTest.UI
{
    public class UITest : IDisposable
    {
        private readonly IWebDriver _driver;

        public UITest()
        {
            _driver = new ChromeDriver();
        }

        [Fact]
        public void Change_PageLanguage_ReturnsSameLanguage()
        {
            _driver.Navigate().GoToUrl("https://www.booking.com");

            _driver.FindElement(By.CssSelector(".bui-avatar__image")).Click();
            _driver.FindElement(By.CssSelector(".bui-group__item:nth-child(4) .bui-grid__column:nth-child(3) .bui-inline-container__main")).Click();

            Assert.Equal("sv_SE", _driver.FindElement(By.CssSelector("meta[property='og:locale']")).GetAttribute("content"));
        }

        public void Dispose()
        {
            //_driver.Quit();
            //_driver.Dispose();
        }
    }
}
