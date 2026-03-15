using NUnit.Framework;
using TestFramework.Constants;
using TestFramework.Models;
using TestFramework.Tests.Integration._01_IdentityTestFlow;
using TestFramework.Utilities;
using TestFramework.Reports.Manager;

namespace TestFramework.Tests.Integration.IdentityTestFlow
{
    [TestFixture]
    [Category("Integration | Identity | Login Flow")]
    public class LoginTestFlow : IdentityBaseTest
    {
        #region SETUP
        [SetUp]
        public void Setup()
        {
            ReportManager.Test.Info("Se deschide pagina de autentificare.");
            authPage.Open();
            DriverMgr.Wait(1);
        }
        #endregion

        #region POSITIVE TESTS
        [Test]
        public void Login_WithValidData_ShouldSucceed()
        {
            ReportManager.Test.Info("Se inregistreaza un user valid pentru test.");
            authPage.Login.ClickRegisterLink();
            User user = RegisterUser();

            ReportManager.Test.Info("Se face logout si se incearca login cu date valide.");
            Logout();
            Login(user);

            ReportManager.Test.Info("Se verifica redirect-ul catre Home Page.");
            AssertRedirect(AppRoutes.HomePageRoute);
        }
        #endregion

        #region NEGATIVE TESTS
        [Test]
        public void Login_WithExistingEmailAndWrongPassword_ShouldFail()
        {
            ReportManager.Test.Info("Se inregistreaza un user valid pentru test.");
            authPage.Login.ClickRegisterLink();
            User user = RegisterUser();

            ReportManager.Test.Info("Se face logout si se incearca login cu parola gresita.");
            Logout();
            authPage.Login.LoginUser(user.Email, Passwords.DifferentValidPassword);

            ReportManager.Test.Info("Se verifica mesajul de eroare pentru parola gresita.");
            AssertError(authPage.Login.GetGlobalErrorMessage(), ErrorMessages.LoginWrongPassword);
        }

        [Test]
        public void Login_WithNonExistingEmail_ShouldFail()
        {
            ReportManager.Test.Info("Se genereaza un user care nu exista in baza de date.");
            User user = RandomIdentityGenerator.GenerateValidUser();

            ReportManager.Test.Info("Se incearca login cu date inexistente.");
            Login(user);

            ReportManager.Test.Info("Se verifica mesajul de eroare pentru email inexistent.");
            AssertError(authPage.Login.GetGlobalErrorMessage(), ErrorMessages.LoginNonExistingEmail);
        }
        #endregion
    }
}