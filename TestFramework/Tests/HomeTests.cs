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
        public void TrialTest(string searchText)
        {
            var homePage = new HomePage(DriverMgr);
            homePage.Open();

            //homePage.SearchBar.TypeSearchText(searchText);
            //homePage.SearchBar.ClickSearch();

            //DriverMgr.Wait(2);
            //homePage.NavBar.clickOnFavorites();
            //DriverMgr.Wait(2);
            //homePage.NavBar.clickOnHomeIcon();
            //DriverMgr.Wait(2);
            //homePage.NavBar.clickOnCart();
            //DriverMgr.Wait(2);
            //homePage.NavBar.clickOnLoggedOutUserAvatar();
            //DriverMgr.Wait(2);
            //homePage.Open();
            //DriverMgr.Wait(2);

            homePage.SideMenu.Filter.DecreaseHighestPriceByNPositions(4);
            DriverMgr.Wait(2);
            homePage.SideMenu.Filter.IncreateLowestPriceByNPositions(5);
            DriverMgr.Wait(2);

            homePage.SideMenu.Filter.CheckOnlyStock();
            DriverMgr.Wait(2);
            homePage.SideMenu.Sorting.Order.CheckAscending();
            DriverMgr.Wait(2);
            homePage.SideMenu.Sorting.Order.CheckDescending();
            DriverMgr.Wait(2);

            homePage.SideMenu.Sorting.Criteria.CheckYear();
            DriverMgr.Wait(2);

            homePage.SideMenu.Sorting.Criteria.CheckAuthor();
            DriverMgr.Wait(2);

            homePage.SideMenu.Sorting.Criteria.CheckPrice();
            DriverMgr.Wait(2);

            homePage.SideMenu.Sorting.Criteria.CheckTitle();
            DriverMgr.Wait(2);

            homePage.SideMenu.Sorting.Criteria.CheckNone();
            DriverMgr.Wait(2);

            homePage.SideMenu.Sorting.Order.CheckNone();
            DriverMgr.Wait(2);
        }

        [Test]
        public void DebugProductList()
        {
            var homePage = new HomePage(DriverMgr);
            homePage.Open();
            var books = homePage.ProductGrid.GetAllBooks();

            foreach (var book in books)
            {
                // TestContext.WriteLine e "sfânt" pentru NUnit
                TestContext.WriteLine($"Carte: {book.Title} | Autor: {book.Author} | An: {book.Year} | Pret: {book.Price} EURO");
            }

            Assert.That(books.Count, Is.GreaterThan(0), "Lista de carti e goala, ceva nu e bine la locatori!");
        }
    }
}
