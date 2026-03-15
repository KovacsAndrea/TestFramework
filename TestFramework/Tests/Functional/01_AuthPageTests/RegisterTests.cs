using System;
using NUnit.Framework;
using TestFramework.Constants;
using TestFramework.Models;
using TestFramework.Pages.Auth;
using TestFramework.Utilities;
using TestFramework.Reports.Manager;

namespace TestFramework.Tests.Functional.AuthPageTests
{
    [TestFixture]
    [Category("Functional | Auth Page | Register")]
    public class RegisterTests : BaseTest
    {
        private AuthPage authPage;

        #region SETUP
        [SetUp]
        public void Setup()
        {
            authPage = new AuthPage(DriverMgr);

            ReportManager.Test.Info("Se initializeaza pagina de autentificare si se deschide formularul Register.");
            authPage.Open();
            authPage.Login.ClickRegisterLink();
            DriverMgr.Wait(1);

            ReportManager.Test.Info("Pagina de register a fost deschisa cu succes.");
        }
        #endregion

        #region HELPERS
        private void FillRegisterForm(User user)
        {
            ReportManager.Test.Info("Completare formular register cu urmatoarele date:");
            authPage.Register.TypeEmail(user.Email);
            authPage.Register.TypeUsername(user.Username);
            authPage.Register.TypePassword(user.Password);
            authPage.Register.TypeConfirmPassword(user.Password);
            DriverMgr.Wait(1);
        }
        private void FillRegisterForm(string email, string username, string password, string confirmPassword)
        {
            ReportManager.Test.Info("Completare formular register cu urmatoarele date.");
            authPage.Register.RegisterUser(
                email: email,
                username: username,
                password: password,
                confirmPassword: confirmPassword
                );
            DriverMgr.Wait(1);
        }
        #endregion

        #region POSITIVE TESTS
        [Test]
        public void Register_WithValidData_ShouldCreateUser()
        {
            User user = GenerateAndLogUser();
            FillRegisterForm(user);
            authPage.Register.ClickRegister();
            DriverMgr.Wait(3);

            AssertRedirect(AppRoutes.HomePageRoute);
        }
        #endregion

        #region NEGATIVE TESTS
        [Test]
        [TestCase(Emails.InvalidFormat)]
        [TestCase(Emails.MissingTopLevelDomain)]
        [TestCase(Emails.InvalidDomainFormat)]
        public void Register_WithInvalidEmail_ShouldReturnError(string invalidEmail)
        {
            ReportManager.Test.Info($"Incercare inregistrare cu email invalid: {invalidEmail}");

            FillRegisterForm(
                invalidEmail,
                RandomIdentityGenerator.GenerateUsername(),
                Passwords.ValidPassword,
                Passwords.ValidPassword);

            AssertFieldError(authPage.Register.GetEmailErrorMessage(), ErrorMessages.RegisterInvalidEmail, "email");
        }

        [Test]
        public void Register_WithMismatchedPasswords_ShouldReturnError()
        {
            ReportManager.Test.Info("Introducere parole care nu coincid.");

            FillRegisterForm(
                RandomIdentityGenerator.GenerateEmail(),
                RandomIdentityGenerator.GenerateUsername(),
                Passwords.ValidPassword,
                Passwords.DifferentValidPassword);

            authPage.Register.ClickRegister();

            AssertFieldError(authPage.Register.GetConfirmPasswordErrorMessage(), ErrorMessages.RegisterConfirmPasswordNotMatching, "confirm password");
        }

        [Test]
        public void Register_WithMissingRequiredFields_ShouldReturnError()
        {
            ReportManager.Test.Info("Trimitere formular gol.");
            authPage.Register.ClickRegister();
            DriverMgr.Wait(1);

            ReportManager.Test.Info("Verificare mesaje de camp obligatoriu pentru toate campurile.");

            AssertFieldError(authPage.Register.GetUsernameErrorMessage(), ErrorMessages.GlobalAuthRequiredField, "username");
            AssertFieldError(authPage.Register.GetEmailErrorMessage(), ErrorMessages.GlobalAuthRequiredField, "email");
            AssertFieldError(authPage.Register.GetPasswordError(), ErrorMessages.GlobalAuthRequiredField, "password");
            AssertFieldError(authPage.Register.GetConfirmPasswordErrorMessage(), ErrorMessages.GlobalAuthRequiredField, "confirm password");
        }
        #endregion
    }
}