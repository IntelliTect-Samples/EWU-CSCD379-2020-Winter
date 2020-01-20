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
    
#nullable disable
        private SqliteConnection SqliteConnection { get; set; }
        protected DbContextOptions<ApplicationDbContext> Options { get; private set; }
#nullable enable

        [TestInitialize]
        public void OpenConnection()
        {
            SqliteConnection = new SqliteConnection("DataSource=:memory:");
            SqliteConnection.Open();

            Options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(SqliteConnection)
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
