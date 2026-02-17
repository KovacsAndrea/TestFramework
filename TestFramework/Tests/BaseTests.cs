using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Drivers;

namespace TestFramework.Tests
{
    public class BaseTest
    {
        // Protected ca să poată fi folosit în clasele de test (ex: LoginTests)
        protected DriverManager DriverMgr;

        [SetUp] // Rulează înainte de FIECARE test
        public void BeforeEach()
        {
            DriverMgr = new DriverManager();
            DriverMgr.StartBrowser();
        }

        [TearDown] // Rulează după FIECARE test
        public void AfterEach()
        {
            DriverMgr.QuitBrowser();
        }
    }
}
