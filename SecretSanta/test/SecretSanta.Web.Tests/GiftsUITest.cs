using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using SecretSanta.Web.Api;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading;

namespace SecretSanta.Web.Tests
{
    [TestClass]
    public class GiftsUITest
    {
        [NotNull]
        public TestContext? TestContext { get; set; }

        [NotNull]
        private IWebDriver? Driver { get; set; }

        static string ApiUrl { get; } = "https://localhost:44388/";
        static string AppURL { get; } = "https://localhost:44394/";

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            CreateUser();
        }

        private static async void CreateUser()
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(ApiUrl);
            IUserClient client = new UserClient(http);
            ICollection<User> users = await client.GetAllAsync();
            if (users.Count == 0)
            {
                UserInput user = new UserInput
                {
                    FirstName = "Inigo",
                    LastName = "Montoya"
                };
                await client.PostAsync(user);
            }
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
            Driver.Manage().Timeouts().ImplicitWait = new System.TimeSpan(0, 0, 10);
        }

        [TestMethod]
        public void VerifySiteIsUp()
        {
            Driver.Navigate().GoToUrl(new Uri(AppURL));
            string text = Driver.FindElement(By.XPath("/html/body/section/div/p")).Text;
            Assert.IsTrue(text.Contains("Welcome to your secret santa app"));
        }

        [TestMethod]
        public void CreateNewGift_Success()
        {
            string GiftTitle = "test gift";
            Driver.Navigate().GoToUrl(new Uri(AppURL));
            Driver.FindElement(By.XPath("/html/body/nav/div[2]/div/a[4]")).Click();
            Thread.Sleep(500);
            Driver.FindElement(By.CssSelector("button[class='button is-secondary']")).Click();
            IWebElement title = Driver.FindElement(By.CssSelector("body > section > div > div > div > div.modal-content > div:nth-child(1) > div > input"));
            title.SendKeys(GiftTitle);
            IWebElement description = Driver.FindElement(By.CssSelector("body > section > div > div > div > div.modal-content > div:nth-child(2) > div > input"));
            description.SendKeys("it's been tested!");
            IWebElement url = Driver.FindElement(By.CssSelector("body > section > div > div > div > div.modal-content > div:nth-child(3) > div > input"));
            url.SendKeys("http://www.test.com");
            SelectElement user = new SelectElement(Driver.FindElement(By.CssSelector("body > section > div > div > div > div.modal-content > div:nth-child(4) > div > select")));
            user.SelectByIndex(0);
            Driver.FindElement(By.CssSelector("#submit")).Click();
            //IWebElement table = Driver.FindElement(By.CssSelector("body > section > div > div > table > tbody > tr"));

            Thread.Sleep(500);
            var table = Driver.FindElement(By.TagName("table"));
            var rows = table.FindElements(By.TagName("tr"));
            Boolean foundTest = false;
            foreach (var row in rows)
            {
                var rowTds = row.FindElements(By.TagName("td"));
                foreach (var td in rowTds)
                {
                    if (td.Text.Equals(GiftTitle))
                    {
                        foundTest = true;
                        break; // because break is awesome when used right
                    }
                }
            }

            Assert.IsTrue(foundTest);

            Driver.TakeScreenshot().SaveAsFile("proof.png", ScreenshotImageFormat.Png);

            Thread.Sleep(2000);
        }

        [TestCleanup()]
        public void TestCleanup()
        {
            Driver.Quit();
        }
    }
}
