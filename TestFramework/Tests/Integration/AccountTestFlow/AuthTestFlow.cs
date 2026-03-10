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

namespace TestFramework.Tests.Integration.AccountTestFlow
{
    public class AuthTestFlow : BaseTest
    {
        private AuthPage authPage;
        private HomePage homePage;

        #region SETUP 
        [SetUp]
        public void Setup()
        {
            authPage = new AuthPage(DriverMgr);
            authPage.Open();
            authPage.Login.ClickRegisterLink();
            homePage = new HomePage(DriverMgr);
            DriverMgr.Wait(1);
        }
        #endregion

        #region POSITIVE TESTS
        [Test]
        public void Logout_AfterRegister_ShouldSucceed()
        {
            //Arrange
            User dummyUser = RandomIdentityGenerator.GenerateValidUser();
            authPage.Register.RegisterUser(dummyUser);
            homePage.NavBar.logOut();

            //Assert
            string expectedPath = AppRoutes.LocalPath + AppRoutes.AuthPageRoute;
            string currentPath = DriverMgr.GetUrl();
            Assert.That(currentPath, Is.EqualTo(expectedPath));
        }

        [Test]
        public void Login_WithValidData_ShouldSucceed()
        {
            //Arrange
            User dummyUser = RandomIdentityGenerator.GenerateValidUser();
            authPage.Register.RegisterUser(dummyUser);
            homePage.NavBar.logOut();

            //Act
            authPage.Login.LoginUser(dummyUser);

            //Assert
            string expectedPath = AppRoutes.LocalPath + AppRoutes.HomePageRoute;
            string currentPath = DriverMgr.GetUrl();
            Assert.That(currentPath, Is.EqualTo(expectedPath));
        }
        #endregion

        #region NEGATIVE TESTS
        [Test]
        public void Login_WithExistingEmailAndWrongPassword_ShouldFail()
        {
            // Arrange
            User dummyUser = RandomIdentityGenerator.GenerateValidUser();
            authPage.Register.RegisterUser(dummyUser);
            homePage.NavBar.logOut();
            // Act
            authPage.Login.LoginUser(
                dummyUser.Email,
                Passwords.DifferentValidPassword);
            DriverMgr.Wait(2);

            // Assert
            string error = authPage.Login.GetGlobalErrorMessage();
            Assert.That(error, Is.Not.Empty, ErrorMessages.LoginWrongPassword);
        }

        [Test]
        public void LoggingIn_WithNonExistingEmail_ShouldFail()
        {
            User dummyUser = RandomIdentityGenerator.GenerateValidUser();
            authPage.Login.LoginUser(dummyUser);

            string errorMessage = authPage.Login.GetGlobalErrorMessage();
            Assert.That(errorMessage, Is.EqualTo(ErrorMessages.LoginNonExistingEmail));
        }

        [Test]
        public void Register_WithExistingEmail_ShouldFail()
        {
            // Arrange
            User dummyUser = RandomIdentityGenerator.GenerateValidUser();
            authPage.Register.RegisterUser(dummyUser);
            homePage.NavBar.logOut();
            // Act
            authPage.Login.ClickRegisterLink();
            authPage.Register.RegisterUser(dummyUser);
            DriverMgr.Wait(2);

            // Assert
            string error = authPage.Register.GetGlobalErrorMessage();
            Assert.That(error, Is.Not.Empty, ErrorMessages.RegisterExistingEmail);
        }

        [Test]
        public void Register_WithExistingUsername_ShouldFail()
        {
            // Arrange
            User dummyUser = RandomIdentityGenerator.GenerateValidUser();
            User dummierUser = new User
            {
                Email = RandomIdentityGenerator.GenerateEmail(),
                Username = dummyUser.Username,
                Password = dummyUser.Password,
            };
            authPage.Register.RegisterUser(dummyUser);
            homePage.NavBar.logOut();
            // Act

            authPage.Login.ClickRegisterLink();
            authPage.Register.RegisterUser(dummierUser);

            DriverMgr.Wait(2);

            // Assert
            string error = authPage.Register.GetGlobalErrorMessage();
            Assert.That(error, Is.Not.Empty, ErrorMessages.RegisterExistingUsername);
        }
        #endregion
    }
}
