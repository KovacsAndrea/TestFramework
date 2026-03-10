using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Constants;
using TestFramework.Models;
using TestFramework.Pages.Auth;
using TestFramework.Pages.Home;
using TestFramework.Utilities;

namespace TestFramework.Tests.Functional.AuthPageTests
{
    public class RegisterTests : BaseTest
    {
        private AuthPage authPage;

        #region SETUP
        [SetUp]
        public void Setup()
        {
            authPage = new AuthPage(DriverMgr);
            authPage.Open();
            authPage.Login.ClickRegisterLink();
            DriverMgr.Wait(1);
        }
        #endregion

        #region POSITIVE TESTS
        [Test]
        public void Register_WithValidData_ShouldCreateUser()
        {
            //Arrange
            User dummyUser = RandomIdentityGenerator.GenerateValidUser();
            // Act
            authPage.Register.RegisterUser(dummyUser);
            DriverMgr.Wait(3);

            // Assert
            string expectedPath = AppRoutes.LocalPath + AppRoutes.HomePageRoute;
            string currentPath = DriverMgr.GetUrl();
            Assert.That(currentPath, Is.EqualTo(expectedPath));
        }
        #endregion

        #region NEGATIVE TESTS
        [Test]
        [TestCase(Emails.InvalidFormat)]
        [TestCase(Emails.MissingTopLevelDomain)]
        [TestCase(Emails.InvalidDomainFormat)]
        public void Register_WithInvalidEmail_ShouldReturnError(string invalidEmail)
        {
            // Act
            authPage.Register.RegisterUser(
                invalidEmail,
                RandomIdentityGenerator.GenerateUsername(),
                Passwords.ValidPassword,
                Passwords.ValidPassword);

            DriverMgr.Wait(2);

            // Assert
            string emailError = authPage.Register.GetEmailErrorMessage();
            Assert.That(emailError, Is.EqualTo(ErrorMessages.RegisterInvalidEmail));
        }


        [Test]
        [TestCase(Passwords.MissingLengthCheck, ErrorMessages.RegisterPasswordLength)]
        [TestCase(Passwords.MissingLowercaseCheck, ErrorMessages.RegisterPasswordLowerCase)]
        [TestCase(Passwords.MissingUpperCaseCheck, ErrorMessages.RegisterPasswordUpperCase)]
        [TestCase(Passwords.MissingNumberCheck, ErrorMessages.RegisterPassowrdNumber)]
        [TestCase(Passwords.MissingSpecialCharacterCheck, ErrorMessages.RegisterPasswordSpecialCharacter)]
        public void Register_WithWeakPassword_ShouldReturnValidationError(string password, string expectedError)
        {
            // Act
            authPage.Register.TypeEmail(RandomIdentityGenerator.GenerateEmail());
            authPage.Register.TypeUsername(RandomIdentityGenerator.GenerateUsername());
            authPage.Register.TypePassword(password);
            authPage.Register.TypeConfirmPassword(password);

            DriverMgr.Wait(2);

            // Assert
            var failMessages = authPage.Register.GetPasswordFailMessages();
            var passMessages = authPage.Register.GetPasswordPassMessages();
            var confirmPasswordMessage = authPage.Register.GetConfirmPasswordErrorMessage();

            Assert.That(failMessages, Does.Contain(expectedError));
            Assert.That(failMessages.Count, Is.EqualTo(1));
            Assert.That(passMessages.Count, Is.EqualTo(4));
            Assert.That(confirmPasswordMessage, Is.EqualTo(ErrorMessages.RegisterConfirmPasswordWeak));
        }

        [Test]
        public void Register_WithMismatchedPasswords_ShouldReturnError()
        {
            // Act
            authPage.Register.TypeEmail(RandomIdentityGenerator.GenerateEmail());
            authPage.Register.TypeUsername(RandomIdentityGenerator.GenerateUsername());
            authPage.Register.TypePassword(Passwords.ValidPassword);
            authPage.Register.TypeConfirmPassword(Passwords.DifferentValidPassword);
            authPage.Register.ClickRegister();
            DriverMgr.Wait(2);

            // Assert
            string confirmPasswordError = authPage.Register.GetConfirmPasswordErrorMessage();
            Assert.That(confirmPasswordError, Is.EqualTo(ErrorMessages.RegisterConfirmPasswordNotMatching));
        }

        [Test]
        public void Register_WithMissingRequiredFields_ShouldReturnError()
        {
            // Act
            authPage.Register.ClickRegister();
            DriverMgr.Wait(2);

            // Assert
            string usernameError = authPage.Register.GetUsernameErrorMessage();
            string emailError = authPage.Register.GetEmailErrorMessage();
            string passwordError = authPage.Register.GetPasswordError();
            string confirmPasswordError = authPage.Register.GetConfirmPasswordErrorMessage();

            Assert.That(usernameError, Is.EqualTo(ErrorMessages.GlobalAuthRequiredField));
            Assert.That(emailError, Is.EqualTo(ErrorMessages.GlobalAuthRequiredField));
            Assert.That(passwordError, Is.EqualTo(ErrorMessages.GlobalAuthRequiredField));
            Assert.That(confirmPasswordError, Is.EqualTo(ErrorMessages.GlobalAuthRequiredField));
        }
        #endregion
    }
}
