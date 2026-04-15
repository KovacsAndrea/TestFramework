using System;
using NUnit.Framework;
using TestFramework.Constants;
using TestFramework.Models;
using TestFramework.Pages.Auth;
using TestFramework.Utilities;
using TestFramework.Reports.Manager;

namespace TestFramework.Tests.Unit.AuthPageTests
{
    [TestFixture]
    [Category("Unit | Auth Page | Register")]
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
        [TestCase(Usernames.UsernameMinValid)]
        [TestCase(Usernames.UsernameMaxValid)]
        public void Username_WithValidBoundaryValues_ShouldNotReturnError(string username)
        {
            ReportManager.Test.Info($"Username testat: {username}");

            authPage.Register.TypeUsername(username);

            string errormesasge = authPage.Register.GetUsernameErrorMessage();

            AssertNoFieldError(errormesasge, "username");
        }

        [Test]
        public void Password_WithValidValue_ShouldNotReturnError()
        {
            string validPassword = Passwords.ValidPassword;
            ReportManager.Test.Info($"Parola testata: {validPassword}");

            authPage.Register.TypePassword(validPassword);

            var errorMessages = authPage.Register.GetPasswordFailMessages().ToList();
            int errorCount = errorMessages.Count;

            if (errorCount > 0)
            {
                ReportManager.Test.Fail($"Eroare: Nu se asteptau mesaje de eroare pentru parola '{validPassword}', dar s-au gasit {errorCount}.");

                foreach (var msg in errorMessages)
                {
                    ReportManager.Test.Info($"Mesaj de eroare neasteptat: {msg}");
                }

                Assert.Fail($"Test esuat: Au aparut {errorCount} erori pentru o parola valida.");
            }
            else
            {
                ReportManager.Test.Pass("Nicio eroare detectata. Parola a fost acceptata corect.");
            }
        }

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
        public void Register_WithAllMissingRequiredFields_ShouldReturnError()
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

        [Test]
        public void Register_WithoutUsername_ShouldShowUsernameRequiredError()
        {
            ReportManager.Test.Info("Trimitere formular cu username gol, restul valide.");

            authPage.Register.TypeEmail(RandomIdentityGenerator.GenerateEmail());
            authPage.Register.TypePassword(Passwords.ValidPassword);
            authPage.Register.TypeConfirmPassword(Passwords.ValidPassword);
            authPage.Register.ClickRegister();

            DriverMgr.Wait(1);

            AssertFieldError(
                authPage.Register.GetUsernameErrorMessage(),
                ErrorMessages.GlobalAuthRequiredField,
                "username"
            );
        }

        [Test]
        public void Register_WithoutEmail_ShouldShowEmailRequiredError()
        {
            ReportManager.Test.Info("Trimitere formular cu email gol, restul valide.");

            authPage.Register.TypeUsername(RandomIdentityGenerator.GenerateUsername());
            authPage.Register.TypePassword(Passwords.ValidPassword);
            authPage.Register.TypeConfirmPassword(Passwords.ValidPassword);
            authPage.Register.ClickRegister();

            DriverMgr.Wait(1);

            AssertFieldError(
                authPage.Register.GetEmailErrorMessage(),
                ErrorMessages.GlobalAuthRequiredField,
                "email"
            );
        }

        [Test]
        public void Register_WithoutPassword_ShouldShowPasswordRequiredError()
        {
            ReportManager.Test.Info("Trimitere formular cu password gol, restul valide.");

            authPage.Register.TypeUsername(RandomIdentityGenerator.GenerateUsername());
            authPage.Register.TypeEmail(RandomIdentityGenerator.GenerateEmail());
            authPage.Register.TypeConfirmPassword(Passwords.ValidPassword);
            authPage.Register.ClickRegister();

            DriverMgr.Wait(1);

            AssertFieldError(
                authPage.Register.GetPasswordError(),
                ErrorMessages.GlobalAuthRequiredField,
                "password"
            );
        }

        [Test]
        public void Register_WithoutConfirmPassword_ShouldShowConfirmPasswordRequiredError()
        {
            ReportManager.Test.Info("Trimitere formular cu confirm password gol, restul valide.");

            authPage.Register.TypeUsername(RandomIdentityGenerator.GenerateUsername());
            authPage.Register.TypeEmail(RandomIdentityGenerator.GenerateEmail());
            authPage.Register.TypePassword(Passwords.ValidPassword);
            authPage.Register.ClickRegister();

            DriverMgr.Wait(1);

            AssertFieldError(
                authPage.Register.GetConfirmPasswordErrorMessage(),
                ErrorMessages.GlobalAuthRequiredField,
                "confirm password"
            );
        }

        [Test]
        [TestCase(Emails.InvalidFormat)]
        [TestCase(Emails.MissingTopLevelDomain)]
        [TestCase(Emails.InvalidDomainFormat)]
        public void Email_WithInvalidFormat_ShouldReturnError(string invalidEmail)
        {
            ReportManager.Test.Info($"Email testat: {invalidEmail}");

            FillRegisterForm(
                invalidEmail,
                RandomIdentityGenerator.GenerateUsername(),
                Passwords.ValidPassword,
                Passwords.ValidPassword);

            AssertFieldError(authPage.Register.GetEmailErrorMessage(), ErrorMessages.RegisterInvalidEmail, "email");
        }

        [Test]
        [TestCase(Usernames.UsernameTooShort, ErrorMessages.UsernameTooShort)]
        [TestCase(Usernames.UsernameTooLong, ErrorMessages.UsernameTooLong)]
        public void Username_WithInvalidBoundaryValues_ShouldReturnError(string username, string expectedError)
        {
            ReportManager.Test.Info($"Username testat: {username}");

            authPage.Register.TypeUsername(username);
            DriverMgr.Wait(5);
            AssertFieldError(
                authPage.Register.GetUsernameErrorMessage(),
                expectedError,
                "username");
        }

        [Test]
        [TestCase(Passwords.MissingLengthCheck, ErrorMessages.RegisterPasswordLength)]
        [TestCase(Passwords.MissingLowercaseCheck, ErrorMessages.RegisterPasswordLowerCase)]
        [TestCase(Passwords.MissingUpperCaseCheck, ErrorMessages.RegisterPasswordUpperCase)]
        [TestCase(Passwords.MissingNumberCheck, ErrorMessages.RegisterPassowrdNumber)]
        [TestCase(Passwords.MissingSpecialCharacterCheck, ErrorMessages.RegisterPasswordSpecialCharacter)]
        public void Password_WithInvalidCriteria_ShouldReturnError(string password, string expectedError)
        {
            ReportManager.Test.Info($"Parola testata: {password}");

            authPage.Register.TypePassword(password);

            var errorMessages = authPage.Register.GetPasswordFailMessages().ToList();
            int errorCount = errorMessages.Count;

            if (errorCount != 1)
            {
                ReportManager.Test.Fail($"Eroare: Se astepta un singur mesaj de eroare, dar s-au gasit {errorCount}.");

                foreach (var msg in errorMessages)
                {
                    ReportManager.Test.Info($"Mesaj de eroare detectat: {msg}");
                }

                Assert.Fail($"Test esuat: Numar incorect de erori (Asteptat: 1, Primite: {errorCount}).");
            }
            else
            {
                AssertFieldError(
                    errorMessages.First(),
                    expectedError,
                    "password");
            }
        }


        [Test]
        public void Passwords_WithMismatchedValues_ShouldReturnError()
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
        #endregion
    }
}