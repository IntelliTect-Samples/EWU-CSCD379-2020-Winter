using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SecretSanta.Data.Tests
{

    [TestClass]
    public class UserTests : TestBase
    {

        [TestMethod]
        public void Constructor_GivenValidData_NotNull()
        {
            var user = new User {Id = 1, FirstName = "Brett", LastName = "Henning", Gifts = new List<Gift>()};

            Assert.IsNotNull(user);
        }

        [DataTestMethod]
        [DataRow(null, "LastName", false)]
        [DataRow("FirstName", null, false)]
        [DataRow("FirstName", "LastName", true)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_InvalidData_ThrowsException(string firstName, string lastName, bool nullList)
        {
            // ReSharper disable once AssignmentIsFullyDiscarded
            _ = new User
            {
                Id = 1, FirstName = "Brett", LastName = "Henning", Gifts = nullList ? null : new List<Gift>()
            };
        }

        [TestMethod]
        public async Task Gift_SaveToDb_RunsCorrectly()
        {
            int id;
            await using (var context = new ApplicationDbContext(Options, Accessor))
            {
                var user = new User {Id = 1, FirstName = "Brett", LastName = "Henning", Gifts = new List<Gift>()};
                context.Users.Add(user);

                await context.SaveChangesAsync();
                id = user.Id;
            }

            await using (var context = new ApplicationDbContext(Options, Accessor))
            {
                var user = await context.Users.Where(u => u.Id == id).SingleOrDefaultAsync();

                Assert.IsNotNull(user);
                Assert.AreEqual(user.Id, id);
                Assert.AreEqual(user.FirstName, "Brett");
                Assert.AreEqual(user.LastName, "Henning");
            }
        }

    }

}
