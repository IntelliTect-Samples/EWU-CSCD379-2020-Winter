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
    public class GroupTests : TestBase
    {
        private readonly Group _Group = new Group
        {
            Name = "group",
            UserGroups = new List<UserGroup>()
        };


        [TestMethod]
        public async Task CreateGroup_ShouldSaveIntoDatabase()
        {
            int groupId = -1;
            // Arrange
            using (var applicationDbContext = new ApplicationDbContext(Options))
            {
                
                applicationDbContext.Groups.Add(_Group);
                await applicationDbContext.SaveChangesAsync();

                groupId = _Group.Id;
                
            }

            // Act
            // Assert
            using (var applicationDbContext = new ApplicationDbContext(Options))
            {
                var group = await applicationDbContext.Groups.Where(a => a.Id == groupId).SingleOrDefaultAsync();

                Assert.IsNotNull(group);
                Assert.AreEqual(_Group.Name, group.Name);
            }
        }

        [TestMethod]
        public async Task CreateGroup_ShouldSetFingerPrintDataOnInitialSave()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "caleb"));

            int groupId = -1;
            // Arrange
            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                
                applicationDbContext.Groups.Add(_Group);
                await applicationDbContext.SaveChangesAsync();

                groupId = _Group.Id;
            }

            // Act
            // Assert
            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var group = await applicationDbContext.Groups.Where(a => a.Id == groupId).SingleOrDefaultAsync();

                Assert.IsNotNull(group);
                Assert.AreEqual("caleb", group.CreatedBy);
                Assert.AreEqual("caleb", group.ModifiedBy);
                
            }
        }

        [TestMethod]
        public async Task CreateGroup_ShouldSetFingerPrintDataOnUpdate()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "imontoya"));

            int groupId = -1;
            // Arrange
            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var group = new Group();
                applicationDbContext.Groups.Add(group);

                var group2 = new Group();
                applicationDbContext.Groups.Add(group2);

                await applicationDbContext.SaveChangesAsync();

                groupId = group.Id;
            }

            // Act
            // change the group that is updating the record
            httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "pbuttercup"));
            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                // Since we are pulling back the record from the database and making changes to it, we don't need to re-add it to the collection
                // thus no Users.Add call, that is only needed when new records are inserted
                var group = await applicationDbContext.Groups.Where(a => a.Id == groupId).SingleOrDefaultAsync();
                group.Name = "Princess";

                await applicationDbContext.SaveChangesAsync();
            }
            // Assert
            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var group = await applicationDbContext.Groups.Where(a => a.Id == groupId).SingleOrDefaultAsync();

                Assert.IsNotNull(group);
                Assert.AreEqual("Princess", group.Name);
            }
        }
    }
}
