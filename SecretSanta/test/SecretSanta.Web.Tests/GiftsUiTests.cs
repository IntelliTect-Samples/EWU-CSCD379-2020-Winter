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
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Web.Tests
{
    [TestClass]
    public class GiftsUiTests
    {
        [NotNull]
        public TestContext? TestContext { get; set; }
        
        [NotNull]
        private IWebDriver? Driver { get; set; }

        private static Process? ApiHostProcess { get; set; }
        private static Process? WebHostProcess { get; set; }

        [ClassInitialize]
        public static async Task ClassInitialize(TestContext testContext)
        {
            if(testContext is null)
            {
                throw new ArgumentNullException(nameof(testContext));
            }

            ApiHostProcess = Process.Start("dotnet.exe", "run -p ..\\..\\..\\..\\..\\src\\SecretSanta.Api\\SecretSanta.Api.csproj");
            WebHostProcess = Process.Start("dotnet.exe", "run -p ..\\..\\..\\..\\..\\src\\SecretSanta.Web\\SecretSanta.Web.csproj");
            ApiHostProcess.WaitForExit(8000);

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44388");

            IUserClient userClient = new UserClient(httpClient);
            ICollection<User> users = await userClient.GetAllAsync();

            if(users.Count == 0)
            {
                UserInput user = new User
                {
                    FirstName = "Inigo",
                    LastName = "Montoya"
                };

                userClient.PostAsync(user);
            }
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            if(!(ApiHostProcess is null))
            {
                ApiHostProcess.Kill();
                ApiHostProcess.CloseMainWindow();
                ApiHostProcess.Close();
            }

            if (!(WebHostProcess is null))
            {
                WebHostProcess.Kill();
                WebHostProcess.CloseMainWindow();
                WebHostProcess.Close();
            }
        }

        [TestInitialize]
        public void TestInitialize()
        {
            Driver = new ChromeDriver();
            Driver.Manage().Timeouts().ImplicitWait = new System.TimeSpan(0, 0, 10);
        }

        [TestMethod]
        public void LaunchSite_Success()
        {
            Driver.Navigate().GoToUrl(new Uri("https://localhost:44394"));
            string text = Driver.FindElement(By.XPath("/html/body/section/div/p")).Text;

            Assert.IsTrue(text.Contains("Welcome to your secret santa app"));
        }

        [TestMethod]
        public void CreateGift_Success()
        {
            // Arrange
            Driver.Navigate().GoToUrl(new Uri("https://localhost:44394/gifts"));
            Driver.FindElement(By.CssSelector("button[class='button is-secondary']")).Click();
            Driver.Manage().Timeouts().ImplicitWait = new System.TimeSpan(0, 0, 10);

            string title = "Test Title";
            string desc = "Test Description";
            string url = "http://www.google.com";
            int userIndex = 0;

            // Act
            EnterGiftDetails(title, desc, url, userIndex);

            Driver.FindElement(By.Id("submit")).Click();

            List<string> gifts = Driver.FindElement(By.Id("gift-table"))
                .FindElements(By.TagName("td"))
                .Select(element => element.Text)
                .ToList();
            int indexOfTitle = gifts.IndexOf(title);

            // Assert
            Assert.AreEqual(title, gifts[indexOfTitle]);
            Assert.AreEqual(desc, gifts[indexOfTitle + 1]);
            Assert.AreEqual(url, gifts[indexOfTitle + 2]);

            // Screenshot
            string path = $"{Directory.GetCurrentDirectory()}CreateGift_Success.png";
            ((ITakesScreenshot)Driver).GetScreenshot().SaveAsFile(path, ScreenshotImageFormat.Png);
            TestContext.AddResultFile(path);
        }

        private void EnterGiftDetails(string title, string desc, string url, int userIndex)
        {
            IWebElement titleField = Driver.FindElement(By.Id("gift-field-title"));
            IWebElement descField = Driver.FindElement(By.Id("gift-field-description"));
            IWebElement urlField = Driver.FindElement(By.Id("gift-field-url"));
            SelectElement userField = new SelectElement(Driver.FindElement(By.Id("gift-field-user")));

            titleField.SendKeys(title);
            descField.SendKeys(desc);
            urlField.SendKeys(url);
            userField.SelectByIndex(userIndex);

            Assert.AreEqual(title, titleField.GetProperty("value"));
            Assert.AreEqual(desc, descField.GetProperty("value"));
            Assert.AreEqual(url, urlField.GetProperty("value"));
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Driver.Quit();
        }
    }
}
