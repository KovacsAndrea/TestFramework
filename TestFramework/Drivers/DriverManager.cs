using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace TestFramework.Drivers
{
    public class DriverManager
    {
        private IWebDriver _driver = null!;
        private WebDriverWait _wait = null!;

        public void StartBrowser()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            _driver = new ChromeDriver(options);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public IWebElement WaitForElement(By locator)
        {
            return _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }

        public IWebElement WaitForElementToBeClickable(By locator)
        {
            return _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
        }

        public IAlert WaitForAlert()
        {
            // Așteptăm să apară alerta (uneori are un mic delay)
            return _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
        }

        public void AcceptAlert()
        {
            var alert = WaitForAlert();
            alert.Accept();
        }

        public void DismissAlert()
        {
            var alert = WaitForAlert();
            alert.Dismiss();
        }

        public string GetAlertText()
        {
            var alert = WaitForAlert();
            return alert.Text ?? string.Empty;
        }

        public IReadOnlyCollection<IWebElement> FindElements(By locator)
        {
            // Asteptam sa apara macar primul element din lista inainte sa le luam pe toate
            _wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
            return _driver.FindElements(locator);
        }

        public IWebElement? FindElement(By locator)
        {
            try
            {
                var elements = _driver.FindElements(locator);
                return elements.Count > 0 ? elements[0] : null;
            }
            catch
            {
                return null;
            }
        }

        public bool IsElementVisible(By locator)
        {
            return WaitForElement(locator) != null;
        }

        public void SendKeys(By locator, string text)
        {
            var element = WaitForElementToBeClickable(locator);
            element.Clear();
            element.SendKeys(text);
        }

        public void Click(By locator)
        {
            var element = WaitForElementToBeClickable(locator);
            element.Click();
        }

        public string GetAttribute(By locator, string attribute)
        {
            var element = WaitForElement(locator);
            return element.GetAttribute(attribute) ?? String.Empty;
        }

        public void Wait(int seconds)
        {
            if (seconds <= 0) return;
            System.Threading.Thread.Sleep(seconds * 1000);
        }

        public void MoveSliderRight(By locator, int positions)
        {
            var element = WaitForElement(locator);
            for (int i = 0; i < positions; i++)
            {
                element.SendKeys(Keys.ArrowRight);
            }
        }

        public void MoveSliderLeft(By locator, int position)
        {
            var element = WaitForElement(locator);
            for(int i = 0;i < position; i++)
            {
                element.SendKeys(Keys.ArrowLeft);
            }
        }

        public string GetText(By locator)
        {
            var element = WaitForElement(locator);
            return element.Text.Trim();
        }

        public int GetBadgeNumber(By locator)
        {
            var badge = WaitForElement(locator);
            if (badge != null && !string.IsNullOrEmpty(badge.Text))
            {
                TestContext.Out.WriteLine(badge.Text);
                return int.TryParse(badge.Text.Trim(), out int result) ? result : 0;
            }

            return 0; 
        }

        public void MoveSliderToValue(By sliderLocator, By labelLocator, int targetValue, string directionKey)
        {
            var slider = WaitForElementToBeClickable(sliderLocator);
            int maxSafetyAttempts = 150; 

            while (maxSafetyAttempts > 0)
            {
                string currentText = GetText(labelLocator);
                if (int.TryParse(currentText, out int currentValue))
                {
                    if (currentValue == targetValue)
                    {
                        return;
                    }
                }
                slider.SendKeys(directionKey);
                maxSafetyAttempts--;
            }
            if (maxSafetyAttempts == 0)
            {
                throw new Exception($"Nu s-a putut ajunge la valoarea {targetValue} in 150 de pasi. Directia folosita: {directionKey}");
            }
        }

        public string GetUrl() { return _driver.Url; }
        public void GoToUrl(string url) => _driver.Navigate().GoToUrl(url);
        public void QuitBrowser() { _driver?.Quit(); _driver?.Dispose(); }

        public string TakeScreenshot(string testName)
        {
            string screenshotDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", "Screenshots");
            if (!Directory.Exists(screenshotDir)) Directory.CreateDirectory(screenshotDir);

            foreach (char c in Path.GetInvalidFileNameChars())
            {
                testName = testName.Replace(c, '_');
            }

            string fileName = $"{testName}_{DateTime.Now:HHmmss}.png";
            string filePath = Path.Combine(screenshotDir, fileName);

            Screenshot screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
            screenshot.SaveAsFile(filePath);

            return Path.Combine("Screenshots", fileName);
        }

        public void Refresh()
        {
            _driver.Navigate().Refresh();
        }

    }
}
