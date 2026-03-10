using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Constants;
using TestFramework.Pages.Faves;

namespace TestFramework.Tests.Functional.FavesPage
{
    public class LoggedOutFaveSideMenuTests: BaseTest
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
