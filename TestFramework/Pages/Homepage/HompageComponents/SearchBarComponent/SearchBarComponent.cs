using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Drivers;

namespace TestFramework.Pages.Homepage.HompageComponents.SearchBarComponent
{
    public class SearchBarComponent : BasePage
    {

        // Locatori privați și imuabili
        private readonly By _searchInput = By.XPath("//div[contains(@class,'search-bar-container')]//input");
        private readonly By _searchButton = By.XPath("//div[contains(@class,'search-bar-container')]//button");
        private readonly By _searchIcon = By.XPath("//div[contains(@class,'search-bar-container')]//button/*");

        public SearchBarComponent(DriverManager driver) : base(driver) { }

        public void TypeSearchText(string text)
        {
            DriverMgr.SendKeys(_searchInput, text);
        }

        public void ClickSearch()
        {
            DriverMgr.Click(_searchButton);
        }
    }

}
