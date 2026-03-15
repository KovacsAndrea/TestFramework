using NUnit.Framework;
using TestFramework.Constants;
using TestFramework.Models;
using TestFramework.Tests.Integration._01_IdentityTestFlow;
using TestFramework.Reports.Manager;

namespace TestFramework.Tests.Integration.IdentityTestFlow
{
    [TestFixture]
    [Category("Integration | Identity | Logout Flow")]
    internal class LogoutTestFlow : IdentityBaseTest
    {
        #region SETUP
        [SetUp]
        public void Setup()
        {
            ReportManager.Test.Info("Se deschide pagina de autentificare pentru testul de logout.");
            authPage.Open();
            authPage.Login.ClickRegisterLink();
            DriverMgr.Wait(1);
        }
        #endregion

        #region POSITIVE TESTS
        [Test]
        public void Logout_AfterRegister_ShouldSucceed()
        {
            ReportManager.Test.Info("Se inregistreaza un user pentru testul de logout.");
            User user = RegisterUser();

            ReportManager.Test.Info("Se face logout prin NavBar.");
            navBar.logOut();

            ReportManager.Test.Info("Se verifica redirect-ul catre Auth Page.");
            AssertRedirect(AppRoutes.AuthPageRoute);
        }
        #endregion
    }
}