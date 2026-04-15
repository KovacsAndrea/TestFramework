using System;
using NUnit.Framework;
using TestFramework.Constants;
using TestFramework.GlobalComponents;
using TestFramework.Models;
using TestFramework.Pages.Auth;
using TestFramework.Pages.Faves;
using TestFramework.Reports.Manager;

namespace TestFramework.Tests.Unit.FavesPage
{
    [Category("Unit | Fave Page | Fave Side Menu | Logged In")]
    [TestFixture(LocatorType.Id)]
    [TestFixture(LocatorType.XPath)]
    public class LoggedInFaveSideMenuTests : BaseTest
    {
        private FavePage favePage;
        private AuthPage authPage;
        private NavBar navBar;
        private readonly LocatorType _locatorType;

        public LoggedInFaveSideMenuTests(LocatorType locatorType)
        {
            _locatorType = locatorType;
        }

        #region SETUP
        [SetUp]
        public void Setup()
        {
            favePage = new FavePage(DriverMgr);
            authPage = new AuthPage(DriverMgr);
            navBar = new NavBar(DriverMgr);

            favePage.SideMenu.LocatorMode = _locatorType;

            ReportManager.Test.Info("Se deschide pagina de autentificare si se face login cu user cunoscut.");
            authPage.Open();
            authPage.Login.LoginUser(KnownUsers.UserWithNoFavorites);
            DriverMgr.Wait(1);

            ReportManager.Test.Info("Se acceseaza pagina Favorites din NavBar.");
            navBar.clickOnFavorites();
            DriverMgr.Wait(1);
        }
        #endregion

        #region HELPERS
        private void ExecutaClickMeniu(string actiune)
        {
            ReportManager.Test.Info($"Se da click pe actiunea '{actiune}' din Side Menu.");
            switch (actiune)
            {
                case "Comenzi": favePage.SideMenu.ClickOnOrders(); break;
                case "Vouchere": favePage.SideMenu.ClickOnVouchers(); break;
                case "My Wallet": favePage.SideMenu.ClickOnMyWallet(); break;
                case "Support": favePage.SideMenu.ClickOnSupport(); break;
                case "Cardurile mele": favePage.SideMenu.ClickOnMyCards(); break;
                case "Service": favePage.SideMenu.ClickOnService(); break;
                case "Retururile mele": favePage.SideMenu.ClickOnMyReturns(); break;
                case "Review-urile mele": favePage.SideMenu.ClickOnMyReviews(); break;
                case "Adrese de livrare": favePage.SideMenu.ClickOnDeliveryAddresses(); break;
                case "Date facturare": favePage.SideMenu.ClickOnBillingDetails(); break;
                case "Setari siguranta": favePage.SideMenu.ClickOnSafetySettings(); break;
                default: throw new ArgumentException($"Actiunea {actiune} nu este definita.");
            }
        }
        #endregion

        #region POSITIVE TESTS
        [Test]
        [TestCase("Comenzi", AppRoutes.OrderHistoryRoute)]
        [TestCase("Vouchere", AppRoutes.VouchersRoute)]
        [TestCase("My Wallet", AppRoutes.MyWalletRoute)]
        [TestCase("Support", AppRoutes.SupportRoute)]
        [TestCase("Cardurile mele", AppRoutes.MyCreditCardsRoute)]
        [TestCase("Service", AppRoutes.ServiceRoute)]
        [TestCase("Retururile mele", AppRoutes.MyReturnsRoute)]
        [TestCase("Review-urile mele", AppRoutes.MyReviewsRoute)]
        [TestCase("Adrese de livrare", AppRoutes.DeliveryAddressesRoute)]
        [TestCase("Date facturare", AppRoutes.BillingDetailsRoute)]
        [TestCase("Setari siguranta", AppRoutes.SafetySettingsRoute)]
        public void SideMenu_Navigation_ShouldRedirectToCorrectPage(string menuAction, string path)
        {
            ExecutaClickMeniu(menuAction);
            DriverMgr.Wait(1);

            ReportManager.Test.Info($"Se verifica redirect-ul catre ruta: {path}");
            AssertRedirect(path);
        }
        #endregion
    }
}