using System;
using NUnit.Framework;
using TestFramework.Constants;
using TestFramework.Pages.Auth;
using TestFramework.Reports.Manager;

namespace TestFramework.Tests.Functional.AuthPageTests
{
    [TestFixture]
    [Category("Functional | Auth Page | Login")]
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
            DriverMgr.Wait(1);

            ReportManager.Test.Info("Pagina de login a fost deschisa cu succes.");
        }
        #endregion

        #region POSITIVE TESTS
        [Test]
        public void Login_ClickingVisitAsGuest_ShouldRedirectToHome()
        {
            ReportManager.Test.Info("Se incearca login ca vizitator (Guest).");

            authPage.Login.ClickGuestLink();
            DriverMgr.Wait(1);

            ReportManager.Test.Info("S-a dat click pe link-ul 'Visit as Guest'.");

            AssertRedirect(AppRoutes.HomePageRoute);
        }
        #endregion

        #region NEGATIVE TESTS
        [Test]
        [TestCase(Emails.InvalidFormat)]
        [TestCase(Emails.MissingTopLevelDomain)]
        [TestCase(Emails.InvalidDomainFormat)]
        public void Login_WithInvalidEmail_ShouldReturnError(string invalidEmail)
        {
            ReportManager.Test.Info($"Introducere email invalid: {invalidEmail}");

            authPage.Login.LoginUser(invalidEmail, Passwords.ValidPassword);
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
        #endregion
    }
}