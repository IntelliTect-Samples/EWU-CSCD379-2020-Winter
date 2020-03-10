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
		public string MyUrl { get; set; } = "http://www.duckduckgo.com";

        //        [ClassInitialize]
        //        public static void ClassInitialize(TestContext testContext)
        //        {

        //            using WebClient webClient = new WebClient();

        //            ApiHostProcess = StartWebHost("SecretSanta.Api", 44388, "Swagger", new string[] { "ConnectionStrings:DefaultConnection='Data Source=SecretSanta.db'" });

        //            WebHostProcess = StartWebHost("SecretSanta.Web", 44394, "", " ApiUrl=https://localhost:44394");

        //            Process StartWebHost(string projectName, int port, string urlSubDirectory, params string[] args)
        //            {

        //                string fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, projectName + ".exe");
        //                Process[] alreadyExecutingProcesses = Process.GetProcessesByName(projectName);
        //                if (alreadyExecutingProcesses.Length != 0)
        //                {
        //                    foreach (Process item in alreadyExecutingProcesses)
        //                    {
        //                        item.Kill();
        //                    }
        //                }

        //                string testAssemblyLocation = Assembly.GetExecutingAssembly().Location;
        //                string testAssemblyName = Path.GetFileNameWithoutExtension(testAssemblyLocation);
        //                string projectExe = testAssemblyLocation.RegexReplace(testAssemblyName, projectName).RegexReplace(@"\\test\\", @"\src\").RegexReplace("dll$", "exe");

        //                string argumentList = $"{string.Join(" ", args)} Urls=https://localhost:{port}";

        //                ProcessStartInfo startInfo = new ProcessStartInfo(projectExe, argumentList)
        //                {
        //                    RedirectStandardError = true,
        //                    RedirectStandardOutput = true,
        //                    UseShellExecute = false,
        //                    CreateNoWindow = true
        //                };

        //                string stdErr = "";
        //                string stdOut = "";
        //                // Justification: Dispose invoked by caller on Process object returned.
        //#pragma warning disable CA2000 // Dispose objects before losing scope
        //                Process host = new Process
        //                {
        //                    EnableRaisingEvents = true,
        //                    StartInfo = startInfo
        //                };
        //#pragma warning restore CA2000 // Dispose objects before losing scope

        //                host.ErrorDataReceived += (sender, args) =>
        //                    stdErr += $"{args.Data}\n";
        //                host.OutputDataReceived += (sender, args) =>
        //                    stdOut += $"{args.Data}\n";
        //                host.Start();
        //                host.BeginErrorReadLine();
        //                host.BeginOutputReadLine();

        //                for (int seconds = 20; seconds > 0; seconds--)
        //                {
        //                    if (stdOut.Contains("Application started."))
        //                    {
        //                        _ = webClient.DownloadString(
        //                            $"https://localhost:{port}/{urlSubDirectory.TrimStart(new char[] { '/', '\\' })}");
        //                        return host;
        //                    }
        //                    else if (host.WaitForExit(1000))
        //                    {
        //                        break;
        //                    }
        //                }

        //                if (!host.HasExited) host.Kill();
        //                host.WaitForExit();
        //                throw new InvalidOperationException($"Unable to execute process successfully: {stdErr}") { Data = { { "StandardOut", stdOut } } };

        //            }
        //        }

        //        [ClassCleanup]
        //        public static void ClassCleanup()
        //        {
        //            ApiHostProcess?.CloseMainWindow();
        //            ApiHostProcess?.Close();
        //            WebHostProcess?.CloseMainWindow();
        //            WebHostProcess?.Close();
        //        }

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

		//[TestMethod]
		//public void MyTestMethod()
		//{
		//	WebDriver.Navigate().GoToUrl(MyUrl);
		//	var input = WebDriver.FindElement(By.Id("search_form_input_homepage"));
		//	input.SendKeys("selenium");
		//	var button = WebDriver.FindElement(By.Id("search_button_homepage"));
		//	button.Click();

		//}

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
