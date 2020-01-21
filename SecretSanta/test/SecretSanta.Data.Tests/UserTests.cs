using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class UserTests : TestBase
    {
        [TestMethod]
        public void UserCreate_ValidData_ValidData()
        {
            // Arrange
            User user = new User
            {
                Id = 1,
                FirstName = "Inigo",
                LastName = "Montoya",
                Gifts = new List<Gift>()
            };

            // Act
            // Assert
            Assert.AreEqual(1, user.Id);
            Assert.AreEqual("Inigo", user.FirstName);
            Assert.AreEqual("Montoya", user.LastName);
            Assert.IsNotNull(user.Gifts);
        }

        [DataTestMethod]
        [DataRow(null!, "Sergio")]
        [DataRow("David", null!)]
        [ExpectedException(typeof(ArgumentNullException))]
        [ExcludeFromCodeCoverage]
        public void UserCreate_NullData_ThrowsException(string firstName, string lastName)
        {
            // Arrange

            // Act
            _ = new User
            {
                Id = 1,
                FirstName = firstName,
                LastName = lastName,
                Gifts = new List<Gift>()
            };
        }

        [TestMethod]
        public async Task UserCreate_SaveToDatabase_UserInserted()
        {
            // Arrange
            int userId = -1;
            using (var applicationDbContext = new ApplicationDbContext(Options, HttpContextAccessor))
            {
                User user = new User
                {
                    FirstName = "David",
                    LastName = "Sergio",
                    Gifts = new List<Gift>()
                };
                applicationDbContext.Users?.Add(user);

                

                await applicationDbContext.SaveChangesAsync();
                userId = user.Id;
            }

            // Act

            // Assert
            using (var applicationDbContext = new ApplicationDbContext(Options, HttpContextAccessor))
            {
                var user = await applicationDbContext.Users.Where(i => i.Id == userId).SingleOrDefaultAsync();

                Assert.IsNotNull(user);
                Assert.AreEqual("David", user.FirstName);
            }
        }
    }
}
