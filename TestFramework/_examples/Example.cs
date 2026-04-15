using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Constants;
using TestFramework.Tests;

namespace TestFramework.Examples
{
    [TestFixture]
    public class Example 
    {
        private IWebDriver driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            driver = new ChromeDriver();
        }

        [SetUp]
        public void BeforeEch()
        {
            driver.Navigate().GoToUrl("https://nunit.org/");
        }

        //[Test]
        //[TestCase("invalidEmail", "Qwerty2@")]
        //[TestCase("test@gmail.com", "Qwerty2@")]
        public void TestLogin(string email, string parola)
        {
            Assert.That(true, "Acest punct a fost atins cu succes.");
        }

        [TearDown]
        public void AfterEach()
        {
        }

        [OneTimeTearDown]
        public void AfterAll()
        {
            driver.Quit();
            driver.Dispose();
        }

    }
}
