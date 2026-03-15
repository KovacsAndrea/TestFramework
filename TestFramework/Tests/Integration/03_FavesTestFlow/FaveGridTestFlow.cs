using TestFramework.Constants;
using TestFramework.Models;
using TestFramework.Reports.Manager;
using TestFramework.Tests.Integration._03_FavesTestFlow;
using TestFramework.Utilities;

namespace TestFramework.Tests.Integration.FavesTestFlow
{
    [TestFixture]
    [Category("Integration | Faves | Fave Grid Flow")]
    public class FaveGridTestFlow : FaveFlowBaseTest
    {
        #region SETUP
        [SetUp]
        public void Setup()
        {
            ReportManager.Test.Info("Se deschide pagina de autentificare si se navigheaza catre formularul Register.");
            authPage.Open();
            authPage.Login.ClickRegisterLink();
        }
        #endregion

        #region HELPERS
        private void AssertFavePageCount(int expectedCount)
        {
            int actual = favePage.FaveGrid.GetFaveBooksCount();
            ReportManager.Test.Info($"Se verifica numarul de produse din Favorites Page. Expected: {expectedCount}, Actual: {actual}");
            Assert.That(actual, Is.EqualTo(expectedCount));
        }
        #endregion

        #region POSITIVE TESTS
        [Test]
        public void AddingBookFromHome_ShouldReflectInFavePage()
        {
            ReportManager.Test.Info("Se inregistreaza user si se adauga produse la Favorites din Home.");
            RegisterUser();

            AddBooksToFavoritesFromHome(1, 2);

            ReportManager.Test.Info("Se navigheaza la pagina Favorites.");
            NavigateToFavorites();

            AssertFavePageCount(2);
        }

        [Test]
        public void AddingBookFromHome_WithSearchSortFilterActive_ShouldReflectInFavePage()
        {
            RegisterUser();

            ReportManager.Test.Info("Se aplica search, filter si sort pe Home Page.");
            homePage.SearchBar.TypeSearchText("Harry Potter");
            homePage.SearchBar.ClickSearch();

            homePage.SideMenu.Filter.IncreateLowestPriceByNPositions(1);
            homePage.SideMenu.Filter.DecreaseHighestPriceByNPositions(2);
            homePage.SideMenu.Sorting.Order.CheckAscending();
            homePage.SideMenu.Sorting.Criteria.CheckPrice();

            AddBooksToFavoritesFromHome(1, 2);

            NavigateToFavorites();

            AssertFavePageCount(2);
        }

        [Test]
        public void AddingBookFromCart_ShouldReflectInFavePage()
        {
            RegisterUser();

            ReportManager.Test.Info("Se adauga produse in Cart din Home.");
            AddBooksToCartFromHome(1, 2, 3);

            NavigateToCart();

            ReportManager.Test.Info("Se adauga produse la Favorites din Cart.");
            AddBooksToFavoritesFromCart(1, 2);

            NavigateToFavorites();

            AssertFavePageCount(2);
        }

        [Test]
        public void RemovingBookFromFaves_ShouldEliminateTheBookFromFavesPage()
        {
            RegisterUser();

            AddBooksToFavoritesFromHome(1, 2, 3);

            NavigateToFavorites();

            AssertFavePageCount(3);

            ReportManager.Test.Info("Se elimina primul produs din Favorites.");
            favePage.FaveGrid.ClickFavoriteOnNthProduct(1);

            AssertFavePageCount(2);
        }

        [Test]
        public void RemovingBooksFromFavesViaHome_ShouldReflectInTheFavePage()
        {
            RegisterUser();

            AddBooksToFavoritesFromHome(1, 2, 3);

            NavigateToFavorites();

            AssertFavePageCount(3);

            ReportManager.Test.Info("Se navigheaza inapoi pe Home si se elimina un produs din Favorites.");
            NavigateHome();

            RemoveBooksFromFavoritesFromHome(1);

            navBar.clickOnFavorites();

            AssertFavePageCount(2);
        }

        [Test]
        public void RemovingBookFromFavesViaCart_ShouldReflectInTheFavePage()
        {
            RegisterUser();

            AddBooksToFavoritesFromHome(1, 2, 3);

            AddBooksToCartFromHome(1, 2, 3);

            NavigateToFavorites();

            AssertFavePageCount(3);

            NavigateToCart();

            ReportManager.Test.Info("Se elimina produs din Favorites direct din Cart.");
            cartPage.CartGrid.ClickFavoriteOnNthProduct(1);

            navBar.clickOnFavorites();

            AssertFavePageCount(2);
        }

        [Test]
        public void Refreshing_ShouldNotInfluenceTheFavePage()
        {
            RegisterUser();

            AddBooksToFavoritesFromHome(1, 2, 3);

            NavigateToFavorites();

            AssertFavePageCount(3);

            ReportManager.Test.Info("Se face refresh pe pagina Favorites.");
            DriverMgr.Refresh();
            DriverMgr.Wait(1);

            AssertFavePageCount(3);
        }

        [Test]
        public void LoggingOut_ShouldNotInfluenceTheFavePage()
        {
            User user = RandomIdentityGenerator.GenerateValidUser();

            ReportManager.Test.Info("Se inregistreaza user si se adauga produse la Favorites.");
            RegisterUser(user);

            AddBooksToFavoritesFromHome(1, 2, 3);

            NavigateToFavorites();

            AssertFavePageCount(3);

            ReportManager.Test.Info("Se face logout si apoi login cu acelasi user.");
            navBar.logOut();

            authPage.Login.LoginUser(user);

            navBar.clickOnFavorites();

            AssertFavePageCount(3);
        }
        #endregion
    }
}