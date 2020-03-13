using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using SecretSanta.Web.Api;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Text.RegularExpressions;

namespace SecretSanta.Web.Tests
{
    [TestClass]
    public class GiftsUITest
    {
        [NotNull]
        public TestContext? TestContext { get; set; }

        [NotNull]
        private IWebDriver? Driver { get; set; }

        private static Process? ApiHostProcess { get; set; }
        private static Process? WebHostProcess { get; set; }

        static int ApiPort { get; } = 44388;
        static string ApiUrl { get; } = $"https://localhost:{ApiPort}/";
        static int AppPort { get; } = 44394;
        static string AppURL { get; } = $"https://localhost:{AppPort}/";

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            /*ApiHostProcess = Process.Start("dotnet.exe", @"run -p ..\..\..\..\..\src\SecretSanta.Api\SecretSanta.Api.csproj");
            WebHostProcess = Process.Start("dotnet.exe", @"run -p ..\..\..\..\..\src\SecretSanta.App\SecretSanta.App.csproj");*/
            using WebClient webClient = new WebClient();

            ApiHostProcess = StartWebHost("SecretSanta.Api", ApiPort, "Swagger", new string[] { "ConnectionStrings:DefaultConnection='Data Source=SecretSanta.db'" });
            WebHostProcess = StartWebHost("SecretSanta.Web", AppPort, "", $" ApiUrl={ApiUrl}");

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

            CreateUser();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            ApiHostProcess?.Kill();
            WebHostProcess?.Kill();
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
                    ChromeOptions options = new ChromeOptions();
                    options.AddArgument("--headless");
                    options.AddArgument("--window-size=1920,1080");
                    options.AddArgument("--start-maximized");
                    Driver = new ChromeDriver(options);
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
            Driver.FindElement(By.Id("submit")).Click();
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

    public static class StringRegExExtension
    {
        static public string RegexReplace(this string input, string findPattern, string replacePattern)
        {
            return Regex.Replace(input, findPattern, replacePattern);
        }
    }
}
