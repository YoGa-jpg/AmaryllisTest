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
            options.AddArgument("--user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.75 Safari/537.36");
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
            _page.GetTickets();//Сделать через прокси или что-то такое

            Assert.True(_page.Url.StartsWith("https://www.booking.com/flights"));
        }

        [Test]
        public void Open_ProfilePage_ReturnsProfilePage() //а это просто сделать
        {
            _driver.Navigate().GoToUrl("https://www.booking.com");
            //_driver.Manage().Window.Size = new System.Drawing.Size(945, 1020);

            _driver.FindElement(By.CssSelector(".js-header-login-link:nth-child(2) > .bui-button__text")).Click();

            _driver.FindElement(By.Id("username")).SendKeys("wacakh@fexbox.org");
            _driver.FindElement(By.CssSelector("#root > div > div > div > div.app > div.access-container.bui_font_body > div > div > div > div > div > div > form > div:nth-child(3) > button")).Click();

            //{
            //    var element = _driver.FindElement(By.CssSelector("#root > div > div > div > div.app > div.access-container.bui_font_body > div > div > div > div > div > div > form > button"));
            //    Actions builder = new Actions(_driver);
            //    builder.MoveToElement(element).Perform();
            //}

            _driver.FindElement(By.Id("password")).SendKeys("381a2ty958N");
            //new Actions(_driver).ClickAndHold(_driver.FindElement(By.CssSelector(".\\_3idbYJ1oAGD-sl-6gdCR2e")));
            _driver.FindElement(By.CssSelector("#root > div > div > div > div.app > div.access-container.bui_font_body > div > div > div > div > div > div > form > button")).Click();

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

            _page.Dates();//Сделать выбор даты по ТЗ
            _page.SetStartDate();
            _page.SetEndDate();

            _page.Guests();//количество людей, количество номеров
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