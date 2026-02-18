using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Pages.Homepage;

namespace TestFramework.Tests
{
    public class HomeTests : BaseTest
    {
        [Test]
        [TestCase("Harry Potter")]
        [TestCase("Brandon Sanderson")]
        public void SearchProductTest(string searchText)
        {
            var homePage = new HomePage(DriverMgr);
            homePage.Open();

            homePage.SearchBar.TypeSearchText(searchText);
            homePage.SearchBar.ClickSearch();

            DriverMgr.Wait(2);
            homePage.NavBar.clickOnFavorites();
            DriverMgr.Wait(2);
            homePage.NavBar.clickOnHomeIcon();
            DriverMgr.Wait(2);
            homePage.NavBar.clickOnCart();
            DriverMgr.Wait(2);
            homePage.NavBar.clickOnLoggedOutUserAvatar();
            DriverMgr.Wait(2);
            homePage.Open();
            DriverMgr.Wait(2);
        }
    }
    
}
