using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Constants;
using TestFramework.Pages.Auth;
using TestFramework.Pages.Faves;
using TestFramework.Pages.Home;

namespace TestFramework.Tests.Functional.FavesPage
{
    public class LoggedOutFaveGridTests : BaseTest
    {
        private FavePage favePage;

        [SetUp]
        public void Setup()
        {
            favePage = new FavePage(DriverMgr);
            favePage.Open();
            DriverMgr.Wait(1);
        }

        [Test]
        public void FavesPage_ShouldDisplayExpectedMessage()
        {
            string emptyFavesTitle = favePage.ProductGrid.GetEmptyListMessageTitle();
            string emptyFavesText = favePage.ProductGrid.GetEmptyListMessageText();
            bool isFaveIconPresent = favePage.ProductGrid.IsFavesIconVisible();
            Assert.That(emptyFavesTitle, Is.EqualTo(AppMessages.emptyFavesTitle));
            Assert.That(emptyFavesText, Is.EqualTo(AppMessages.emptyFavesText));
            Assert.That(isFaveIconPresent, Is.True);
        }

        [Test]
        public void FavesPage_ClickingBrowseButton_ShouldRedirectToHome()
        {
            favePage.ProductGrid.ClickOnBrowse();
            DriverMgr.Wait(1);
            string expectedPath = AppRoutes.LocalPath + AppRoutes.HomePageRoute;
            string currentPath = DriverMgr.GetUrl();
            Assert.That(currentPath, Is.EqualTo(expectedPath));

        }

        [Test]
        public void SideMenu_ClickingLogIn_ShouldRedirectToAuth()
        {
            favePage.SideMenu.ClickOnLogIn();
            DriverMgr.Wait(1);
            string expectedPath = AppRoutes.LocalPath + AppRoutes.AuthPageRoute;
            string currentPath = DriverMgr.GetUrl();
            Assert.That(currentPath, Is.EqualTo(expectedPath));
        }
    }
}
