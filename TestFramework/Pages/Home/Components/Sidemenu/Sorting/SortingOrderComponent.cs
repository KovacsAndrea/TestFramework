using OpenQA.Selenium;
using TestFramework.Drivers;

namespace TestFramework.Pages.Home
{
    public class SortingOrderComponent: BasePage
    {
        public SortingOrderComponent(DriverManager driver) : base(driver) { }

        private readonly By _noneCheckbox = By.XPath("//div[@id=\"sort-order-section\"]/*[1]");
        private readonly By _ascendingCheckbox = By.XPath("//div[@id=\"sort-order-section\"]/*[2]");
        private readonly By _descendingCheckbox = By.XPath("//div[@id=\"sort-order-section\"]/*[3]");

        public void CheckNone()
        {
            DriverMgr.Click(_noneCheckbox);
        }

        public void CheckAscending() {
            DriverMgr.Click(_ascendingCheckbox);
        }

        public void CheckDescending() { 
            DriverMgr.Click(_descendingCheckbox);
        }
    }
}
