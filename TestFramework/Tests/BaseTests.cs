using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Drivers;
using TestFramework.Reports.Manager;

namespace TestFramework.Tests
{
    public class BaseTest
    {
        protected DriverManager DriverMgr;

        [OneTimeSetUp]
        public void StartReport()
        {
            ReportManager.InitReport();
        }

        [SetUp] 
        public void BeforeEach()
        {
            DriverMgr = new DriverManager();
            DriverMgr.StartBrowser();
        }

        [TearDown]
        public void AfterEach()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var errorMessage = TestContext.CurrentContext.Result.Message;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;
            if (status == TestStatus.Failed)
            {
                ReportManager.Test.Fail($"<b>Test Failed!</b><br>Error: {errorMessage}");
                ReportManager.Test.Fail($"<pre>{stackTrace}</pre>");
            }
            else if (status == TestStatus.Passed)
            {
                ReportManager.Test.Pass("Test finalizat cu succes.");
            }
            else if (status == TestStatus.Inconclusive || status == TestStatus.Skipped)
            {
                ReportManager.Test.Skip("Testul a fost sărit sau este inconcludent.");
            }
            DriverMgr.QuitBrowser();
        }

        [OneTimeTearDown]
        public void EndReport()
        {
            ReportManager.Flush();
        }
    }
}
