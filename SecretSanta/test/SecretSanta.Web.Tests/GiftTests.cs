using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using SecretSanta.Web.Api;

namespace SecretSanta.Web.Tests
{

    [TestClass]
    public class GiftTests
    {

        [NotNull]
        public TestContext? TestContext { get; set; }

        [NotNull]
        private IWebDriver? Driver { get; set; }

        private static string AppUrl { get; } = "http://localhost:5000/";
        private static string ApiUrl { get; } = "http://locahost:5002/";

        private static Process Api { get; set; }
        private static Process Web { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            const string browser = "Firefox";
            Driver = browser switch
            {
                "Firefox" => new FirefoxDriver(),
                _         => new InternetExplorerDriver()
            };

            Driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 10);
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            Api = Process.Start("dotnet",
                                $@"run -p ..\..\..\..\..\src\SecretSanta.Api\SecretSanta.Api.csproj --urls={ApiUrl}");
            Web = Process.Start("dotnet",
                                $@"run -p ..\..\..\..\..\src\SecretSanta.Web\SecretSanta.Web.csproj --urls={AppUrl}");
        }

        [TestMethod]
        public async Task CreateGift_Success()
        {
            var user = await AddUser();

            const string title = "Hall of Fame Bust";
            const string desc  = "The most prestigious NFL honor.";
            const string url   = "https://www.nfl.com";

            Driver.Navigate().GoToUrl(new Uri(AppUrl + "Gifts/"));
            Driver.Manage().Window.Maximize();
            Driver.FindElement(By.CssSelector("body > section > div > div > button")).Click();
            var textInputs = Driver.FindElements(By.ClassName("input"));
            textInputs[0].SendKeys(title);
            textInputs[1].SendKeys(desc);
            textInputs[2].SendKeys(url);
            new SelectElement(Driver.FindElement(By.Id("selectUser"))).SelectByIndex(0);
            Driver.FindElement(By.Id("submit")).Click();

            Assert.AreEqual(1, Driver.FindElements(By.TagName("tr")).Count);
        }

        private static async Task<User> AddUser()
        {
            using HttpClient httpClient = new HttpClient {BaseAddress = new Uri(ApiUrl)};
            var client = new UserClient(httpClient);
            var user = new UserInput
            {
                FirstName = "John",
                LastName  = "Madden"
            };

            return await client.PostAsync(user);
        }
        
        [TestCleanup]
        public void TestCleanup()
        {
            Driver.Quit();
        }

        [ClassCleanup]
        public static void CleanupProcesses()
        {
            Web.Kill();
            Web.Close();
            Api.Kill();
            Api.Close();
        }

    }

}
