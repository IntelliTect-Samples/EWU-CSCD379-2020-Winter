using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class UserTests : TestBase
    {
        [TestMethod]
        public async Task CreateUser_ShouldSaveIntoDatabase()
        {
            int userId = -1;
            // Arrange
            using (var applicationDbContext = new ApplicationDbContext(Options))
            {
                var user = new User
                {
                    FirstName = "Inigo",
                    LastName = "Montoya",
                    
                };
                applicationDbContext.Users.Add(user);

                var user2 = new User
                {
                    FirstName = "Inigo",
                    LastName = "Montoya",
         
                };
                applicationDbContext.Users.Add(user2);

                await applicationDbContext.SaveChangesAsync();

                userId = user.Id;
            }

            // Act
            // Assert
            using (var applicationDbContext = new ApplicationDbContext(Options))
            {
                var user = await applicationDbContext.Users.Where(a => a.Id == userId).SingleOrDefaultAsync();

                Assert.IsNotNull(user);
                Assert.AreEqual("Inigo", user.FirstName);
            }
        }

        [TestMethod]
        public async Task CreateUser_ShouldSetFingerPrintDataOnInitialSave()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "imontoya"));

            int userId = -1;
            // Arrange
            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var user = new User
                {
                    FirstName = "Inigo",
                    LastName = "Montoya",
                    
                };
                applicationDbContext.Users.Add(user);

                var user2 = new User
                {
                    FirstName = "Inigo",
                    LastName = "Montoya",
                   
                };
                applicationDbContext.Users.Add(user2);

                await applicationDbContext.SaveChangesAsync();

                userId = user.Id;
            }

            // Act
            // Assert
            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var user = await applicationDbContext.Users.Where(a => a.Id == userId).SingleOrDefaultAsync();

                Assert.IsNotNull(user);
                Assert.AreEqual("Inigo", user.FirstName);
                Assert.AreEqual("Montoya", user.LastName);
            }
        }

        [TestMethod]
        public async Task CreateUser_ShouldSetFingerPrintDataOnUpdate()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "imontoya"));

            int userId = -1;
            // Arrange
            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var user = new User
                {
                    FirstName = "Inigo",
                    LastName = "Montoya",
                    
                };
                applicationDbContext.Users.Add(user);

                var user2 = new User
                {
                    FirstName = "Inigo",
                    LastName = "Montoya",
                    
                };
                applicationDbContext.Users.Add(user2);

                await applicationDbContext.SaveChangesAsync();

                userId = user.Id;
            }

            // Act
            // change the user that is updating the record
            httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "pbuttercup"));
            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                // Since we are pulling back the record from the database and making changes to it, we don't need to re-add it to the collection
                // thus no Users.Add call, that is only needed when new records are inserted
                var user = await applicationDbContext.Users.Where(a => a.Id == userId).SingleOrDefaultAsync();
                user.FirstName = "Princess";
                user.LastName = "Buttercup";

                await applicationDbContext.SaveChangesAsync();
            }
            // Assert
            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var user = await applicationDbContext.Users.Where(a => a.Id == userId).SingleOrDefaultAsync();

                Assert.IsNotNull(user);
                Assert.AreEqual("Princess", user.FirstName);
                Assert.AreEqual("Buttercup", user.LastName);
            }
        }
    }
}
