using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace AmaryllisTest.AutomatedUI
{
    public class Tests : IDisposable
    {
        private IWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            //options.AddLocalStatePreference("excludeSwitches", "enable-automation");
            //options.AddLocalStatePreference("useAutomationExtension", false);
            //options.AddArgument("--disable-blink-features=AutomationControlled");

            _driver = new ChromeDriver(options);
        }

        [Test]
        public void Change_PageLanguage_ReturnsSameLanguage()
        {
            _driver.Navigate().GoToUrl("https://www.booking.com");

            _driver.FindElement(By.CssSelector(".bui-avatar__image")).Click();
            _driver.FindElement(By.CssSelector(".bui-group__item:nth-child(4) .bui-grid__column:nth-child(3) .bui-inline-container__main")).Click();

            Assert.AreEqual("sv_SE", _driver.FindElement(By.CssSelector("meta[property='og:locale']")).GetAttribute("content"));
        }

        [Test]
        public void Open_TicketsPage_ReturnsNothing()
        {
            _driver.Navigate().GoToUrl("https://www.booking.com");

            _driver.FindElement(By.CssSelector(".bui-tab__item:nth-child(2) .bui-tab__text:nth-child(2)")).Click();

            var url = _driver.Url;

            Assert.AreNotEqual(url, "https://www.booking.com/tickets");
        }

        [Test]
        public void Open_ProfilePage_ReturnsProfilePage()
        {
            _driver.Navigate().GoToUrl("https://www.booking.com");
            //_driver.Manage().Window.Size = new System.Drawing.Size(945, 1020);

            _driver.FindElement(By.CssSelector(".js-header-login-link:nth-child(2) > .bui-button__text")).Click();

            _driver.FindElement(By.Id("username")).SendKeys("wacakh@fexbox.org");
            _driver.FindElement(By.CssSelector(".\\_1jp30RWusTBQoML9GSCZ_C")).Click();

            {
                var element = _driver.FindElement(By.CssSelector(".\\_3idbYJ1oAGD-sl-6gdCR2e:nth-child(1)"));
                Actions builder = new Actions(_driver);
                builder.MoveToElement(element).Perform();
            }

            _driver.FindElement(By.Id("password")).SendKeys("381a2ty958N");
            //new Actions(_driver).ClickAndHold(_driver.FindElement(By.CssSelector(".\\_3idbYJ1oAGD-sl-6gdCR2e")));
            _driver.FindElement(By.CssSelector(".\\_3idbYJ1oAGD-sl-6gdCR2e")).Click();

            _driver.FindElement(By.CssSelector(".bui-f-color-complement")).Click();
            _driver.FindElement(By.CssSelector(".bui-dropdown-menu__item:nth-child(1) .bui-dropdown-menu__text")).Click();

            Assert.Pass();
            //wacakh@fexbox.org
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}