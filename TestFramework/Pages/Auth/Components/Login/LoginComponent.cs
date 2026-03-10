using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Drivers;
using TestFramework.Models;

namespace TestFramework.Pages.Auth
{
    public class LoginComponent(DriverManager driver) : BasePage(driver)
    {
        private const string _container = "//div[@id='login-component-container']";

        private readonly By _loginTitle = By.Id("login-title");

        private const string _emailContainer = _container + "/*[2]";
        private readonly By _emailInput = By.XPath(_emailContainer + "//input");
        private readonly By _emailErrorMessage = By.XPath(_emailContainer + "//p");

        private const string _passwordContainer = _container + "/*[3]";
        private readonly By _passwordInput = By.XPath(_passwordContainer + "//input");
        private readonly By _showPasswordButton = By.XPath(_passwordContainer + "//button");
        private readonly By _passwordErrorMessage = By.XPath(_passwordContainer + "//p");

        private readonly By _globalErrorMessage = By.Id("login-error-message");
        private readonly By _loginButton = By.Id("login-button");
        private readonly By _registerLink = By.Id("register-link");
        private readonly By _guestLink = By.Id("login-guest-link");

        public string GetLoginTitle()
        {
            return DriverMgr.GetText(_loginTitle);
        }

        public string GetEmailErrorMessage()
        {
            return DriverMgr.GetText(_emailErrorMessage);
        }

        public string GetPasswordErrorMessage()
        {
            return DriverMgr.GetText(_passwordErrorMessage);
        }

        public string GetGlobalErrorMessage()
        {
            return DriverMgr.GetText(_globalErrorMessage);
        }

        public void TypeEmail(string email)
        {
            DriverMgr.SendKeys(_emailInput, email);
        }

        public void TypePassword(string password)
        {
            DriverMgr.SendKeys(_passwordInput, password);
        }

        public void ClickShowPassword()
        {
            DriverMgr.Click(_showPasswordButton);
        }

        public void ClickLogin()
        {
            DriverMgr.Click(_loginButton);
        }

        public void ClickRegisterLink()
        {
            DriverMgr.Click(_registerLink);
        }

        public void ClickGuestLink()
        {
            DriverMgr.Click(_guestLink);
        }

        public void LoginUser(string email, string password)
        {
            TypeEmail(email);
            TypePassword(password);
            ClickLogin();
        }

        public void LoginUser(User user)
        {
            TypeEmail(user.Email);
            TypePassword(user.Password);
            ClickLogin();
        }

    }
}
