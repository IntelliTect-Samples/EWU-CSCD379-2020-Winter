using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SecretSanta.Web.Api;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Web.Tests
{
    [TestClass]
    public class GiftTests
    {
        [NotNull]
        public TestContext? TextContext { get; set; }

        [NotNull]
        private IWebDriver? Driver { get; set; }

        private static Process? ApiHostProcess { get; set; }
        private static Process? WebHostProcess { get; set; }

        static string AppUrl { get; } = "https://localhost:44388/";
        static string WebUrl { get; } = "https://localhost:44394/";

        [ClassInitialize]
        public static async System.Threading.Tasks.Task ClassInitializeAsync(TestContext testContext)
        {
            if (testContext is null)
            {
                throw new ArgumentNullException(nameof(testContext));
            }
            ApiHostProcess = Process.Start("dotnet.exe", "run -p ..\\..\\..\\..\\..\\src\\SecretSanta.Api\\SecretSanta.Api.csproj");
            WebHostProcess = Process.Start("dotnet.exe", "run -p ..\\..\\..\\..\\..\\src\\SecretSanta.Web\\SecretSanta.Web.csproj");
            ApiHostProcess.WaitForExit(8000);

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(WebUrl);
            UserClient userClient = new UserClient(httpClient);

            ICollection<User> users = await userClient.GetAllAsync();
            if (users.Count < 1)
            {
                UserInput userInput = new UserInput
                {
                    FirstName = "Inigo",
                    LastName = "Montoya"
                };
                await userClient.PostAsync(userInput);
            }
            httpClient.Dispose();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            if(ApiHostProcess != null)
            {
                ApiHostProcess.Kill();
                ApiHostProcess.CloseMainWindow();
                ApiHostProcess.Close();
            }
            if(WebHostProcess != null)
            {
                WebHostProcess.Kill();
                WebHostProcess.CloseMainWindow();
                WebHostProcess.Close();
            }
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void Create_Gift_Success()
        {
            Driver = new ChromeDriver();
            Driver.Navigate().GoToUrl(WebUrl + "/Gifts");
            Driver.FindElement(By.CssSelector("create-button")).Click();
            Driver.FindElement(By.Id("gift-title")).SendKeys("Test Title");
            Driver.FindElement(By.Id("gift-description")).SendKeys("Test Description");
            Driver.FindElement(By.Id("gift-url")).SendKeys("www.test.com");
            IWebElement dropDown = Driver.FindElement(By.Id("user-dropdown"));
            SelectElement selectDropDown = new SelectElement(dropDown);
            selectDropDown.SelectByText("Inigo Montoya");
            IWebElement element = Driver.FindElement(By.Id("submit"));

            string[] testGift = Driver.FindElement(By.Id("gift-table"))
                                    .FindElements(By.TagName("td"))
                                    .Select(e => e.Text)
                                    .ToArray();

            Assert.AreEqual("Test Title", testGift[0]);
            Assert.AreEqual("Test Description", testGift[1]);
            Assert.AreEqual("www.test.com", testGift[2]);

            string path = $"{Directory.GetCurrentDirectory()}CreateGift.Png";
            Screenshot screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
            screenshot.SaveAsFile(path, ScreenshotImageFormat.Png);
        }
    }
}
