using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics.CodeAnalysis;
using SecretSanta.Web.Api;
using System.Net.Http;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Extensions;
using System.Diagnostics;
using System.IO;

namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class GiftTests
    {
        [NotNull]
        public TestContext? TestContext { get; set; }

        [NotNull]
        private IWebDriver? Driver { get; set; }

        private static Process? ApiHostProcess { get; set; }
        private static Process? WebHostProcess { get; set; }

        string ApiURL { get; } = "https://localhost:44388/";
        string AppURL { get; } = "https://localhost:44394/";

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            _ = testContext ?? throw new ArgumentNullException(nameof(testContext));

            ApiHostProcess = Process.Start("dotnet.exe", "run -p ..\\..\\..\\..\\..\\src\\SecretSanta.Api\\SecretSanta.Api.csproj");
            WebHostProcess = Process.Start("dotnet.exe", "run -p ..\\..\\..\\..\\..\\src\\SecretSanta.Web\\SecretSanta.Web.csproj");
            ApiHostProcess.WaitForExit(10000);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            ApiHostProcess?.Kill();
            ApiHostProcess?.CloseMainWindow();
            ApiHostProcess?.Close();
            WebHostProcess?.Kill();
            WebHostProcess?.CloseMainWindow();
            WebHostProcess?.Close();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("ignore-certificate-errors");
            Driver = new ChromeDriver(options);
            Driver.Manage().Timeouts().ImplicitWait = new System.TimeSpan(0, 0, 10);
        }

        [TestMethod]
        public void VerifySiteIsRunning()
        {
            Driver.Navigate().GoToUrl(new Uri(AppURL));
            string text = Driver.FindElement(By.XPath("/html/body/section/div/p")).Text;
            Assert.IsTrue(text.Contains("Welcome to your secret santa app"));
        }

        [TestMethod]
        public async Task AddGift_Success()
        {
            await CreateUser();
            string testTitle = "TestTitle";
            string testDescription = "TestDescription";
            string testUrl = "www.Test.com";
            Driver.Navigate().GoToUrl(new Uri(AppURL + "Gifts"));

            Driver.FindElement(By.CssSelector("button[class='button is-secondary']")).Click();

#pragma warning disable CA1303 // Passing literals as localized parameters
            Driver.FindElements(By.CssSelector(".input"))[0].SendKeys(testTitle);
            Driver.FindElements(By.CssSelector(".input"))[1].SendKeys(testDescription);
            Driver.FindElements(By.CssSelector(".input"))[2].SendKeys(testUrl);
#pragma warning restore CA1303 // Passing literals as localized parameters
            new SelectElement(Driver.FindElement(By.CssSelector("body > section > div > div > div > div.modal-content > div:nth-child(4) > div > select"))).SelectByIndex(0);

            Driver.FindElement(By.Id("submit")).Click();

            Driver.Manage().Timeouts().ImplicitWait = new System.TimeSpan(0, 0, 5);
            var table = Driver.FindElement(By.TagName("table"));
            var rows = table.FindElements(By.TagName("tr"));
            var testRow = rows[rows.Count - 1];

            Assert.AreEqual(("TestTitle", "TestDescription", "www.Test.com"),
                (testRow.FindElements(By.TagName("td"))[1].Text,
                testRow.FindElements(By.TagName("td"))[2].Text,
                testRow.FindElements(By.TagName("td"))[3].Text));

            string path = Directory.GetCurrentDirectory() + "TestScreenshot.png";
            ((ITakesScreenshot)Driver).GetScreenshot().SaveAsFile(path, ScreenshotImageFormat.Png);
            TestContext.AddResultFile(path);
        }

        private async Task CreateUser()
        {
            using HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.BaseAddress = new Uri(ApiURL);
            IUserClient userClient = new UserClient(httpClient);

            var users = await userClient.GetAllAsync();

            if (users.Count == 0)
            {
                UserInput userInput = new UserInput
                {
                    FirstName = "Inigo",
                    LastName = "Montoya"
                };
                await userClient.PostAsync(userInput);
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Driver.Quit();
        }
    }
}
