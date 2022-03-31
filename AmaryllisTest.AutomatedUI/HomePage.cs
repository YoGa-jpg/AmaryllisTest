using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using OpenQA.Selenium;

namespace AmaryllisTest.AutomatedUI
{
    class HomePage
    {
        private readonly IWebDriver _driver;

        private const string URI = "https://www.booking.com/";

        private IWebElement CurrentLanguageMeta => _driver.FindElement(By.CssSelector("meta[property='og:locale']"));
        private IWebElement CurrentLanguageElement => _driver.FindElement(By.CssSelector(".bui-avatar__image"));
        private IWebElement SwedenLanguageElement => _driver.FindElement(
            By.CssSelector(".bui-group__item:nth-child(4) .bui-grid__column:nth-child(3) .bui-inline-container__main"));
        private IWebElement TicketsElement =>
            _driver.FindElement(By.CssSelector(".bui-tab__item:nth-child(2) .bui-tab__text:nth-child(2)"));
        private IWebElement LocationFilterElement => _driver.FindElement(By.Id("ss"));
        private IWebElement DatesElement =>
            _driver.FindElement(By.CssSelector(".xp__input-group:nth-child(2) .sb-date-field__icon"));
        private IWebElement StartDateElement => _driver.FindElement(By.CssSelector(
            ".bui-calendar__wrapper:nth-child(2) .bui-calendar__row:nth-child(2) > .bui-calendar__date:nth-child(4)"));
        private IWebElement EndDateElement => _driver.FindElement(By.CssSelector(
            ".bui-calendar__wrapper:nth-child(2) .bui-calendar__row:nth-child(2) > .bui-calendar__date:nth-child(6) > span > span"));
        private IWebElement GuestsElement => _driver.FindElement(By.CssSelector(".xp__guests__count"));
        private IWebElement ChildrenCountElement => _driver.FindElement(By.CssSelector(
            "#xp__guests__inputs-container > div > div > div.sb-group__field.sb-group-children > div > div.bui-stepper__wrapper.sb-group__stepper-a11y > span.bui-stepper__display"));
        private IWebElement SubtractChildrenElement => _driver.FindElement(By.CssSelector(
            "#xp__guests__inputs-container > div > div > div.sb-group__field.sb-group-children > div > div.bui-stepper__wrapper.sb-group__stepper-a11y > button.bui-button.bui-button--secondary.bui-stepper__subtract-button"));
        private IWebElement AddChildrenElement => _driver.FindElement(By.CssSelector(
            "#xp__guests__inputs-container > div > div > div.sb-group__field.sb-group-children > div > div.bui-stepper__wrapper.sb-group__stepper-a11y > button.bui-button.bui-button--secondary.bui-stepper__add-button"));
        private IWebElement ChildrenAgeElement => _driver.FindElement(By.Name("age"));
        private IWebElement Children12 => ChildrenAgeElement.FindElement(By.CssSelector("#xp__guests__inputs-container > div > div > div.sb-group__children__field.clearfix > select > option:nth-child(12)"));
        private IWebElement SearchBoxElement =>
            _driver.FindElement(By.CssSelector(".sb-searchbox__button > span:nth-child(1)"));

        public string Language => CurrentLanguageMeta.GetAttribute("content");
        public string Url => _driver.Url;
        public int Children => int.Parse(ChildrenCountElement.Text);

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Navigate() => _driver.Navigate().GoToUrl(URI);
        public void Fullscreen() => _driver.Manage().Window.FullScreen();
        public void SwitchLanguage() => CurrentLanguageElement.Click();
        public void SelectLanguage() => SwedenLanguageElement.Click();
        public void GetTickets() => TicketsElement.Click();
        public void LocationFilter() => LocationFilterElement.Click();
        public void SendLocation(string location) => LocationFilterElement.SendKeys(location);
        public void Dates() => DatesElement.Click();
        public void SetStartDate() => StartDateElement.Click();
        public void SetEndDate() => EndDateElement.Click();
        public void Guests() => GuestsElement.Click();
        public void AddChildren() => AddChildrenElement.Click();
        public void SubtractChildren() => SubtractChildrenElement.Click();
        public void ChildrenAge() => ChildrenAgeElement.Click();
        public void SetChildrenAge() => Children12.Click();
        public void SearchByFilters() => SearchBoxElement.Click();
    }
}
