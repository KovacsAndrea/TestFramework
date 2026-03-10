using System;
using System.IO;

namespace TestFramework.Reports.Manager
{
    public static class ReportManager
    {
        private static AventStack.ExtentReports.ExtentReports _extent;
        public static AventStack.ExtentReports.ExtentTest Test;

        public static void InitReport()
        {
            if (_extent != null) return;

            string reportDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");
            if (!Directory.Exists(reportDir)) Directory.CreateDirectory(reportDir);

            string reportPath = Path.Combine(reportDir, "index.html");

            // Folosim numele complet ca să forțăm compilatorul să-l găsească
            var spark = new AventStack.ExtentReports.Reporter.ExtentSparkReporter(reportPath);

            _extent = new AventStack.ExtentReports.ExtentReports();
            _extent.AttachReporter(spark);
        }

        public static void CreateTest(string testName)
        {
            if (_extent == null) InitReport();
            Test = _extent.CreateTest(testName);
        }

        public static void Flush()
        {
            _extent?.Flush();
        }
    }
}