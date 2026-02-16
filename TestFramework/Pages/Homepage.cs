using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Pages
{
    public class HomePage
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        // Elements
        private IWebElement SearchBox => _driver.FindElement(By.Id("searchInput"));
        private IWebElement SearchButton => _driver.FindElement(By.Id("searchButton"));

        // Actions
        public void EnterSearch(string text)
        {
            SearchBox.Clear();
            SearchBox.SendKeys(text);
        }

        public void ClickSearch()
        {
            SearchButton.Click();
        }

        public void SearchFor(string text)
        {
            EnterSearch(text);
            ClickSearch();
        }
    }
}
