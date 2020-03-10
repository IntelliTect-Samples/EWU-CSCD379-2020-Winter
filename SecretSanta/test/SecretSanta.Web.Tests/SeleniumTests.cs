using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace SecretSanta.Web.Tests
{
    [TestClass]
    public class SeleniumTests
    {
        [NotNull]
        public TestContext? TestContext { get; set; }

        [NotNull]
        private IWebDriver? Driver { get; set; }

        private static Process? ApiHostProcess { get; set; }

        private static Process? WebHostProcess { get; set; }
        static string AppURL { get; } = "https://localhost:44394/";
        static string ApiURL { get; } = "https://localhost:44388";



        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            if (testContext is null)
                throw new ArgumentNullException(nameof(testContext));

              ApiHostProcess = Process.Start("dotnet.exe", $@"run -p ..\..\src\SecretSanta.Api\SecretSanta.Api.csproj --urls={ApiURL}");
              WebHostProcess = Process.Start("dotnet.exe", $@"run -p ..\..\src\SecretSanta.Web\SecretSanta.Web.csproj --urls={AppURL}");
             
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            ApiHostProcess?.CloseMainWindow();
            ApiHostProcess?.Close();
            WebHostProcess?.CloseMainWindow();
            WebHostProcess?.Close();
        }



        [TestMethod]
        [TestCategory("Chrome")]
        public void NavigateToHome_Success()
        {

            Driver.Navigate().GoToUrl(new Uri(AppURL));
            Thread.Sleep(5000);
            Driver.Navigate().GoToUrl(AppURL + "Gifts");
            Thread.Sleep(5000);

        }


        [TestInitialize()]
        public void SetupTest()
        {
            Driver = new ChromeDriver();
           

            Driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 10);
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            Driver.Quit();
        }

    }
}
