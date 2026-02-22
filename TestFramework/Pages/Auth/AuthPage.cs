using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Constants;
using TestFramework.Drivers;

namespace TestFramework.Pages.Auth
{
    public class AuthPage : BasePage
    {
        private readonly string _basePath = AppRoutes.LocalPath + AppRoutes.AuthPageRoute;

        private LoginComponent _login = null!;
        private RegisterComponent _register = null!;

        public LoginComponent Login => _login ??= new LoginComponent(DriverMgr);

        public RegisterComponent Register =>  _register ??= new RegisterComponent(DriverMgr);

        public AuthPage(DriverManager driver) : base(driver)
        {
        }

        public void Open()
        {
            DriverMgr.GoToUrl(_basePath);
        }
    }
}
