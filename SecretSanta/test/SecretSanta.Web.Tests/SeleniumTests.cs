using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using SecretSanta.Web.Api;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
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
        public static void ClassInitialize(TestContext testContext)
        {
            if (testContext is null)
                throw new ArgumentNullException(nameof(testContext));

            ApiHostProcess = Process.Start("dotnet.exe", $@"run -p ..\..\src\SecretSanta.Api\SecretSanta.Api.csproj --urls={ApiURL}");
            WebHostProcess = Process.Start("dotnet.exe", $@"run -p ..\..\src\SecretSanta.Web\SecretSanta.Web.csproj --urls={AppURL}");

            ApiHostProcess.WaitForExit(8000);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            ApiHostProcess?.CloseMainWindow();
            ApiHostProcess?.Close();
            WebHostProcess?.CloseMainWindow();
            WebHostProcess?.Close();
        }

        //public async void CreateUser()
        //{
        //    HttpClient httpClient = new HttpClient();
        //    httpClient.BaseAddress = new Uri(AppURL);
        //    UserClient userClient = new UserClient(httpClient);

        //    ICollection<User> users = await userClient.GetAllAsync();
        //    if (users.Count < 1)
        //    {
        //        UserInput userInput = new UserInput
        //        {
        //            FirstName = "Inigo",
        //            LastName = "Montoya"
        //        };
        //        await userClient.PostAsync(userInput);
        //    }
        //    httpClient.Dispose();
        //}

        [TestMethod]
        [TestCategory("Chrome")]
        public void NavigateToHome_Success()
        {
            Driver.Navigate().GoToUrl(new Uri(AppURL));
            Thread.Sleep(5000);
            Driver.Navigate().GoToUrl(AppURL + "Gifts");
            Thread.Sleep(5000);
            Driver.Navigate().GoToUrl(AppURL + "Users");
            Thread.Sleep(5000);
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
