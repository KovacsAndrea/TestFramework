using OpenQA.Selenium;
using System;
using TestFramework.Drivers;
using TestFramework.Models;

namespace TestFramework.Pages.Faves.Components
{
    public class SideMenuComponent : BasePage
    {
        public LocatorType LocatorMode { get; set; } = LocatorType.XPath;

        public SideMenuComponent(DriverManager driver) : base(driver)
        {
        }

        // Quick Actions (By Id)
        private readonly By _ordersQuickItemById = By.Id("quick-order-history");
        private readonly By _vouchersQuickItemById = By.Id("quick-vouchers");
        private readonly By _myWalletQuickItemById = By.Id("quick-my-wallet");
        private readonly By _supportQuickItemById = By.Id("quick-support");

        // Detailed Menu (By Id)
        private readonly By _myCardsSideItemById = By.Id("menu-my-credit-cards");
        private readonly By _serviceSideItemById = By.Id("menu-service");
        private readonly By _myReturnsSideItemById = By.Id("menu-my-returns");
        private readonly By _myReviewsSideItemById = By.Id("menu-my-reviews");
        private readonly By _deliveryAddressesSideItemById = By.Id("menu-delivery-addresses");
        private readonly By _billingDetailsSideItemById = By.Id("menu-billing-details");
        private readonly By _safetySettingsSideItemById = By.Id("menu-safety-settings");

        // Quick Actions (Robust XPath)
        private readonly By _ordersQuickItemByXPath = By.XPath("//div[@class='quick-actions']/div[1]");
        private readonly By _vouchersQuickItemByXPath = By.XPath("//div[@class='quick-actions']/div[2]");
        private readonly By _myWalletQuickItemByXPath = By.XPath("//div[@class='quick-actions']/div[3]");
        private readonly By _supportQuickItemByXPath = By.XPath("//div[@class='quick-actions']/div[4]");

        // Detailed Menu (Robust XPath)
        private readonly By _myCardsSideItemByXPath = By.XPath("//div[@class='detailed-menu']/div[1]/div[@class='menu-left']");
        private readonly By _serviceSideItemByXPath = By.XPath("//div[@class='detailed-menu']/div[2]/div[@class='menu-left']");
        private readonly By _myReturnsSideItemByXPath = By.XPath("//div[@class='detailed-menu']/div[3]/div[@class='menu-left']");
        private readonly By _myReviewsSideItemByXPath = By.XPath("//div[@class='detailed-menu']/div[4]/div[@class='menu-left']");
        private readonly By _deliveryAddressesSideItemByXPath = By.XPath("//div[@class='detailed-menu']/div[5]/div[@class='menu-left']");
        private readonly By _billingDetailsSideItemByXPath = By.XPath("//div[@class='detailed-menu']/div[6]/div[@class='menu-left']");
        private readonly By _safetySettingsSideItemByXPath = By.XPath("//div[@class='detailed-menu']/div[7]/div[@class='menu-left']");

        private readonly By _loggedOutButton = By.Id("logged-out-component-button");

        private By GetLocator(By idLocator, By xpathLocator)
        {
            return LocatorMode == LocatorType.Id ? idLocator : xpathLocator;
        }

        // Quick Actions
        public void ClickOnOrders() => DriverMgr.Click(GetLocator(_ordersQuickItemById, _ordersQuickItemByXPath));
        public void ClickOnVouchers() => DriverMgr.Click(GetLocator(_vouchersQuickItemById, _vouchersQuickItemByXPath));
        public void ClickOnMyWallet() => DriverMgr.Click(GetLocator(_myWalletQuickItemById, _myWalletQuickItemByXPath));
        public void ClickOnSupport() => DriverMgr.Click(GetLocator(_supportQuickItemById, _supportQuickItemByXPath));

        // Detailed Menu
        public void ClickOnMyCards() => DriverMgr.Click(GetLocator(_myCardsSideItemById, _myCardsSideItemByXPath));
        public void ClickOnService() => DriverMgr.Click(GetLocator(_serviceSideItemById, _serviceSideItemByXPath));
        public void ClickOnMyReturns() => DriverMgr.Click(GetLocator(_myReturnsSideItemById, _myReturnsSideItemByXPath));
        public void ClickOnMyReviews() => DriverMgr.Click(GetLocator(_myReviewsSideItemById, _myReviewsSideItemByXPath));
        public void ClickOnDeliveryAddresses() => DriverMgr.Click(GetLocator(_deliveryAddressesSideItemById, _deliveryAddressesSideItemByXPath));
        public void ClickOnBillingDetails() => DriverMgr.Click(GetLocator(_billingDetailsSideItemById, _billingDetailsSideItemByXPath));
        public void ClickOnSafetySettings() => DriverMgr.Click(GetLocator(_safetySettingsSideItemById, _safetySettingsSideItemByXPath));

        public void ClickOnLogIn() => DriverMgr.Click(_loggedOutButton);
    }
}