﻿using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace SecretSanta.Web.Tests
{
    [TestClass]
    public class GiftTests
    {
        [NotNull]
        private TestContext? TestContextInstance;
        [NotNull]
        private IWebDriver? Driver;

        [TestMethod]
        [TestCategory("Chrome")]
        public void Add_Selenium_Success()
        {
            //Arrange
            Driver.Navigate().GoToUrl(AppUrl + "/Gift");
            Driver.FindElement(By.Id("CreateButton")).Click();
            Driver.FindElement(By.Id("TitleInput")).SendKeys("MONEY MONEY MONEY");
            Driver.FindElement(By.Id("DescriptionInput")).SendKeys("A five letter word for happiness");
            Driver.FindElement(By.Id("UrlInput")).SendKeys("https://en.wikipedia.org/wiki/Mr._Krabs");
            IWebElement userDropDown = Driver.FindElement(By.Id("UserDropdown"));
            SelectElement selectUserDropdown = new SelectElement(userDropDown);
            selectUserDropdown.SelectByText("Inigo Montoya");

            //Act
            ITakesScreenshot scrShot = ((ITakesScreenshot)Driver);
            Driver.FindElement(By.Id("submit")).Click();
            //Assert
        }
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
                return TestContextInstance;
            }
            set
            {
                TestContextInstance = value;
            }
        }

        [TestInitialize()]
        public void SetupTest()
        {

            string browser = "Chrome";
            switch (browser)
            {
                case "Chrome":
                    Driver = new ChromeDriver();
                    break;
                default:
                    Driver = new ChromeDriver();
                    break;
            }
            Driver.Manage().Timeouts().ImplicitWait = new System.TimeSpan(0, 0, 10);
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            Driver.Quit();
        }

        static Process? ApiHostProcess { get; set; }
        static Process? WebHostProcess { get; set; }
        string AppUrl { get; } = "https://localhost:44388";
        static string WebUrl { get; } = "https://localhost:44394";

        //[ClassInitialize]
        //public static void ClassInitialize(TestContext testContext)
        //{
        //    string ApiProjectPath = "../../../../../src/SecretSanta.Api";
        //    ApiHostProcess = Process.Start(
        //        "dotnet.exe",
        //        "ApiProjectPath");

        //    string WebProjectPath = "../../../../../src/SecretSanta.Web";
        //    WebHostProcess = Process.Start(
        //        "dotnet.exe",
        //        "WebProjectPath");
        //}

        //[ClassCleanup]
        //public static void ClassCleanup()
        //{
        //    ApiHostProcess?.Close();
        //    WebHostProcess?.Close();
        //}
    }
}
