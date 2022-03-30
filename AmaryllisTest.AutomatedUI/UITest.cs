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

            _driver.FindElement(By.CssSelector(".bui-avatar__image")).Click(); // currentlanguage 
            _driver.FindElement(By.CssSelector(".bui-group__item:nth-child(4) .bui-grid__column:nth-child(3) .bui-inline-container__main")).Click(); // sweden

            Assert.AreEqual("sv_SE", _driver.FindElement(By.CssSelector("meta[property='og:locale']")).GetAttribute("content")); // language
        }

        [Test]
        public void Open_TicketsPage_ReturnsNothing()
        {
            _driver.Navigate().GoToUrl("https://www.booking.com");

            _driver.FindElement(By.CssSelector(".bui-tab__item:nth-child(2) .bui-tab__text:nth-child(2)")).Click(); //tickets

            var url = _driver.Url; // Url

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

        [Test]
        public void Add_Filters_List()
        {
            _driver.Navigate().GoToUrl("https://www.booking.com");
            _driver.Manage().Window.Size = new System.Drawing.Size(945, 1020);

            _driver.FindElement(By.Id("ss")).Click(); // locationfilter
            _driver.FindElement(By.Id("ss")).SendKeys("Варшава");
                //_driver.FindElement(By.CssSelector(".sb-autocomplete__item--icon_revamp:nth-child(1)")).Click();
            _driver.FindElement(By.CssSelector(".xp__input-group:nth-child(2) .sb-date-field__icon")).Click(); // dateselement
            _driver.FindElement(By.CssSelector(".bui-calendar__wrapper:nth-child(2) .bui-calendar__row:nth-child(2) > .bui-calendar__date:nth-child(4)")).Click(); //startdate element
            _driver.FindElement(By.CssSelector(".bui-calendar__wrapper:nth-child(2) .bui-calendar__row:nth-child(2) > .bui-calendar__date:nth-child(6) > span > span")).Click(); //enddate element
            _driver.FindElement(By.CssSelector(".xp__guests__count")).Click();//guestselement
            //_driver.FindElement(By.CssSelector(".sb-group-children .bui-stepper__add-button")).Click();
            //_driver.FindElement(By.Id("group_children")).SendKeys("1");

            {
                var count = int.Parse(_driver.FindElement(By.CssSelector(
                        "#xp__guests__inputs-container > div > div > div.sb-group__field.sb-group-children > div > div.bui-stepper__wrapper.sb-group__stepper-a11y > span.bui-stepper__display"))
                    .Text); // children

                for (int i = count; i != 1; i = count > 1 ? i - 1 : i + 1)
                {
                    _driver.FindElement(By.CssSelector(
                            i > 1
                                ? "#xp__guests__inputs-container > div > div > div.sb-group__field.sb-group-children > div > div.bui-stepper__wrapper.sb-group__stepper-a11y > button.bui-button.bui-button--secondary.bui-stepper__subtract-button" //subtract
                                : "#xp__guests__inputs-container > div > div > div.sb-group__field.sb-group-children > div > div.bui-stepper__wrapper.sb-group__stepper-a11y > button.bui-button.bui-button--secondary.bui-stepper__add-button")) //add
                        .Click();
                }
            }

            _driver.FindElement(By.Name("age")).Click();//children
            {
                var dropdown = _driver.FindElement(By.Name("age"));//children
                dropdown.FindElement(By.CssSelector("#xp__guests__inputs-container > div > div > div.sb-group__children__field.clearfix > select > option:nth-child(12)")).Click();//children12
            }
            _driver.FindElement(By.CssSelector(".sb-searchbox__button > span:nth-child(1)")).Click();//searchbox
            Assert.True(true);
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}