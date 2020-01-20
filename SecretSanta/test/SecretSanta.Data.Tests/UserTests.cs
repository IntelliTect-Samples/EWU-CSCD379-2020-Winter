using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;

namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class UserTests : TestBase
    {
        [TestMethod]
        public async Task User_CanBeCreate_AllPropertiesGetSet()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "imontoya"));

            int userId = -1;

            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {

                // Arrange
                var user1 = new User
                {
                    FirstName = "Inigo",
                    LastName = "Montoya"
                };
                dbContext.Users.Add(user1);

                await dbContext.SaveChangesAsync();

                userId = user1.Id;
            }

            // Act
            // Assert
            using (var dbContext = new ApplicationDbContext(Options))
            {
                var user = await dbContext.Users.Where(u => u.Id == userId).SingleOrDefaultAsync();
                Assert.AreEqual(1, user.Id);
                Assert.AreEqual("Inigo", user.FirstName);
                Assert.AreEqual("Montoya", user.LastName);
            }
        }

        [TestMethod]
        public async Task User_Create_FingerPrintCheck()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "imontoya"));

            int userId = -1;

            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {

                // Arrange
                var user1 = new User
                {
                    FirstName = "Inigo",
                    LastName = "Montoya"
                };
                dbContext.Users.Add(user1);

                await dbContext.SaveChangesAsync();

                userId = user1.Id;
            }

            // Act
            // Assert
            using (var dbContext = new ApplicationDbContext(Options))
            {
                var user = await dbContext.Users.Where(u => u.Id == userId).SingleOrDefaultAsync();
                Assert.AreEqual("imontoya", user.CreatedBy);
                Assert.AreEqual("imontoya", user.ModifiedBy);
            }
        }

        [DataRow(1, null!, "Montoya")]
        [DataRow(1, "Inigo", null!)]
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void User_SetFieldsToNull_ThrowsArgumentNullException(int id, string fname, string lname)
        {
            User user = new User
            {
                Id = id,
                FirstName = fname,
                LastName = lname
            };
        }
    }
}
