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
        public async Task User_Create_DbAllPropertiesGetSet()
        {
            // Arrange
            int userId = -1;

            var user = new User
            {
                Id = 1,
                FirstName = "Inigo",
                LastName = "Montoya",
                CreatedBy = "imontoya",
                Santa = null,
                Gifts = new List<Gift>(),
                UserGroups = new List<UserGroup>()
            };

            // Act
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options))
            {
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();
                userId = user.Id;
            }
            // Assert
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options))
            {
                User testUser = await dbContext.Users.Where(u => u.Id == userId).SingleOrDefaultAsync();
                Assert.IsNotNull(user);
                Assert.AreEqual("Inigo", testUser.FirstName);
                Assert.AreEqual("Montoya", testUser.LastName);
            }
        }

        [TestMethod]
        public async Task User_ShouldSaveFingerPrintData()
        {
            // Arrange
            int userId = -1;
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta => hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "imont"));


            var user = new User
            {
                Id = 1,
                FirstName = "Inigo",
                LastName = "Montoya",
                CreatedBy = "imont",
                Santa = null,
                Gifts = new List<Gift>(),
                UserGroups = new List<UserGroup>()
            };

            //Act
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();
                userId = user.Id;
            }

            // Assert
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                User testUser = await dbContext.Users.Where(u => u.Id == userId).SingleOrDefaultAsync();
                Assert.IsNotNull(testUser);
                Assert.AreEqual("imont", testUser.CreatedBy);
                Assert.AreEqual("imont", testUser.ModifiedBy);
            }
        }
    }
}
