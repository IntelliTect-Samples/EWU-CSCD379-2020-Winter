using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

using OpenQA.Selenium.Remote;
using SecretSanta.Web.Api;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SecretSanta.Web.Tests
{
	[TestClass]
	public class GiftUITests
	{
		[NotNull]
		public TestContext? TestContext { get; set; }
		[NotNull]
		public IWebDriver? WebDriver { get; set; }
		private static Process? ApiHostProcess { get; set; }
		private static Process? WebHostProcess { get; set; }

        [ClassInitialize]
        public static void ClassInitalize(TestContext testContext)
        {
            if (testContext is null)
                throw new ArgumentNullException(nameof(testContext));

            ApiHostProcess = Process.Start("dotnet.exe", "run -p ..\\..\\..\\..\\..\\src\\SecretSanta.Api\\SecretSanta.Api.csproj");
            WebHostProcess = Process.Start("dotnet.exe", "run -p ..\\..\\..\\..\\..\\src\\SecretSanta.Web\\SecretSanta.Web.csproj");
            Console.WriteLine("output: " + ApiHostProcess.ToString());
            ApiHostProcess.WaitForExit(8000);

        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            if (ApiHostProcess != null)
            {
                ApiHostProcess.Kill();
                ApiHostProcess.CloseMainWindow();
                ApiHostProcess.Close();
            }
            if (WebHostProcess != null)
            {
                WebHostProcess.Kill();
                WebHostProcess.CloseMainWindow();
                WebHostProcess.Close();
            }
        }


        [TestInitialize]
		public void TestInitialize()
		{
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("ignore-certificate-errors");
            
            WebDriver = new ChromeDriver(options);
            
			WebDriver.Manage().Timeouts().ImplicitWait = new System.TimeSpan(0, 0, 10);
		}
		[TestCleanup]
		public void TestCleanup()
		{
			WebDriver.Quit();
		}

        [TestMethod]
        public void VerifySiteIsUp()
        {
            WebDriver.Navigate().GoToUrl(new Uri("https://localhost:44394/"));
            Thread.Sleep(5000);
            string text = WebDriver.FindElement(By.XPath("/html/body/section/div/p")).Text;
            Console.WriteLine("text: " + text);
            Assert.IsTrue(text.Contains("Welcome to your secret santa app"));
        }

        [TestMethod]
        public void GoToGiftsPage_CreateGift()
        {
            WebDriver.Navigate().GoToUrl(new Uri("https://localhost:44394/Gifts"));
            Thread.Sleep(5000);
            var button = WebDriver.FindElement(By.CssSelector("#createGift"));
            button.Click();
            Thread.Sleep(1000);
            var inputTitle = WebDriver.FindElement(By.Id("giftTitleInput"));
            inputTitle.SendKeys("Cylon Detector");
            var inputDescription = WebDriver.FindElement(By.Id("giftDescriptionInput"));
            inputDescription.SendKeys("Version 1");
            var inputUrl = WebDriver.FindElement(By.Id("giftUrlInput"));
            inputUrl.SendKeys("www.findacylon.com");
            var inputUser = WebDriver.FindElements(By.CssSelector("#giftUserIdInput option"));
            inputUser[0].Click();
            var buttonSubmit = WebDriver.FindElement(By.Id("submit"));
            buttonSubmit.Click();
            Thread.Sleep(5000);

            Screenshot screenshot = ((ITakesScreenshot)WebDriver).GetScreenshot();
            screenshot.SaveAsFile("dsergio_Assignment9_CreateGift_Screenshot.png", ScreenshotImageFormat.Png);

        }
    }

    public static class StringRegExExtension
    {
        static public string RegexReplace(this string input, string findPattern, string replacePattern)
        {
            return Regex.Replace(input, findPattern, replacePattern);
        }
    }
}
