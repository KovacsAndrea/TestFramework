using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Drivers;
using TestFramework.Pages;

namespace TestFramework.GlobalComponents
{
    public class NavBar : BasePage
    {
        private readonly By _siteTitle = By.Id("nav-bar-site-title");
        private readonly By _homeIcon = By.Id("nav-bar-home-icon");
        private readonly By _faveIcon = By.Id("nav-bar-favorite-icon");
        private readonly By _cartIcon = By.Id("nav-bar-cart-icon");
        private readonly By _userAvatarLoggedOut = By.Id("nav-bar-user-avatar-logged-out");
        private readonly By _userAvatarLoggedIn = By.Id("nav-bar-user-avatar-logged-in");
        private readonly By _faveBadge = By.XPath("//span[@id='nav-bar-favorite-count-badge']/span");
        private readonly By _cartBadge = By.XPath("//span[@id='nav-bar-cart-count-badge']/span");
        private readonly By _avatarDropDownProfileButton = By.Id("avatar-drop-down-profile-section");
        private readonly By _avatarDropDownLogoutButton = By.Id("avatar-drop-down-logout-section");

        public NavBar(DriverManager driver) : base(driver) { }

        public void clickOnLogo()
        {
            DriverMgr.Click(_siteTitle);
        }

        public void clickOnHomeIcon()
        {
            DriverMgr.Click(_homeIcon);
        }

        public void clickOnFavorites()
        {
            DriverMgr.Click(_faveIcon);
        }

        public void clickOnCart()
        {
            DriverMgr.Click(_cartIcon);
        }

        public void clickOnLoggedOutUserAvatar()
        {
            DriverMgr.Click(_userAvatarLoggedOut);
        }

        public void clickOnLoggedInUserAvatar()
        {
            DriverMgr.Click(_userAvatarLoggedIn);
        }

        public void navigateToProfile()
        {
            DriverMgr.Click(_userAvatarLoggedIn);
            DriverMgr.Click(_avatarDropDownProfileButton);
        }

        public void logOut()
        {
            DriverMgr.Click(_userAvatarLoggedIn);
            DriverMgr.Click(_avatarDropDownLogoutButton);
        }
        public int GetCartBadgeCount()
        {
            return DriverMgr.GetBadgeNumber(_cartBadge);
        }

        public int GetFaveBadgeCount()
        {
            return DriverMgr.GetBadgeNumber(_faveBadge);
        }
    }
}
