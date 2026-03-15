using TestFramework.Models;
using TestFramework.Reports.Manager;
using TestFramework.Tests.Integration._03_FavesTestFlow;
using TestFramework.Utilities;

namespace TestFramework.Tests.Integration.FavesTestFlow
{
    [TestFixture]
    [Category("Integration | Faves | Fave Badge Flow")]
    internal class FaveBadgeTestFlow : FaveFlowBaseTest
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
        private void AssertFaveBadgeCount(int expectedCount)
        {
            int actualCount = navBar.GetFaveBadgeCount();
            ReportManager.Test.Info($"Se verifica Fave Badge. Expected: {expectedCount}, Actual: {actualCount}");
            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }
        #endregion

        #region POSITIVE TESTS
        [Test]
        public void AddingBookFromHome_ShouldReflectInFaveBadgeNumber()
        {
            ReportManager.Test.Info("Se inregistreaza user pentru testul Fave Badge.");
            RegisterUser();

            AddBooksToFavoritesFromHome(1, 2);

            AssertFaveBadgeCount(2);
        }

        [Test]
        public void AddingBookFromHome_WithSearchSortFilterActive_ShouldReflectInFaveBadgeNumber()
        {
            ReportManager.Test.Info("Se inregistreaza user pentru testul Fave Badge cu filtre.");
            RegisterUser();

            ReportManager.Test.Info("Se cauta 'Harry Potter' si se aplica filtre si sortari.");
            homePage.SearchBar.TypeSearchText("Harry Potter");
            homePage.SearchBar.ClickSearch();

            homePage.SideMenu.Filter.IncreateLowestPriceByNPositions(1);
            homePage.SideMenu.Filter.DecreaseHighestPriceByNPositions(2);
            homePage.SideMenu.Sorting.Order.CheckAscending();
            homePage.SideMenu.Sorting.Criteria.CheckPrice();

            AddBooksToFavoritesFromHome(1, 2);
            AssertFaveBadgeCount(2);
        }

        [Test]
        public void AddingBookFromCart_ShouldReflectInFaveBadgeNumber()
        {
            ReportManager.Test.Info("Se inregistreaza user pentru testul Fave Badge via Cart.");
            RegisterUser();

            AddBooksToCartFromHome(1, 2, 3);

            ReportManager.Test.Info("Se navigheaza la Cart.");
            NavigateToCart();

            AddBooksToFavoritesFromCart(1, 2);

            AssertFaveBadgeCount(2);
        }

        [Test]
        public void RemovingBookFromFaves_ShouldReflectInTheFaveBadgeNumber()
        {
            ReportManager.Test.Info("Se inregistreaza user si se adauga 3 carti la Favorites.");
            RegisterUser();
            AddBooksToFavoritesFromHome(1, 2, 3);

            NavigateToFavorites();

            int faveCount = favePage.FaveGrid.GetFaveBooksCount();
            ReportManager.Test.Info($"Numar initial de Favorite: {faveCount}");
            Assert.That(faveCount, Is.EqualTo(3));

            ReportManager.Test.Info("Se sterge primul produs din Favorites.");
            favePage.FaveGrid.ClickFavoriteOnNthProduct(1);

            faveCount = favePage.FaveGrid.GetFaveBooksCount();
            ReportManager.Test.Info($"Numar Favorite dupa stergere: {faveCount}");
            Assert.That(faveCount, Is.EqualTo(2));
        }

        [Test]
        public void RemovingBooksFromFavesViaHome_ShouldReflectInTheFaveBadgeIcon()
        {
            RegisterUser();
            AddBooksToFavoritesFromHome(1, 2, 3);

            AssertFaveBadgeCount(3);

            ReportManager.Test.Info("Se sterge primul produs din Favorites via Home Page.");
            RemoveBooksFromFavoritesFromHome(1);

            AssertFaveBadgeCount(2);
        }

        [Test]
        public void RemovingBookFromFavesViaCart_ShouldReflectInTheFaveBadge()
        {
            RegisterUser();
            AddBooksToFavoritesFromHome(1, 2, 3);
            AddBooksToCartFromHome(1, 2, 3);

            NavigateToCart();

            AssertFaveBadgeCount(3);

            ReportManager.Test.Info("Se sterge primul produs din Favorites via Cart.");
            cartPage.CartGrid.ClickFavoriteOnNthProduct(1);

            AssertFaveBadgeCount(2);
        }

        [Test]
        public void LoggingOut_ShouldNotInfluenceTheFaveBadgeNumber()
        {
            User user = RandomIdentityGenerator.GenerateValidUser();

            RegisterUser(user);

            AddBooksToFavoritesFromHome(1, 2, 3);

            AssertFaveBadgeCount(3);

            ReportManager.Test.Info("Se face logout si se reconecteaza user-ul.");
            navBar.logOut();
            authPage.Login.LoginUser(user);

            AssertFaveBadgeCount(3);
        }
        #endregion
    }
}