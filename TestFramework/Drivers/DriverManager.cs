using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace TestFramework.Drivers
{
    public class DriverManager
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        public void StartBrowser()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            _driver = new ChromeDriver(options);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public IWebElement WaitForElement(By locator, int timeoutInSeconds = 10)
        {
            return _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }

        public IWebElement WaitForElementToBeClickable(By locator, int timeoutInSeconds = 10)
        {
            return _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
        }

        // Heavy Lifting: Metodă care scrie TEXT doar după ce câmpul e gata
        public void SendKeys(By locator, string text)
        {
            var element = WaitForElementToBeClickable(locator);
            element.Clear();
            element.SendKeys(text);
        }

        // Heavy Lifting: Click sigur
        public void Click(By locator)
        {
            var element = WaitForElementToBeClickable(locator);
            element.Click();
        }

        // Pentru extragerea de atribute (iconițe, clase CSS)
        public string GetAttribute(By locator, string attribute)
        {
            return WaitForElement(locator).GetAttribute(attribute);
        }

        public void Wait(int seconds)
        {
            if (seconds <= 0) return;
            System.Threading.Thread.Sleep(seconds * 1000);
        }


        public void GoToUrl(string url) => _driver.Navigate().GoToUrl(url);
        public void QuitBrowser() { _driver?.Quit(); _driver?.Dispose(); }

    }
}
