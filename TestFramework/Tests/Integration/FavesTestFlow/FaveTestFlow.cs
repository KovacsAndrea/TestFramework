using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Constants;
using TestFramework.Pages.Auth;
using TestFramework.Pages.Faves;
using TestFramework.Pages.Home;

namespace TestFramework.Tests.Integration.FavesTestFlow
{
    public class FaveTestFlow: BaseTest
    {
        private HomePage homePage;
        private FavePage favePage;
        private AuthPage authPage;

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

        [Test]

        public void AddingBookFromHome_ShouldReflectInFavePage()
        {

        }

        public void AddingBookFromHome_ShouldReflectInFaveBadgeNumber()
        {

        }

        [Test]
        public void AddingBookFromHome_WithSearchSortFilterActive_ShouldReflectInFavePage()
        {

        }

        public void AddingBookFromCart_ShouldReflectInFavePage()
        {

        }

        public void AddingBookFromCart_ShouldReflectInFaveBadgeNumber()
        {

        }

        public void RemovingBookFromFaves_ShouldEliminateTheBookFromFavesPage()
        {

        }

        public void RemovingBookFromFavesViaHome_ShouldReflectInTheFavePage()
        {

        }

        public void RemovingBookFromFavesViaHome_ShouldReflectInTheFaveBadgeIcon()
        {

        }

        public void RemovingBookFromFavesViaCart_ShouldReflectInTheFavePage()
        {

        }

        public void RemovingBookFromFavesViaCart_ShouldReflectInTheFaveBadge()
        {

        }

        public void Refreshing_ShouldNotInfluenceTheFavePage()
        {

        }

        public void LoggingOut_ShouldNotInfluenceTheFavePage()
        {

        }

        public void LoggingOut_ShouldNotInfluenceTheFaveBadgeNumber()
        {

        }

    }
}
