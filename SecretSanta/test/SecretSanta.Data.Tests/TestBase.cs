using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SecretSanta.Data.Tests
{

    public class TestBase
    {

#nullable disable
        private SqliteConnection SqliteConnection { get; set; }

        protected DbContextOptions<ApplicationDbContext> Options { get; private set; }
#nullable enable

        protected IHttpContextAccessor Accessor
        {
            get => _Accessor;
            private set => _Accessor = value ?? throw new ArgumentNullException(nameof(Accessor));
        }

        private IHttpContextAccessor _Accessor = Mock.Of<IHttpContextAccessor>(
            accessor => accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)
                     == new Claim(ClaimTypes.NameIdentifier, "gamerbah"));

        private static ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
            {
                builder.AddConsole()
                       .AddFilter(DbLoggerCategory.Database.Command.Name,
                                  LogLevel.Information);
            });
            return serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();
        }

        [TestInitialize]
        public void OpenConnection()
        {
            SqliteConnection = new SqliteConnection("DataSource=:memory:");
            SqliteConnection.Open();

            Options = new DbContextOptionsBuilder<ApplicationDbContext>()
                     .UseSqlite(SqliteConnection)
                     .UseLoggerFactory(GetLoggerFactory())
                     .EnableSensitiveDataLogging()
                     .Options;

            using var context = new ApplicationDbContext(Options);
            context.Database.EnsureCreated();
        }

        [TestCleanup]
        public void CloseConnection()
        {
            SqliteConnection.Close();
        }

    }

}
