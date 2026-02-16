using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestFramework.Drivers
{
    internal class DriverManager
    {
        public IWebDriver Driver { get; private set; }

        public void StartBrowser()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");

            Driver = new ChromeDriver(options);
        }

        public void QuitBrowser()
        {
            Driver.Quit();
            Driver.Dispose();
        }
    }
}
