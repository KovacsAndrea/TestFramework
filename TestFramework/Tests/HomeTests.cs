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

            DriverMgr.Wait(10); // sau WaitForElement dacă vrei
                               // Aici ar veni un Assert, de exemplu:
        }
    }
    
}
