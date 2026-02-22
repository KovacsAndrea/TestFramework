using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Drivers;

namespace TestFramework.Pages.Auth
{
    public class RegisterComponent(DriverManager driver) : BasePage(driver)
    {
        private const string _container = "//div[@id='register-component-container']";

        private readonly By _registerTitle = By.Id("register-title");

        // ===== EMAIL =====
        private const string _emailContainer = _container + "/*[2]";
        private readonly By _emailInput = By.XPath(_emailContainer + "//input");
        private readonly By _emailErrorMessage = By.XPath(_emailContainer + "//p");

        // ===== USERNAME =====
        private const string _usernameContainer = _container + "/*[3]";
        private readonly By _usernameInput = By.XPath(_usernameContainer + "//input");
        private readonly By _usernameErrorMessage = By.XPath(_usernameContainer + "//p");

        // ===== PASSWORD =====
        private const string _passwordContainer = _container + "/*[4]";
        private readonly By _passwordInput = By.XPath(_passwordContainer + "//input");
        private readonly By _showPasswordButton = By.XPath(_passwordContainer + "//button");
        private readonly By _passwordPassMessages = By.XPath(_passwordContainer + "//div[contains(@class,'password-dropdown')]/p[contains(@class,'ok')]");
        private readonly By _passwordFailMessages = By.XPath(_passwordContainer + "//div[contains(@class,'password-dropdown')]/p[contains(@class,'error')]");

        // ===== CONFIRM PASSWORD =====
        private const string _confirmPasswordContainer = _container + "/*[5]";
        private readonly By _confirmPasswordInput = By.XPath(_confirmPasswordContainer + "//input");
        private readonly By _confirmPasswordErrorMessage = By.XPath(_confirmPasswordContainer + "//p");

        // ===== GLOBAL ELEMENTS =====
        private readonly By _globalErrorMessage = By.Id("register-error-message");
        private readonly By _registerButton = By.Id("register-button");
        private readonly By _loginLink = By.Id("login-link");
        private readonly By _guestLink = By.Id("register-guest-link");

        // ===== EMAIL =====
            public void TypeEmail(string email)
            {
                DriverMgr.SendKeys(_emailInput, email);
            }

            public string GetEmailErrorMessage()
            {
                return DriverMgr.GetText(_emailErrorMessage);
            }

            // ===== USERNAME =====
            public void TypeUsername(string username)
            {
                DriverMgr.SendKeys(_usernameInput, username);
            }

            public string GetUsernameErrorMessage()
            {
                return DriverMgr.GetText(_usernameErrorMessage);
            }

            // ===== PASSWORD =====
            public void TypePassword(string password)
            {
                DriverMgr.SendKeys(_passwordInput, password);
            }

            public void ClickShowPassword()
            {
                DriverMgr.Click(_showPasswordButton);
            }

            public IReadOnlyCollection<string> GetPasswordFailMessages()
            {
                var elements = DriverMgr.FindElements(_passwordFailMessages);
                return elements.Select(e => e.Text.Trim()).ToList().AsReadOnly();
            }

            public IReadOnlyCollection<string> GetPasswordPassMessages()
            {
                var elements = DriverMgr.FindElements(_passwordPassMessages);
                return elements.Select(e => e.Text.Trim()).ToList().AsReadOnly();
            }

        // ===== CONFIRM PASSWORD =====
        public void TypeConfirmPassword(string password)
            {
                DriverMgr.SendKeys(_confirmPasswordInput, password);
            }

            public string GetConfirmPasswordErrorMessage()
            {
                return DriverMgr.GetText(_confirmPasswordErrorMessage);
            }

            // ===== GLOBAL =====
            public void ClickRegister()
            {
                DriverMgr.Click(_registerButton);
            }

            public string GetGlobalErrorMessage()
            {
                return DriverMgr.GetText(_globalErrorMessage);
            }

            public void ClickLoginLink()
            {
                DriverMgr.Click(_loginLink);
            }

            public void ClickGuestLink()
            {
                DriverMgr.Click(_guestLink);
            }

            // ============================================================
            // ====================== HIGH LEVEL ==========================
            // ============================================================

            public void FillRegisterForm(string email, string username, string password, string confirmPassword)
            {
                TypeEmail(email);
                TypeUsername(username);
                TypePassword(password);
                TypeConfirmPassword(confirmPassword);
            }

            public void Register(string email, string username, string password, string confirmPassword)
            {
                FillRegisterForm(email, username, password, confirmPassword);
                ClickRegister();
            }

        }
    }
