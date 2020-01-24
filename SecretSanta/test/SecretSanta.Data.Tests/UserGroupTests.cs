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
    public class UserGroupTests : TestBase
    {

        [TestMethod]
        public async Task Create_UserWithManyGroups()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
               hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "caleb"));

            var user = new User
            {
                Id = 1,
                FirstName = "caleb",
                LastName = "walsh",
                Gifts = new List<Gift>()
            };
            var group1 = new Group
            {
                Name = "group1"
            };
            var group2 = new Group
            {
                Name = "group2"
            };

            // _Gift.User = _User;
            // _User.Gifts = new List<Gift> { _Gift };
            // _User.UserGroups = userGroups;
            //user.UserGroups = new List<UserGroup>();
            //user.UserGroups.Add(new UserGroup { User = user, Group = group1 });
            //user.UserGroups.Add(new UserGroup { User = user, Group = group2 });

            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();
            }


            //using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            //{
            //    var retrievedUsers = await dbContext.Users.Where(u => u.Id == user.Id).Include(u => u.Gifts)
            //        .Include(u => u.UserGroups).ThenInclude(g => g.Group).SingleOrDefaultAsync();

    
            //    Assert.IsNotNull(retrievedUsers);
            //    Assert.AreEqual(2, retrievedUsers.UserGroups.Count);
            //    //  Assert.AreEqual(user.FirstName, retrievedUsers.FirstName);
            //    // Assert.AreEqual(user.LastName, retrievedUsers.LastName);
            //    //  Assert.AreEqual(user.Id, retrievedUsers.Id);
            //    // Assert.IsNotNull(user.UserGroups[0]);
            //    // Assert.IsNotNull(user.UserGroups[1]);
            //    //Assert.AreEqual(user.UserGroups[0].GroupId, retrievedUsers.UserGroups[0].GroupId);
            //    //Assert.AreEqual(user.UserGroups[0].UserId, retrievedUsers.UserGroups[0].UserId);
            //    //Assert.AreEqual(user.UserGroups[1].GroupId, retrievedUsers.UserGroups[1].GroupId);
            //    //Assert.AreEqual(user.UserGroups[1].UserId, retrievedUsers.UserGroups[1].UserId);
            //    // Assert.AreEqual(1, retrievedUsers.Gifts.Count);

            //}
        }
    
        //[TestMethod]
        //public async Task CreateGroupWithManyUsers()
        //{
        //    // Arrange
        //    IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
        //        hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "imontoya"));

        //    var group = new Group();
            
           
        //    var user1 = new User
        //    {
        //        FirstName = "Inigo",
        //        LastName = "Montoya",
        //        Gifts = new List<Gift>()
        //    };
        //    var user2 = new User
        //    {
        //        FirstName = "Billy",
        //        LastName = "Bob",
        //        Gifts = new List<Gift>()
        //    };

        //    // Act
            
        //    group.UserGroups = new List<UserGroup>();
        //    group.UserGroups.Add(new UserGroup { Group = group, User = user1 });
        //    group.UserGroups.Add(new UserGroup { Group = group, User = user2 });

        //    using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
        //    {
        //        dbContext.Groups.Add(group);
        //        await dbContext.SaveChangesAsync();
        //    }

        //    // Assert
        //    using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
        //    {
        //        var retrievedPost = await dbContext.Groups.Where(p => p.Id == group.Id)
        //            .Include(p => p.UserGroups).ThenInclude(pt => pt.User).SingleOrDefaultAsync();

        //        Assert.IsNotNull(retrievedPost);
        //        Assert.AreEqual(2, retrievedPost.UserGroups.Count);
        //        Assert.IsNotNull(retrievedPost.UserGroups[0].User);
        //        Assert.IsNotNull(retrievedPost.UserGroups[1].User);
        //    }
      //  }
    }
}
