using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Diagnostics;

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
            return _wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public IWebElement WaitForElementToBeClickable(By locator)
        {
            return _wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        public IReadOnlyCollection<IWebElement> FindElements(By locator)
        {
            try
            {
                _wait.Until(d => d.FindElements(locator).Count > 0);
                return _driver.FindElements(locator);
            }
            catch (WebDriverTimeoutException)
            {
                return Array.Empty<IWebElement>();
            }
        }

        public IWebElement? FindElement(By locator)
        {
            try
            {
                var elements = _driver.FindElements(locator);
                return elements.Count > 0 ? elements[0] : null;
            }
            catch (WebDriverTimeoutException)
            {
                return null;
            }
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
            return element.GetAttribute(attribute) ?? string.Empty;
        }

        public IAlert WaitForAlert()
        {
            return _wait.Until(ExpectedConditions.AlertIsPresent());
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

        public string GetText(By locator)
        {
            try
            {
                var element = WaitForElement(locator);
                return element.Text?.Trim() ?? string.Empty;
            }
            catch (WebDriverTimeoutException)
            {
                return string.Empty;
            }
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

        public string GetUrl() { return _driver.Url; }
        public void GoToUrl(string url) => _driver.Navigate().GoToUrl(url);
        public void QuitBrowser()
        {
            try
            {
                _driver?.Quit();
                _driver?.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _driver = null;

                foreach (var process in Process.GetProcessesByName("chromedriver"))
                {
                    try { process.Kill(); } catch { }
                }
            }
        }

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

        public void Wait(int seconds)
        {
            if (seconds <= 0) return;
            System.Threading.Thread.Sleep(seconds * 1000);
        }

        public bool IsElementVisible(By locator)
        {
            return WaitForElement(locator) != null;
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

        public void Refresh()
        {
            _driver.Navigate().Refresh();
        }

    }
}
