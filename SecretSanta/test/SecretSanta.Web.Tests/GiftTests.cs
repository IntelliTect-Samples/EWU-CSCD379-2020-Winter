using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SecretSanta.Web.Api;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace SecretSanta.Web.Tests
{
    [TestClass]
    public class GiftPageTests
    {
        [NotNull]
        public TestContext? TestContext { get; set; }

        [NotNull]
        private IWebDriver? Driver { get; set; }

        static string AppURL { get; } = "https://localhost:44394/";
        static string ApiURL { get; } = "https://localhost:44388";
        private static Process? ApiHostProcess { get; set; }
        private static Process? WebHostProcess { get; set; }


        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            if (testContext is null) throw new ArgumentNullException(nameof(testContext));

            ApiHostProcess = Process.Start("dotnet", $@"run -p ..\..\..\..\..\src\SecretSanta.Api\SecretSanta.Api.csproj --urls={ApiURL}");
            WebHostProcess = Process.Start("dotnet", $@"run -p ..\..\..\..\..\src\SecretSanta.Web\SecretSanta.Web.csproj --urls={AppURL}");

        }

        [ClassCleanup]
        public static void ClassCleanUp()
        {
            ApiHostProcess?.CloseMainWindow();
            ApiHostProcess?.Close();
            WebHostProcess?.CloseMainWindow();
            WebHostProcess?.Close();
        }

        [TestInitialize]
        public void TestInitialize()
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
            Driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 10);
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        }

        private async Task<User> AddUser()
        {
            using HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ApiURL);
            UserClient client = new UserClient(httpClient);
            UserInput user = new UserInput
            {
                FirstName = "Kyle",
                LastName = "Smith"
            };

            return await client.PostAsync(user);
        }


        [TestMethod]
        public async Task GiftPage_AddGift_Success()
        {
            User user = await AddUser();

            Driver.Navigate().GoToUrl(new Uri(AppURL + "Gifts"));
            Driver.Manage().Window.Maximize();

            ReadOnlyCollection<IWebElement> GetGiftList = Driver.FindElements(By.TagName("tr"));
            int GiftListCount = GetGiftList.Count;

            IWebElement CreateGiftBtn = Driver.FindElement(By.CssSelector("body > section > div > div > button"));
            CreateGiftBtn.Click();
            ReadOnlyCollection<IWebElement> Inputs = Driver.FindElements(By.ClassName("input"));
            Assert.IsTrue(Inputs.Count == 3);
            Inputs[0].SendKeys("A gift");
            Inputs[1].SendKeys("Look at this description");
            Inputs[2].SendKeys("www.Awebsite.com");
            SelectElement Select = new SelectElement(Driver.FindElement(By.TagName("select")));
            Assert.IsTrue(Select.Options.Count > 0);
            Select.SelectByValue(user.Id.ToString());
            IWebElement SubmitBtn = Driver.FindElement(By.Id("submit"));
            SubmitBtn.Click();
            Thread.Sleep(5000);

            ReadOnlyCollection<IWebElement> UpdatedGiftList = Driver.FindElements(By.TagName("tr"));
            int UpdatedGiftListCount = UpdatedGiftList.Count;

            Assert.IsTrue(GiftListCount < UpdatedGiftListCount);
            Screenshot image = ((ITakesScreenshot)Driver).GetScreenshot();
            image.SaveAsFile("../../../ScreenShot.png", ScreenshotImageFormat.Png);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Driver.Quit();
        }


    }
}