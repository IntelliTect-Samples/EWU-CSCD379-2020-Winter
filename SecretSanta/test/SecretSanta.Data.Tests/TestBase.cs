using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data.Tests
{
    public class TestBase
    {
        public static User _Spongebob = new User
        {
            FirstName = "Spongebob",
            LastName = "SquarePants",
            Id = 1,
            Santa = _Patrick,
        };

        public static User _Patrick = new User
        {
            FirstName = "Patrick",
            LastName = "Star",
            Id = 2,
            Santa = _Spongebob,
        };

        public static Group _JellyFishGroup = new Group
        {
            CreatedBy = "Spongebob",
            Id = 1,
            Name = "JellyFishHunters",
            ModifiedBy = "Patrick",
        };

        public static Gift _Gift = new Gift()
        {
            Id = 9879679,
            Title = "Spatula"
        };

#nullable disable
        private SqliteConnection SqliteConnection { get; set; }
        protected DbContextOptions<ApplicationDbContext> Options { get; private set; }
#nullable enable

        private static ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
            {
                builder.AddConsole()
                    .AddFilter(DbLoggerCategory.Database.Command.Name,
                        LogLevel.Information);
            });
            return serviceCollection.BuildServiceProvider().
                GetService<ILoggerFactory>();
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

            using (var context = new ApplicationDbContext(Options))
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
