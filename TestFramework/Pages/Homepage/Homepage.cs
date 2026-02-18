using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Drivers;
using TestFramework.Pages.Homepage.HompageComponents.NavBarComponent;
using TestFramework.Pages.Homepage.HompageComponents.SearchBarComponent;

namespace TestFramework.Pages.Homepage
{
    public class HomePage : BasePage
    {
        private readonly string _basePath = "http://localhost:5173/home";

        // Componenta este privată, accesibilă prin proprietate
        private SearchBarComponent _searchBar;
        private NavBarComponent _navBar;
        public SearchBarComponent SearchBar => _searchBar ??= new SearchBarComponent(DriverMgr);
        public NavBarComponent NavBar => _navBar ??= new NavBarComponent(DriverMgr);

        public HomePage(DriverManager driver) : base(driver) { }

        public void Open()
        {
            DriverMgr.GoToUrl(_basePath);
        }

        // Metodă de flux (High Level)
        public void PerformFullSearch(string text)
        {
            SearchBar.TypeSearchText(text);
            SearchBar.ClickSearch();
        }
    }
}
