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

        private void MoveSlider(By locator, string directionKey)
        {
            var element = _wait.Until(d => d.FindElement(locator));
            element.SendKeys(directionKey);
        }

        public void MoveSliderRightOnePosition(By locator) => MoveSlider(locator, Keys.ArrowRight);
        public void MoveSliderLeftOnePosition(By locator) => MoveSlider(locator, Keys.ArrowLeft);

        public string GetText(By locator)
        {
            var element = WaitForElement(locator);
            return element.Text.Trim();
        }

        public void MoveSliderToValue(By sliderLocator, By labelLocator, string targetValue, string directionKey)
        {
            var slider = WaitForElement(sliderLocator);
            int maxSafetyAttempts = 100; // Protecție împotriva loop-urilor infinite

            while (GetText(labelLocator) != targetValue && maxSafetyAttempts > 0)
            {
                slider.SendKeys(directionKey);
                maxSafetyAttempts--;
            }

            if (maxSafetyAttempts == 0)
                throw new Exception($"Nu s-a putut ajunge la valoarea {targetValue} în 100 de pași.");
        }

        public void GoToUrl(string url) => _driver.Navigate().GoToUrl(url);
        public void QuitBrowser() { _driver?.Quit(); _driver?.Dispose(); }

    }
}
