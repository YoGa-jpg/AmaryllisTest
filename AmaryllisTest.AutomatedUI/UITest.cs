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
        private HomePage _page;

        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            //options.AddLocalStatePreference("excludeSwitches", "enable-automation");
            //options.AddLocalStatePreference("useAutomationExtension", false);
            //options.AddArgument("--disable-blink-features=AutomationControlled");

            _driver = new ChromeDriver(options);
            _page = new HomePage(_driver);
            _page.Navigate();
            _page.Fullscreen();
        }

        [Test]
        public void Change_PageLanguage_ReturnsSameLanguage()
        {
            _page.SwitchLanguage();
            _page.SelectLanguage();

            Assert.AreEqual("sv_SE", _page.Language);
        }

        [Test]
        public void Open_TicketsPage_ReturnsNothing()
        {
            _page.GetTickets();

            Assert.True(_page.Url.StartsWith("https://www.booking.com/flights"));
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

        [Test]
        public void Add_Filters_List()
        {
            _page.LocationFilter();
            _page.SendLocation("Варшава");

            _page.Dates();
            _page.SetStartDate();
            _page.SetEndDate();

            _page.Guests();
            {
                var count = _page.Children;

                for (int i = count; i != 1; i = count > 1 ? i - 1 : i + 1)
                {
                    if (i > 1)
                    {
                        _page.SubtractChildren();
                    }
                    else
                    {
                        _page.AddChildren();
                    }
                }
            }
            _page.ChildrenAge();
            _page.SetChildrenAge();

            _page.SearchByFilters();
            Assert.True(_page.Url.StartsWith("https://www.booking.com/searchresults"));
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}