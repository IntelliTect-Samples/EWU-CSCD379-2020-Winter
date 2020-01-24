using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace SecretSanta.Data.Tests
{
    /// <summary>
    /// Extra Credit: See below GetLoggerFactory method
    /// </summary>
    [TestClass]
    public class TestBase
    {
#nullable disable
        private SqliteConnection SqliteConnection { get; set; }
        protected DbContextOptions<ApplicationDbContext> Options { get; private set; }
#nullable enable

        public IHttpContextAccessor HttpContextAccessor { get => _HttpContextAccessor; set => _HttpContextAccessor = value ?? throw new ArgumentNullException(nameof(HttpContextAccessor)); }
        private IHttpContextAccessor _HttpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "dsergio"));


        private static ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
            {
                builder.AddConsole()
                    .AddFilter(DbLoggerCategory.Database.Command.Name, 
                    LogLevel.Trace);
            });
            return serviceCollection.BuildServiceProvider().
                GetService<ILoggerFactory>();
        }

        [TestInitialize]
        public void OpenConnection()
        {
            SqliteConnection = new SqliteConnection("DataSource=:memory:");
            //SqliteConnection = new SqliteConnection("DataSource=testdatabase.db");
            SqliteConnection.Open();

            Options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(SqliteConnection)
                .UseLoggerFactory(GetLoggerFactory())
                .EnableSensitiveDataLogging()
                .Options;

            using (ApplicationDbContext context = new ApplicationDbContext(Options))
            {
                context.Database.EnsureCreated();
            }

             
        }

        [TestCleanup]
        public void CloseConnection()
        {
            SqliteConnection.Close();
        }
    }
}
