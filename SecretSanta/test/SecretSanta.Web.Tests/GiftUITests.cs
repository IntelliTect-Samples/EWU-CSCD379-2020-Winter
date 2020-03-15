﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SecretSanta.Web.Tests
{
    [TestClass]
    public class GiftUITests
    {
        [NotNull]
        public TestContext? TestContext { get; set; }

        [NotNull]
        private IWebDriver? Driver { get; set; }

        private static Process? ApiHostProcess { get; set; }

        private static Process? WebHostProcess { get; set; }

        string AppURL { get; } = "https://localhost:44394/";

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            using WebClient webClient = new WebClient();
            ApiHostProcess = StartWebHost("SecretSanta.Api", 5000, "Swagger", new string[] { "ConnectionStrings:DefaultConnection='Data Source=Blog.db'" });

            WebHostProcess = StartWebHost("SecretSanta.Web", 5001, "", " ApiUrl=https://localhost:5001");

            Process StartWebHost(string projectName, int port, string urlSubDirectory, params string[] args)
            {

                string fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, projectName + ".exe");
                Process[] alreadyExecutingProcesses = Process.GetProcessesByName(projectName);
                if (alreadyExecutingProcesses.Length != 0)
                {
                    foreach (Process item in alreadyExecutingProcesses)
                    {
                        item.Kill();
                    }
                }

                string testAssemblyLocation = Assembly.GetExecutingAssembly().Location;
                string testAssemblyName = Path.GetFileNameWithoutExtension(testAssemblyLocation);
                string projectExe = testAssemblyLocation.RegexReplace(testAssemblyName, projectName)
                    .RegexReplace(@"\\test\\", @"\src\").RegexReplace("dll$", "exe");

                string argumentList = $"{string.Join(" ", args)} Urls=https://localhost:{port}";

                ProcessStartInfo startInfo = new ProcessStartInfo(projectExe, argumentList)
                {
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                string stdErr = "";
                string stdOut = "";
                // Justification: Dispose invoked by caller on Process object returned.
#pragma warning disable CA2000 // Dispose objects before losing scope
                Process host = new Process
                {
                    EnableRaisingEvents = true,
                    StartInfo = startInfo
                };
#pragma warning restore CA2000 // Dispose objects before losing scope

                host.ErrorDataReceived += (sender, args) =>
                    stdErr += $"{args.Data}\n";
                host.OutputDataReceived += (sender, args) =>
                    stdOut += $"{args.Data}\n";
                host.Start();
                host.BeginErrorReadLine();
                host.BeginOutputReadLine();

                for (int seconds = 20; seconds > 0; seconds--)
                {
                    if (stdOut.Contains("Application started."))
                    {
                        _ = webClient.DownloadString(
                            $"https://localhost:{port}/{urlSubDirectory.TrimStart(new char[] { '/', '\\' })}");
                        return host;
                    }
                    else if (host.WaitForExit(1000))
                    {
                        break;
                    }
                }

                if (!host.HasExited) host.Kill();
                host.WaitForExit();
                throw new InvalidOperationException($"Unable to execute process successfully: {stdErr}") { Data = { { "StandardOut", stdOut } } };

            }
        }

        [ClassCleanup]
        public static void ClassCleanup()
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
            Driver.Manage().Timeouts().ImplicitWait = new System.TimeSpan(0, 0, 10);
        }

        [TestMethod]
        public void VerifySiteIsUp()
        {
            Driver.Navigate().GoToUrl(new Uri("https://localhost:5001/"));
            string text = Driver.FindElement(By.XPath("/html/body/section/div/p")).Text;
            Assert.IsTrue(text.Contains("Welcome to your secret santa app"));
        }

        [TestMethod]

        public void CreateGift() {
            string sentInTitle = "Test title";
            string sentInDescription = "Test description";
            string sentInUrl = "Test url";
            
            
            //nagivate to gift
            Driver.Navigate().GoToUrl(new Uri("https://localhost:44394/Gifts"));

            //find create gift button
            Driver.FindElement(By.CssSelector("#createGift")).Click();

            IWebElement element = Driver.FindElement(By.Id("gift-title-input"));
#pragma warning disable CA1303 // Do not pass literals as localized parameters
            element.SendKeys(sentInTitle);

            Assert.AreEqual<string>(sentInTitle, element.GetProperty("value"));

            IWebElement element1 = Driver.FindElement(By.Id("gift-description-input"));
            element1.SendKeys(sentInDescription);
            Assert.AreEqual<string>(sentInDescription, element1.GetProperty("value"));

            IWebElement element2 = Driver.FindElement(By.Id("gift-url-input"));
            element1.SendKeys(sentInUrl);
            Assert.AreEqual<string>(sentInUrl, element2.GetProperty("value"));
#pragma warning restore CA1303 // Do not pass literals as localized parameters justification(testing proposses)

            Driver.FindElement(By.Id("submit")).Click();

            Screenshot saveAzure = ((ITakesScreenshot)Driver).GetScreenshot();

            string path = $"{Directory.GetCurrentDirectory()}Test.png";
            saveAzure.SaveAsFile(path, ScreenshotImageFormat.Png);

            TestContext.AddResultFile(path);
        }

        [TestCleanup()]
        public void TestCleanup()
        {
            Driver.Quit();
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