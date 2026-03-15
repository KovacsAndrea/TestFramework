using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.GlobalComponents;
using TestFramework.Models;
using TestFramework.Pages;
using TestFramework.Pages.Auth;
using TestFramework.Pages.Cart;
using TestFramework.Pages.Faves;
using TestFramework.Pages.Home;
using TestFramework.Utilities;

namespace TestFramework.Tests.Integration
{
    public class IntegrationBaseTest: BaseTest
    {
        protected AuthPage authPage;
        protected NavBar navBar;

        [SetUp]
        public void SetupIntegration()
        {
            authPage = new AuthPage(DriverMgr);
            navBar = new NavBar(DriverMgr);
        }

        #region HELPERS
        protected User RegisterUser(User? givenUser = null)
        {
            User user = givenUser ?? RandomIdentityGenerator.GenerateValidUser();

            authPage.Register.RegisterUser(user);

            DriverMgr.Wait(1);

            return user;
        }

        protected void Login(User user)
        {
            authPage.Login.LoginUser(user);
        }

        protected void Logout()
        {
            navBar.logOut();
        }

        protected void NavigateToCart()
        {
            navBar.clickOnCart();
            DriverMgr.Wait(1);
        }

        protected void NavigateHome()
        {
            navBar.clickOnLogo();
            DriverMgr.Wait(1);
        }

        protected void NavigateToFavorites()
        {
            navBar.clickOnFavorites();
            DriverMgr.Wait(1);
        }

        protected void NavigateToProfile()
        {
            navBar.navigateToProfile();
            DriverMgr.Wait(1);
        }
        #endregion
    }
}
