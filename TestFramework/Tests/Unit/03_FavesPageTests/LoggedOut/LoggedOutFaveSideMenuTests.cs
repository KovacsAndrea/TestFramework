using NUnit.Framework;
using TestFramework.Constants;
using TestFramework.Pages.Faves;
using TestFramework.Reports.Manager;

namespace TestFramework.Tests.Unit.FavesPage
{
    [TestFixture]
    [Category("Unit | Fave Page | Fave Side Menu | Logged Out")]
    public class LoggedOutFaveSideMenuTests : BaseTest
    {
        private FavePage favePage;

        #region SETUP
        [SetUp]
        public void Setup()
        {
            favePage = new FavePage(DriverMgr);

            ReportManager.Test.Info("Se deschide pagina Favorites pentru user neautentificat.");
            favePage.Open();
            DriverMgr.Wait(1);
        }
        #endregion

        #region POSITIVE TESTS
        [Test]
        public void SideMenu_ShouldDisplayExpectedMessage()
        {
            ReportManager.Test.Info("Se verifica daca mesajul afisat este cel corect.");
 
        }
        [Test]
        public void SideMenu_ClickingLogIn_ShouldRedirectToAuth()
        {
            ReportManager.Test.Info("Se da click pe Log In din Side Menu si se verifica redirect-ul catre Auth.");
            favePage.SideMenu.ClickOnLogIn();

            AssertRedirect(AppRoutes.AuthPageRoute);
        }
        #endregion
    }
}