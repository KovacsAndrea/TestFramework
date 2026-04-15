using NUnit.Framework.Interfaces;
using TestFramework.Constants;
using TestFramework.Drivers;
using TestFramework.Models;
using TestFramework.Reports.Manager;
using TestFramework.Utilities;

namespace TestFramework.Tests
{
    public class BaseTest
    {
        protected DriverManager DriverMgr;

        #region HELPERS
        protected void AssertRedirect(string redirectUrl)
        {
            DriverMgr.Wait(1);
            string expectedPath = AppRoutes.LocalPath + redirectUrl;
            string currentPath = DriverMgr.GetUrl();

            ReportManager.Test.Info($"Se verifica redirectionarea catre: {expectedPath}");

            if (currentPath != expectedPath)
            {
                string message = $"Redirectionare incorecta. Url asteptat: {expectedPath} | Url actual: {currentPath}";

                ReportManager.Test.Fail(message);
                Assert.Fail(message);
            }

            ReportManager.Test.Pass("Redirectionarea a fost realizata corect.");
        }

        protected void AssertError(string actual, string expected)
        {
            ReportManager.Test.Info("Se verifica mesajul de eroare afisat.");

            ReportManager.Test.Info($"Mesaj asteptat: {expected} | Mesaj afisat: {actual}");

            if (actual != expected)
            {
                string message = $"Mesaj de eroare incorect. Expected: {expected} | Actual: {actual}";
                ReportManager.Test.Fail(message);
                Assert.Fail(message);
            }

            ReportManager.Test.Pass("Mesajul de eroare afisat este corect.");
        }

        protected void AssertFieldError(string actual, string expected, string field)
        {
            ReportManager.Test.Info($"Se verifica mesajul de eroare pentru {field}.");
            ReportManager.Test.Info($"Mesaj asteptat: {expected} | Mesaj afisat: {actual}");

            if (actual != expected)
            {
                string message = $"Mesaj incorect pentru {field}. Expected: {expected} | Actual: {actual}";
                ReportManager.Test.Fail(message);
                Assert.Fail(message);
            }

            ReportManager.Test.Info($"Mesajul de eroare pentru {field} este corect.");
        }

        protected void AssertNoFieldError(string actual, string field)
        {
            ReportManager.Test.Info($"Se verifica lipsa mesajului de eroare pentru {field}.");
            ReportManager.Test.Info($"Mesaj afisat: {actual}");

            if (!string.IsNullOrEmpty(actual))
            {
                string message = $"A fost afisat un mesaj de eroare neasteptat pentru {field}: {actual}";
                ReportManager.Test.Fail(message);
                Assert.Fail(message);
            }

            ReportManager.Test.Info($"Nu exista mesaj de eroare pentru {field}, comportamentul este corect.");
        }


        protected void AssertMessage(string actualMessage, string expectedMessage)
        {

            ReportManager.Test.Info("Se verifica mesajul afisat de sistem.");

            ReportManager.Test.Info($"Mesaj asteptat: {expectedMessage} | Mesaj afisat: {actualMessage}");

            if (actualMessage != expectedMessage)
            {
                string message = $"Mesaj incorect. Mesaj asteptat: {expectedMessage} | Mesaj afisat: {actualMessage}";

                ReportManager.Test.Fail(message);
                Assert.Fail(message);
            }

            ReportManager.Test.Pass("Mesajul afisat este corect.");
        }

        protected User GenerateAndLogUser()
        {
            User dummyUser = RandomIdentityGenerator.GenerateValidUser();
            ReportManager.Test.Info($"Generare date valide pentru utilizator: {dummyUser.Username}");
            return dummyUser;
        }

        #endregion

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

            var testName = TestContext.CurrentContext.Test.Name;
            ReportManager.CreateTest(testName);

            foreach (var category in TestContext.CurrentContext.Test.AllPropertyValues("Category"))
            {
                ReportManager.Test.AssignCategory(category.ToString());
            }
        }

        [TearDown]
        public void AfterEach()
        {
            var result = TestContext.CurrentContext.Result;
            var status = result.Outcome.Status;
            var errorMessage = result.Message;
            var stackTrace = result.StackTrace;

            try
            {
                if (status == TestStatus.Failed)
                {
                    string screenshotPath = DriverMgr.TakeScreenshot(TestContext.CurrentContext.Test.Name);

                    ReportManager.Test.Fail($"<b>Testul a esuat.</b><br>Error: {errorMessage}",
                        AventStack.ExtentReports.MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());

                    if (!string.IsNullOrEmpty(stackTrace))
                    {
                        ReportManager.Test.Fail($"<b>Stack Trace:</b><pre>{stackTrace}</pre>");
                    }
                }
                else if (status == TestStatus.Passed)
                {
                    ReportManager.Test.Pass("Test finalizat cu succes.");
                }
                else
                {
                    ReportManager.Test.Skip($"Test status: {status}");
                }
            }
            catch (Exception ex)
            {
                ReportManager.Test.Info("Eroare raportare: " + ex.Message);
            }
            finally
            {
                DriverMgr.QuitBrowser();
            }
        }

        [OneTimeTearDown]
        public void EndReport()
        {
            ReportManager.Flush();
        }
    }
}
