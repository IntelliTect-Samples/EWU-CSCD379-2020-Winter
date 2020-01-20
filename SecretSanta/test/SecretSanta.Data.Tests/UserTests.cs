using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class UserTests : TestBase
    {
        [TestMethod]
        public void User_CanBeCreate_AllPropertiesGetSet()
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

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void User_SetFirstNameToNull_ThrowsArgumentNullException()
        {
            _ = new User
            {
                FirstName = null!
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void User_SetLastNameToNull_ThrowsArgumentNullException()
        {
            _ = new User
            {
                LastName = null!
            };
        }

        [TestMethod]
        public async Task AddUser_SampleUser_ShouldExistInDatabase()
        {
            //Arrange
            //Act
            using (ApplicationDbContext applicationDbContext = new ApplicationDbContext(Options))
            {
                applicationDbContext.Users.Add(_Spongebob);
                await applicationDbContext.SaveChangesAsync();
            }
            //Assert
            using (ApplicationDbContext applicationDbContext =  new ApplicationDbContext((Options)))
            {
                User user = await applicationDbContext.Users.Where(u => u.Id == _Spongebob.Id).SingleOrDefaultAsync();
                Assert.IsNotNull(user);
                Assert.AreEqual<int>(_Spongebob.Id, user.Id);
                Assert.AreEqual<string>(_Spongebob.FirstName, user.FirstName);
            }
        }
    }
}
