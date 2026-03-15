using NUnit.Framework;
using TestFramework.Constants;
using TestFramework.Pages.Faves;
using TestFramework.Reports.Manager;

namespace TestFramework.Tests.Functional.FavesPage
{
    [TestFixture]
    [Category("Functional | Fave Page | Fave Grid | Logged Out")]
    public class LoggedOutFaveGridTests : BaseTest
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
        public void FavesPage_ShouldDisplayExpectedMessage()
        {
            ReportManager.Test.Info("Se verifica mesajele si iconita pe pagina Favorites goala.");

            string emptyFavesTitle = favePage.FaveGrid.GetEmptyListMessageTitle();
            string emptyFavesText = favePage.FaveGrid.GetEmptyListMessageText();
            bool isFaveIconPresent = favePage.FaveGrid.IsFavesIconVisible();

            AssertMessage(emptyFavesTitle, AppMessages.emptyFavesTitle);
            AssertMessage(emptyFavesText, AppMessages.emptyFavesText);
            Assert.That(isFaveIconPresent, Is.True);
        }

        [Test]
        public void FavesPage_ClickingBrowseButton_ShouldRedirectToHome()
        {
            ReportManager.Test.Info("Se da click pe Browse si se verifica redirect-ul catre Home.");
            favePage.FaveGrid.ClickOnBrowse();
            DriverMgr.Wait(1);

            AssertRedirect(AppRoutes.HomePageRoute);
        }

        [Test]
        public void SideMenu_ClickingLogIn_ShouldRedirectToAuth()
        {
            ReportManager.Test.Info("Se da click pe Log In din Side Menu si se verifica redirect-ul catre Auth.");
            favePage.SideMenu.ClickOnLogIn();
            DriverMgr.Wait(1);

            AssertRedirect(AppRoutes.AuthPageRoute);
        }
        #endregion
    }
}