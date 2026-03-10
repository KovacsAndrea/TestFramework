using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TestFramework.Constants;
using TestFramework.Models;
using TestFramework.Pages.Auth;
using TestFramework.Utilities;
using TestFramework.Reports.Manager;

namespace TestFramework.Tests.Functional.AuthPageTests
{
    public class LoginTests : BaseTest
    {
        private AuthPage authPage;

        #region SETUP
        [SetUp]
        public void Setup()
        {
            ReportManager.CreateTest(TestContext.CurrentContext.Test.Name);

            authPage = new AuthPage(DriverMgr);
            authPage.Open();
            ReportManager.Test.Info("Auth page opened");
            DriverMgr.Wait(1);
        }
        #endregion

        #region POSITIVE TESTS
        [Test]
        public void Login_ClickingVisitAsGuest_ShouldRedirectToHome()
        {
            // Act
            ReportManager.Test.Info("Clicking guest link");
            authPage.Login.ClickGuestLink();
            DriverMgr.Wait(1);

            // Assert
            string expectedPath = AppRoutes.LocalPath + AppRoutes.HomePageRoute;
            string currentPath = DriverMgr.GetUrl();

            ReportManager.Test.Info($"Expected URL: {expectedPath}");
            ReportManager.Test.Info($"Current URL: {currentPath}");

            Assert.That(currentPath, Is.EqualTo(expectedPath));

            ReportManager.Test.Pass("User successfully redirected to home page");
        }
        #endregion

        #region NEGATIVE TESTS
        [Test]
        [TestCase(Emails.InvalidFormat)]
        [TestCase(Emails.MissingTopLevelDomain)]
        [TestCase(Emails.InvalidDomainFormat)]
        public void Login_WithInvalidEmail_ShouldReturnError(string invalidEmail)
        {
            // Act
            ReportManager.Test.Info($"Attempting login with invalid email: {invalidEmail}");

            authPage.Login.LoginUser(
                invalidEmail,
                Passwords.ValidPassword);

            DriverMgr.Wait(2);

            // Assert
            string emailError = authPage.Login.GetEmailErrorMessage();

            ReportManager.Test.Info($"Email error message returned: {emailError}");

            Assert.That(emailError, Is.EqualTo(ErrorMessages.LoginInvalidEmail));

            ReportManager.Test.Pass("Correct validation message displayed for invalid email");
        }

        [Test]
        public void Login_WithMissingFields_ShouldReturnError()
        {
            // Act
            ReportManager.Test.Info("Attempting login with missing fields");

            authPage.Login.ClickLogin();
            DriverMgr.Wait(2);

            // Assert
            string emailError = authPage.Login.GetEmailErrorMessage();
            string passwordError = authPage.Login.GetPasswordErrorMessage();

            ReportManager.Test.Info($"Email error: {emailError}");
            ReportManager.Test.Info($"Password error: {passwordError}");

            Assert.That(emailError, Is.EqualTo(ErrorMessages.GlobalAuthRequiredField));
            Assert.That(passwordError, Is.EqualTo(ErrorMessages.GlobalAuthRequiredField));

            ReportManager.Test.Pass("Validation messages displayed for missing fields");
        }
        #endregion
    }
}