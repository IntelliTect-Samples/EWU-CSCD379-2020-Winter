using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace SecretSanta.Web.Tests
{
    [TestClass]
    public class GiftTests
    {
        [NotNull]
        private TestContext? testContextInstance;
        [NotNull]
        private IWebDriver? driver;

        private string appURL;

        //[TestMethod]
        //[TestCategory("Chrome")]
        //public void TheBingSearchTest()
        //{
        //    driver.Navigate().GoToUrl(appURL + "/");
        //    driver.FindElement(By.Id("sb_form_q")).SendKeys("Azure Pipelines");
        //    driver.FindElement(By.Id("sb_form_go")).Click();
        //    driver.FindElement(By.XPath("//ol[@id='b_results']/li/h2/a/strong[3]")).Click();
        //    Assert.IsTrue(driver.Title.Contains("Azure Pipelines"), "Verified title of the page");
        //}

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestInitialize()]
        public void SetupTest()
        {
            appURL = "http://www.bing.com/";

            string browser = "Chrome";
            switch (browser)
            {
                case "Chrome":
                    driver = new ChromeDriver();
                    break;
                case "Firefox":
                    driver = new FirefoxDriver();
                    break;
                case "IE":
                    driver = new InternetExplorerDriver();
                    break;
                default:
                    driver = new ChromeDriver();
                    break;
            }

        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            driver.Quit();
        }

        static string WebUrl { get; } = "https://localhost:5011";
        static Process? ApiHostProcess { get; set; }
        static Process? WebHostProcess { get; set; }


        string AppUrl { get; } = "https://localhost:44394";

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            string ApiProjectPath = "";
            ApiHostProcess = Process.Start(
                "dotnet.exe",
                "ApiProjectPath");
        }

        [ClassCleanup]
        public void ClassCleanup()
        {
            ApiHostProcess?.Close();
            ApiHostProcess?.CloseMainWindow();
        }
    }
}
