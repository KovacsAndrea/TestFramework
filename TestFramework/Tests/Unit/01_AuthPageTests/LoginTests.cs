using System;
using NUnit.Framework;
using TestFramework.Constants;
using TestFramework.Pages.Auth;
using TestFramework.Reports.Manager;
using TestFramework.Utilities;

namespace TestFramework.Tests.Unit.AuthPageTests
{
    [TestFixture]
    [Category("Unit | Auth Page | Login")]
    public class LoginTests : BaseTest
    {
        private AuthPage authPage;

        #region SETUP
        [SetUp]
        public void Setup()
        {
            authPage = new AuthPage(DriverMgr);
            ReportManager.Test.Info("Se initializeaza pagina de autentificare.");
            authPage.Open();
            ReportManager.Test.Info("Pagina de login a fost deschisa cu succes.");
        }
        #endregion

        #region POSITIVE TESTS
        [Test]
        public void Login_ClickingVisitAsGuest_ShouldRedirectToHome()
        {
            ReportManager.Test.Info("Se incearca inaintarea ca vizitator.");

            authPage.Login.ClickGuestLink();
            DriverMgr.Wait(1);

            ReportManager.Test.Info("S-a dat click pe link-ul 'Visit as Guest'.");

            AssertRedirect(AppRoutes.HomePageRoute);
        }
        #endregion

        #region NEGATIVE TESTS
        [Test]
        public void Email_WithInvalidFormat_ShouldReturnError()
        {
            ReportManager.Test.Info($"Introducere email invalid: {Emails.InvalidFormat}");

            authPage.Login.TypeEmail(Emails.InvalidFormat);
            DriverMgr.Wait(2);

            AssertFieldError(authPage.Login.GetEmailErrorMessage(), ErrorMessages.LoginInvalidEmail, "email");
        }

        [Test]
        public void Login_WithMissingFields_ShouldReturnError()
        {
            ReportManager.Test.Info("Click pe Login fara a completa campurile.");

            authPage.Login.ClickLogin();
            DriverMgr.Wait(2);

            AssertFieldError(authPage.Login.GetEmailErrorMessage(), ErrorMessages.GlobalAuthRequiredField, "email");
            AssertFieldError(authPage.Login.GetPasswordErrorMessage(), ErrorMessages.GlobalAuthRequiredField, "password");
        }

        [Test]
        public void Login_WithMissingEmail_ShouldReturnError()
        {
            ReportManager.Test.Info("Click pe Login fara a completa campul de email.");
            authPage.Login.TypePassword(Passwords.ValidPassword);
            authPage.Login.ClickLogin();
            DriverMgr.Wait(2);

            AssertFieldError(authPage.Login.GetEmailErrorMessage(), ErrorMessages.GlobalAuthRequiredField, "email");
        }

        [Test]
        public void Login_WithMissingPassword_ShouldReturnError()
        {
            ReportManager.Test.Info("Click pe Login fara a completa campul de parola.");
            authPage.Login.TypePassword(Passwords.ValidPassword);
            authPage.Login.ClickLogin();
            DriverMgr.Wait(2);

            AssertFieldError(authPage.Login.GetEmailErrorMessage(), ErrorMessages.GlobalAuthRequiredField, "email");
        }
        #endregion
    }
}