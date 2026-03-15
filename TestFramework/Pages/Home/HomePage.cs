using TestFramework.Drivers;
using TestFramework.Constants;

namespace TestFramework.Pages.Home
{
    public class HomePage(DriverManager driver) : BasePage(driver)
    {
        private readonly string _basePath = AppRoutes.LocalPath + AppRoutes.HomePageRoute;

        private SearchBarComponent _searchBar = null!;
        private HomeSidemenuComponent _sideMenu = null!;
        private ProductGridComponent _productGrid = null!;

        public SearchBarComponent SearchBar => _searchBar ??= new SearchBarComponent(DriverMgr);
        public HomeSidemenuComponent SideMenu => _sideMenu ??= new HomeSidemenuComponent(DriverMgr);
        public ProductGridComponent ProductGrid => _productGrid ??= new ProductGridComponent(DriverMgr);
        public void Open()
        {
            DriverMgr.GoToUrl(_basePath);
        }

        public void PerformFullSearch(string text)
        {
            SearchBar.TypeSearchText(text);
            SearchBar.ClickSearch();
        }

        public void AcceptAlert()
        {
            DriverMgr.AcceptAlert();
        }

        public void DismissAlert()
        {
            DriverMgr.DismissAlert();
        }

        public string GetAlertText()
        {
            return DriverMgr.GetAlertText();
        }
    }
}
