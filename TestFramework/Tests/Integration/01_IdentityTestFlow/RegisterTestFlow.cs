using NUnit.Framework;
using TestFramework.Constants;
using TestFramework.Models;
using TestFramework.Tests.Integration._01_IdentityTestFlow;
using TestFramework.Utilities;
using TestFramework.Reports.Manager;

namespace TestFramework.Tests.Integration.IdentityTestFlow
{
    [TestFixture]
    [Category("Integration | Identity | Register Flow")]
    internal class RegisterTestFlow : IdentityBaseTest
    {
        #region SETUP
        [SetUp]
        public void Setup()
        {
            ReportManager.Test.Info("Se deschide pagina de autentificare si se navigheaza catre formularul Register.");
            authPage.Open();
            authPage.Login.ClickRegisterLink();
            DriverMgr.Wait(1);
        }
        #endregion

        #region NEGATIVE TESTS
        [Test]
        public void Register_WithExistingEmail_ShouldFail()
        {
            ReportManager.Test.Info("Se inregistreaza un user valid pentru testul de email existent.");
            User user = RegisterUser();

            ReportManager.Test.Info("Se face logout si se incearca register cu acelasi email.");
            Logout();
            authPage.Login.ClickRegisterLink();
            RegisterUser(user);
            DriverMgr.Wait(1);

            ReportManager.Test.Info("Se verifica mesajul de eroare pentru email deja existent.");
            AssertError(authPage.Register.GetGlobalErrorMessage(), ErrorMessages.RegisterExistingEmail);
        }

        [Test]
        public void Register_WithExistingUsername_ShouldFail()
        {
            ReportManager.Test.Info("Se inregistreaza un user valid pentru testul de username existent.");
            User user = RegisterUser();

            ReportManager.Test.Info("Se face logout si se incearca register cu acelasi username.");
            Logout();
            authPage.Login.ClickRegisterLink();

            User dummyUser = new User
            {
                Email = RandomIdentityGenerator.GenerateEmail(),
                Username = user.Username,
                Password = user.Password,
            };

            RegisterUser(dummyUser);

            ReportManager.Test.Info("Se verifica mesajul de eroare pentru username deja existent.");
            AssertError(authPage.Register.GetGlobalErrorMessage(), ErrorMessages.RegisterExistingUsername);
        }
        #endregion
    }
}