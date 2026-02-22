using TestFramework.Drivers;
using TestFramework.Constants;

namespace TestFramework.Pages.Home
{
    public class HomePage(DriverManager driver) : BasePage(driver)
    {
        private readonly string _basePath = AppRoutes.LocalPath + AppRoutes.HomePageRoute;

        // Componenta este privată, accesibilă prin proprietate
        private SearchBarComponent _searchBar = null!;
        private NavBarComponent _navBar = null!;
        private HomeSidemenuComponent _sideMenu = null!;
        private ProductGridComponent _productGrid = null!;

        public SearchBarComponent SearchBar => _searchBar ??= new SearchBarComponent(DriverMgr);
        public NavBarComponent NavBar => _navBar ??= new NavBarComponent(DriverMgr);
        public HomeSidemenuComponent SideMenu => _sideMenu ??= new HomeSidemenuComponent(DriverMgr);
        public ProductGridComponent ProductGrid => _productGrid ??= new ProductGridComponent(DriverMgr);
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
