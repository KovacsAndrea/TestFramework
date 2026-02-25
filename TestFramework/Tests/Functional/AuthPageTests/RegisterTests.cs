using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Constants;
using TestFramework.Pages.Auth;

namespace TestFramework.Tests.Functional.AuthPageTests
{
    public class RegisterTests : BaseTest
    {
        private AuthPage authPage;

        [SetUp]
        public void Setup()
        {
            authPage = new AuthPage(DriverMgr);
            authPage.Open();
            authPage.Login.ClickRegisterLink();
            DriverMgr.Wait(1);
        }

        [Test]
        public void Register_WithValidData_ShouldCreateUser()
        {
            // Arrange
            string uniqueEmail = $"test{Guid.NewGuid()}@yopmail.com";

            // Act
            authPage.Register.TypeUsername("validUser");
            authPage.Register.TypeEmail(uniqueEmail);
            authPage.Register.TypePassword("StrongPass123!");
            authPage.Register.TypeConfirmPassword("StrongPass123!");
            authPage.Register.ClickRegister();
            DriverMgr.Wait(3);

            // Assert
            string expectedPath = AppRoutes.LocalPath + AppRoutes.HomePageRoute;
            string currentPath = DriverMgr.GetUrl();
            Assert.That(currentPath, Is.EqualTo(expectedPath),
                $"Expected to be redirected to {expectedPath} but was {currentPath}");
        }

        [Test]
        public void Register_WithExistingEmail_ShouldReturnError()
        {
            // Arrange
            string existingEmail = "test@yopmail.com";

            // Act
            authPage.Register.TypeUsername("anotherUser");
            authPage.Register.TypeEmail(existingEmail);
            authPage.Register.TypePassword("StrongPass123!");
            authPage.Register.TypeConfirmPassword("StrongPass123!");
            authPage.Register.ClickRegister();
            DriverMgr.Wait(2);

            // Assert
            string error = authPage.Register.GetGlobalErrorMessage();
            Assert.That(error, Is.Not.Empty, "Acest nume de utilizator este deja folosit. Încearcă altul!");
        }

        [Test]
        public void Register_WithExistingUsername_ShouldReturnError()
        {
            // Arrange
            string existingUsername = "sonic";

            // Act
            authPage.Register.TypeUsername(existingUsername);
            authPage.Register.TypeEmail($"test{Guid.NewGuid()}@yopmail.com");
            authPage.Register.TypePassword("StrongPass123!");
            authPage.Register.TypeConfirmPassword("StrongPass123!");
            authPage.Register.ClickRegister();
            DriverMgr.Wait(2);

            // Assert
            string error = authPage.Register.GetGlobalErrorMessage();
            Assert.That(error, Is.Not.Empty, "Expected error for existing username.");
        }

        [Test]
        public void Register_WithInvalidEmail_ShouldReturnValidationError()
        {
            // Act
            authPage.Register.TypeUsername("userTest");
            authPage.Register.TypeEmail("invalid-email");
            authPage.Register.TypePassword("StrongPass123!");
            authPage.Register.TypeConfirmPassword("StrongPass123!");
            authPage.Register.ClickRegister();
            DriverMgr.Wait(2);

            // Assert
            string emailError = authPage.Register.GetEmailErrorMessage();
            Assert.That(emailError, Is.Not.Empty, "Expected validation error for invalid email.");
        }

        [Test]
        public void Register_WithWeakPassword_ShouldReturnValidationError()
        {
            // Act
            authPage.Register.TypeUsername("userWeakPass");
            authPage.Register.TypeEmail($"test{Guid.NewGuid()}@yopmail.com");
            authPage.Register.TypePassword("123");
            authPage.Register.TypeConfirmPassword("123");
            DriverMgr.Wait(2);

            // Assert
            var failMessages = authPage.Register.GetPasswordFailMessages();
            Assert.That(failMessages.Count, Is.GreaterThan(0), "Weak password validation not triggered.");
        }

        [Test]
        public void Register_WithMissingRequiredFields_ShouldFail()
        {
            // Act
            authPage.Register.ClickRegister();
            DriverMgr.Wait(2);

            // Assert
            string usernameError = authPage.Register.GetUsernameErrorMessage();
            string emailError = authPage.Register.GetEmailErrorMessage();
            string passwordError = authPage.Register.GetPasswordErrorMessage();

            Assert.Multiple(() =>
            {
                Assert.That(usernameError, Is.Not.Empty);
                Assert.That(emailError, Is.Not.Empty);
                Assert.That(passwordError, Is.Not.Empty);
            });
        }

        [Test]
        public void Register_WithMismatchedPasswords_ShouldFail()
        {
            // Act
            authPage.Register.TypeUsername("userMismatch");
            authPage.Register.TypeEmail($"test{Guid.NewGuid()}@yopmail.com");
            authPage.Register.TypePassword("StrongPass123!");
            authPage.Register.TypeConfirmPassword("DifferentPass123!");
            authPage.Register.ClickRegister();
            DriverMgr.Wait(2);

            // Assert
            string confirmPasswordError = authPage.Register.GetConfirmPasswordErrorMessage();
            Assert.That(confirmPasswordError, Is.Not.Empty, "Expected error for mismatched passwords.");
        }
    }
}
