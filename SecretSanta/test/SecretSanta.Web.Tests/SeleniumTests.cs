using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using SecretSanta.Web.Api;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        public static async Task ClassInitialize(TestContext testContext)
        {
            if (testContext is null)
                throw new ArgumentNullException(nameof(testContext));

            ApiHostProcess = Process.Start("dotnet.exe", $@"run -p ..\..\src\SecretSanta.Api\SecretSanta.Api.csproj --urls={ApiURL}");
            WebHostProcess = Process.Start("dotnet.exe", $@"run -p ..\..\src\SecretSanta.Web\SecretSanta.Web.csproj --urls={AppURL}");

            ApiHostProcess.WaitForExit(8000);

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ApiURL);

            UserClient userClient = new UserClient(httpClient);

            ICollection<User> users = await userClient.GetAllAsync();
            if (users.Count <= 0)
            {
                UserInput userInput = new UserInput
                {
                    FirstName = "Caleb",
                    LastName = "Walsh"
                };

                await userClient.PostAsync(userInput);
            }
            httpClient.Dispose();
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
        public async Task NavigateToHome_Success()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ApiURL);
            GiftClient giftClient = new GiftClient(httpClient);
            ICollection<Gift> giftsBeforeInsert = await giftClient.GetAllAsync();
            

            Driver.Navigate().GoToUrl(new Uri(AppURL));
            Thread.Sleep(3000);
            Driver.Navigate().GoToUrl(new Uri(AppURL + "Gifts"));
            Thread.Sleep(5000);
            IWebElement createGift = Driver.FindElement(By.CssSelector(".is-secondary"));
            createGift.Click();
            Thread.Sleep(3000);

            IWebElement titleInput = Driver.FindElement(By.CssSelector("input[id='titleInput']"));
            titleInput.SendKeys("Test Title");

            IWebElement descriptionInput = Driver.FindElement(By.CssSelector("input[id='descriptionInput']"));
            descriptionInput.SendKeys("Test Description");

            IWebElement urlInput = Driver.FindElement(By.CssSelector("input[id='urlInput']"));
            urlInput.SendKeys("www.url.com");

            IWebElement userInput = Driver.FindElement(By.CssSelector("select[id='userInput']"));
            userInput.Click();

            IWebElement userSelect = Driver.FindElement(By.CssSelector("option"));
            userSelect.Click();

            IWebElement submitButton = Driver.FindElement(By.CssSelector("button[id='submit']"));
            submitButton.Click();
          
            Screenshot screenShot = ((ITakesScreenshot)Driver).GetScreenshot();
            screenShot.SaveAsFile("../../../screenshotGiftAdded.png", ScreenshotImageFormat.Png);


            ICollection<Gift> giftsAfterInsert = await giftClient.GetAllAsync();
            Assert.IsTrue(giftsBeforeInsert.Count < giftsAfterInsert.Count);

            httpClient.Dispose();
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
