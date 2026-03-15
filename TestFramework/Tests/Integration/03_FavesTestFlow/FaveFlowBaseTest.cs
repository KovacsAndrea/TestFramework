using TestFramework.Pages.Cart;
using TestFramework.Pages.Faves;
using TestFramework.Pages.Home;
using TestFramework.Reports.Manager;

namespace TestFramework.Tests.Integration._03_FavesTestFlow
{
    public class FaveFlowBaseTest : IntegrationBaseTest
    {
        protected HomePage homePage;
        protected FavePage favePage;
        protected CartPage cartPage;

        #region SETUP
        [SetUp]
        public void SetupFaveFlow()
        {
            favePage = new FavePage(DriverMgr);
            homePage = new HomePage(DriverMgr);
            cartPage = new CartPage(DriverMgr);
        }
        #endregion

        #region HELPERS

        protected void AddBooksToFavoritesFromHome(params int[] indexes)
        {
            foreach (var index in indexes)
            {
                ReportManager.Test.Info($"Se adauga produsul de pe pozitia {index} la Favorites din Home Page.");
                homePage.ProductGrid.ClickFavoriteOnNthProduct(index);
            }
            DriverMgr.Wait(1);
        }

        protected void RemoveBooksFromFavoritesFromHome(params int[] indexes)
        {
            foreach (var index in indexes)
            {
                ReportManager.Test.Info($"Se sterge produsul de pe pozitia {index} din Favorites din Home Page.");
                homePage.ProductGrid.ClickFavoriteOnNthProduct(index);
            }
            DriverMgr.Wait(1);
        }

        protected void AddBooksToCartFromHome(params int[] indexes)
        {
            foreach (var index in indexes)
            {
                ReportManager.Test.Info($"Se adauga produsul de pe pozitia {index} in Cart din Home Page.");
                homePage.ProductGrid.AddNthBookToCart(index);
            }
            DriverMgr.Wait(1);
        }

        protected void AddBooksToFavoritesFromCart(params int[] indexes)
        {
            foreach (var index in indexes)
            {
                ReportManager.Test.Info($"Se adauga produsul de pe pozitia {index} la Favorites din Cart Page.");
                cartPage.CartGrid.ClickFavoriteOnNthProduct(index);
            }
            DriverMgr.Wait(1);
        }

        #endregion
    }
}