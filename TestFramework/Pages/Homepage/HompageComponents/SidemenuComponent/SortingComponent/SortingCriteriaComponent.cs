using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Drivers;

namespace TestFramework.Pages.Homepage
{
    public class SortingCriteriaComponent: BasePage
    {

        private readonly By _noneCheckbox = By.XPath("//div[@id=\"sort-criteria-section\"]/*[1]");
        private readonly By _titleCheckbox = By.XPath("//div[@id=\"sort-criteria-section\"]/*[2]");
        private readonly By _authorCheckbox = By.XPath("//div[@id=\"sort-criteria-section\"]/*[3]");
        private readonly By _yearCheckbox = By.XPath("//div[@id=\"sort-criteria-section\"]/*[4]");
        private readonly By _priceCheckbox = By.XPath("//div[@id=\"sort-criteria-section\"]/*[5]");

        public SortingCriteriaComponent(DriverManager driver) : base(driver) { }

        public void CheckNone()
        {
            DriverMgr.Click(_noneCheckbox);
        }

        public void CheckTitle()
        {
            DriverMgr.Click(_titleCheckbox);
        }

        public void CheckAuthor()
        {
            DriverMgr.Click(_authorCheckbox);
        }

        public void CheckYear()
        {
            DriverMgr.Click(_yearCheckbox);
        }

        public void CheckPrice()
        {
            DriverMgr.Click(_priceCheckbox);
        }
    }
}
