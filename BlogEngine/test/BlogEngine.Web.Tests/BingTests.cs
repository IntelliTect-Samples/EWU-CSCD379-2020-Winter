using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics.CodeAnalysis;

namespace BlogEngine.Web.Tests
{
    [TestClass]
    public class BingTests
    {
        [NotNull]
        public TestContext? TestContext { get; set; }

        [NotNull]
        private IWebDriver? Driver { get; set; }

        string AppURL { get; } = "http://www.bing.com/";

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

        public void EnterBingSearchText(string text)
        {
            Driver.Navigate().GoToUrl(new Uri(AppURL + "/"));
            IWebElement element = Driver.FindElement(By.Id("sb_form_q"));
            element.SendKeys(text);
            Assert.AreEqual<string>(text, element.GetProperty("value"));
        }

        [TestMethod]
        public void BingSearch_UsingXPath_Success()
        {
            string searchString = "Inigo Montoya";
            EnterBingSearchText(searchString);
            Driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div[2]/form/label")).Click();
            Assert.IsTrue(Driver.Title.Contains(searchString), "Verified title of the page");
        }

        [TestMethod]
        public void BingSearch_UsingCSSSelector_Success()
        {
            string searchString = "Inigo Montoya";
            EnterBingSearchText(searchString);
            Driver.FindElement(By.CssSelector("label[for='sb_form_go']")).Click();
            Assert.IsTrue(Driver.Title.Contains(searchString), "Verified title of the page");
        }

        [TestCleanup()]
        public void TestCleanup()
        {
            Driver.Quit();
        }
    }
}
