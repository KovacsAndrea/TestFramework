using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Constants;
using TestFramework.Models;
using TestFramework.Pages.Auth;
using TestFramework.Pages.Faves;
using TestFramework.Pages.Home;

namespace TestFramework.Tests.Functional.FavesPage
{
    public class LoggedInFaveGridTests: BaseTest
    {
        private FavePage favePage;
        private AuthPage authPage;
        private HomePage homePage;

        #region SETUP
        [SetUp]
        public void Setup()
        {

            favePage = new FavePage(DriverMgr);
            authPage = new AuthPage(DriverMgr);
            homePage = new HomePage(DriverMgr);

            authPage.Open();
            authPage.Login.LoginUser(KnownUsers.UserWithNoFavorites);
            DriverMgr.Wait(1);
            homePage.NavBar.clickOnFavorites();
            DriverMgr.Wait(1);
        }
        #endregion

        #region POSITIVE TESTS
        #endregion
        [Test]
        public void EmptyFavesPage_ShouldDisplayExpectedMessage()
        {
            string emptyFavesTitle = favePage.ProductGrid.GetEmptyListMessageTitle();
            string emptyFavesText = favePage.ProductGrid.GetEmptyListMessageText();
            bool isFaveIconPresent = favePage.ProductGrid.IsFavesIconVisible();
            Assert.That(emptyFavesTitle, Is.EqualTo(AppMessages.emptyFavesTitle));
            Assert.That(emptyFavesText, Is.EqualTo(AppMessages.emptyFavesText));
            Assert.That(isFaveIconPresent, Is.True);
        }

        [Test]
        public void EmptyFavesPage_ClickingBrowseButton_ShouldRedirectToHome()
        {
            favePage.ProductGrid.ClickOnBrowse();
            DriverMgr.Wait(1);
            string expectedPath = AppRoutes.LocalPath + AppRoutes.HomePageRoute;
            string currentPath = DriverMgr.GetUrl();
            Assert.That(currentPath, Is.EqualTo(expectedPath));
        }

        
    }
}
