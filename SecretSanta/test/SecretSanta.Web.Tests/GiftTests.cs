using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using SecretSanta.Web.Api;
using System.Collections.Generic;

namespace SecretSanta.Web.Tests
{
    [TestClass]
    public class GiftTests
    {
        static Process? ApiHostProcess { get; set; }
        static Process? WebHostProcess { get; set; }
        static string AppUrl { get; } = "https://localhost:44388";
        static string WebUrl { get; } = "https://localhost:44394";
        [NotNull]
        private TestContext? TestContextInstance;
        [NotNull]
        private IWebDriver? Driver;

        private static async void AddUser()
        {
            using HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(AppUrl);
            UserClient client = new UserClient(http);
            ICollection<User> users = await client.GetAllAsync();
            if (users.Count <= 0)
            {
                UserInput spongebob = new UserInput
                {
                    FirstName = "Spongebob",
                    LastName = "Squarepants",
                    SantaId = 1
                };
                client.PostAsync(spongebob);
            }
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void Add_Selenium_Success()
        {
            //Arrange
            Driver.Navigate().GoToUrl(WebUrl + "/Gifts");
            Driver.FindElement(By.CssSelector("#CreateButton")).Click();
            Driver.FindElement(By.Id("TitleInput")).SendKeys("MONEY MONEY MONEY");
            Driver.FindElement(By.Id("DescriptionInput")).SendKeys("A five letter word for happiness");
            Driver.FindElement(By.Id("UrlInput")).SendKeys("https://en.wikipedia.org/wiki/Mr._Krabs");
            IWebElement userDropDown = Driver.FindElement(By.Id("UserDropdown"));
            SelectElement selectUserDropdown = new SelectElement(userDropDown);
            selectUserDropdown.SelectByText("Spongebob Squarepants");

            //Act
            Driver.FindElement(By.Id("submit")).Click();
            Screenshot ss = ((ITakesScreenshot)Driver).GetScreenshot();
            string path = "../../../" + "UpdatedGiftsScreenshot.png";
            ss.SaveAsFile(path);
            //Assert
            String moneyGift = Driver.FindElement(By.XPath(".//*[@id='GiftTable']//td[contains(.,'MONEY MONEY MONEY')]")).Text;
            Assert.IsNotNull(moneyGift);
        }
    
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
            AddUser();
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



        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            ApiHostProcess = Process.Start(
                "dotnet.exe",
                "run -p ..\\..\\..\\..\\..\\src\\SecretSanta.Api\\");

            WebHostProcess = Process.Start(
                "dotnet.exe",
                "run -p ..\\..\\..\\..\\..\\src\\SecretSanta.Web\\");
            ApiHostProcess.WaitForExit(6000);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            ApiHostProcess?.Kill();
            ApiHostProcess?.Close();
            WebHostProcess?.Kill();
            WebHostProcess?.Close();
        }
    }
}
