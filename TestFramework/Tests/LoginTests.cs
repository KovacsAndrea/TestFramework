using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Drivers;
using System.Threading;


namespace TestFramework.Tests
{
    internal class LoginTests
    {
        private DriverManager _driverManager;

        [SetUp]
        public void Setup()
        {
            _driverManager = new DriverManager();
            _driverManager.StartBrowser();
        }

        [Test]
        public void Navigate_To_Login_Page()
        {
            _driverManager.Driver.Navigate()
                .GoToUrl("http://localhost:5173/login");

            Thread.Sleep(3000); // așteaptă 3 secunde

            Assert.That(_driverManager.Driver.Url,
                Is.EqualTo("http://localhost:5173/login"));
        }

        [TearDown]
        public void TearDown()
        {
            _driverManager.QuitBrowser();
        }
    
    }
}
