using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Drivers;

namespace TestFramework.Pages.Home
{
    public class NavBarComponent:BasePage
    {
        private readonly By _siteTitle = By.Id("nav-bar-site-title");
        private readonly By _homeIcon = By.Id("nav-bar-home-icon");
        private readonly By _faveIcon = By.Id("nav-bar-favorite-icon");
        private readonly By _cartIcon = By.Id("nav-bar-cart-icon");
        private readonly By _userAvatarLoggedOut = By.Id("nav-bar-user-avatar-logged-out");
        private readonly By _userAvatarLoggedIn = By.Id("nav-bar-user-avatar-logged-in");
        private readonly By _faveBadge = By.Id("//button[@id=\"nav-bar-favorite-icon\"]//span[contains(@class, 'MuiBadge-badge')]");
        private readonly By _cartBadge = By.Id("//button[@id=\"nav-bar-cart-icon\"]//span[contains(@class, 'MuiBadge-badge')]");

        public NavBarComponent(DriverManager driver) : base(driver) { }

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
