using System;
using NUnit.Framework;
using TestFramework.Constants;
using TestFramework.Models;
using TestFramework.Pages.Auth;
using TestFramework.Pages.Faves;
using TestFramework.Pages.Home;
using TestFramework.GlobalComponents;
using TestFramework.Reports.Manager;

namespace TestFramework.Tests.Unit.FavesPage
{
    [TestFixture]
    [Category("Unit | Fave Page | Fave Grid | Logged In")]
    public class LoggedInFaveGridTests : BaseTest
    {
        private FavePage favePage;
        private AuthPage authPage;
        private NavBar navBar;

        #region SETUP
        [SetUp]
        public void Setup()
        {
            favePage = new FavePage(DriverMgr);
            authPage = new AuthPage(DriverMgr);
            navBar = new NavBar(DriverMgr);

            ReportManager.Test.Info("Se deschide pagina de autentificare si se face login cu user-ul cunoscut.");
            authPage.Open();
            authPage.Login.LoginUser(KnownUsers.UserWithNoFavorites);
            DriverMgr.Wait(1);

            ReportManager.Test.Info("Se acceseaza pagina de Favorites din NavBar.");
            navBar.clickOnFavorites();
            DriverMgr.Wait(1);
        }
        #endregion

        #region POSITIVE TESTS
        [Test]
        public void EmptyFavesGrid_ShouldDisplayExpectedMessage()
        {
            ReportManager.Test.Info("Verificare mesaj si iconita pe pagina Favorites goala.");

            string emptyFavesTitle = favePage.FaveGrid.GetEmptyListMessageTitle();
            string emptyFavesText = favePage.FaveGrid.GetEmptyListMessageText();
            bool isFaveIconPresent = favePage.FaveGrid.IsFavesIconVisible();

            AssertMessage(emptyFavesTitle, AppMessages.emptyFavesTitle);
            AssertMessage(emptyFavesText, AppMessages.emptyFavesText);
            Assert.That(isFaveIconPresent, Is.True);
        }

        [Test]
        public void EmptyFavesGrid_ClickingBrowseButton_ShouldRedirectToHome()
        {
            ReportManager.Test.Info("Se da click pe Browse si se verifica redirect-ul catre Home.");

            favePage.FaveGrid.ClickOnBrowse();
            DriverMgr.Wait(1);

            AssertRedirect(AppRoutes.HomePageRoute);
        }
        #endregion
    }
}