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
	public class UserGroupTests : TestBase
	{
		[TestMethod]
		public void UserGroupCreate_ValidData_ValidData()
		{
			// Arrange
			Group group1 = new Group
			{
				Id = 1,
				Name = "Group 1",
				UserGroups = new List<UserGroup>()
			};

			User user1 = new User
			{
				Id = 1,
				FirstName = "David",
				LastName = "Sergio",
				Gifts = new List<Gift>()
			};

			UserGroup userGroup = new UserGroup
			{
				Group = group1,
				GroupId = group1.Id,
				User = user1,
				UserId = user1.Id
			};

			// Act

			// Assert
			Assert.AreEqual(user1.FirstName, userGroup.User.FirstName);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[ExcludeFromCodeCoverage]
		public void UserGroupCreate_GroupNull_ThrowsException()
		{
			// Arrange

			User user1 = new User
			{
				Id = 1,
				FirstName = "David",
				LastName = "Sergio",
				Gifts = new List<Gift>()
			};

			UserGroup userGroup = new UserGroup
			{
				Group = null!,
				GroupId = 0,
				User = user1,
				UserId = user1.Id
			};

			// Act

			// Assert
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[ExcludeFromCodeCoverage]
		public void UserGroupCreate_UserNull_ThrowsException()
		{
			// Arrange
			Group group1 = new Group
			{
				Id = 1,
				Name = "Group 1",
				UserGroups = new List<UserGroup>()
			};


			UserGroup userGroup = new UserGroup
			{
				Group = group1,
				GroupId = group1.Id,
				User = null!,
				UserId = 0
			};

			// Act

			// Assert
		}

		[TestMethod]
		public async Task UserGroupCreate_SaveToDatabase_UserGroupInserted()
		{
			// Arrange
			int groupId = -1;
			int userId = -1;
			using (var applicationDbContext = new ApplicationDbContext(Options, HttpContextAccessor))
			{
				Group group1 = new Group
				{
					Name = "Group 1",
					UserGroups = new List<UserGroup>()
				};
				applicationDbContext.Groups?.Add(group1);

				User user1 = new User
				{
					FirstName = "David",
					LastName = "Sergio",
					Gifts = new List<Gift>()
				};
				applicationDbContext.Users?.Add(user1);

				UserGroup userGroup = new UserGroup
				{
					Group = group1,
					GroupId = group1.Id,
					User = user1,
					UserId = user1.Id
				};
				applicationDbContext.UserGroups?.Add(userGroup);


				await applicationDbContext.SaveChangesAsync();
				groupId = userGroup.GroupId;
				userId = userGroup.UserId;
			}

			// Act

			// Assert
			using (var applicationDbContext = new ApplicationDbContext(Options, HttpContextAccessor))
			{
				var userGroup = await applicationDbContext.UserGroups.Where(i => i.GroupId == groupId).SingleOrDefaultAsync();

				Assert.IsNotNull(userGroup);
				Assert.AreEqual(groupId, userGroup.GroupId);
				Assert.AreEqual(userId, userGroup.UserId);
			}
		}

		[TestMethod]
		public async Task UserCreate_CreateUserWithManyGroups_DataInserted()
		{
			// Arrange
			User user = new User
			{
				FirstName = "David",
				LastName = "Sergio"
			};
			
			Group group1 = new Group
			{
				Name = "Group 1"
			};
			Group group2 = new Group
			{
				Name = "Group 2"
			};
			Group group3 = new Group
			{
				Name = "Group 3"
			};

			user.UserGroups.Add(new UserGroup { Group = group1, User = user });
			user.UserGroups.Add(new UserGroup { Group = group2, User = user });
			user.UserGroups.Add(new UserGroup { Group = group3, User = user });

			// Act
			// Assert
			using (var applicationDbContext = new ApplicationDbContext(Options, HttpContextAccessor))
			{
				applicationDbContext.Users?.Add(user);
				await applicationDbContext.SaveChangesAsync();

				var retrievedUser = await applicationDbContext.Users
					.Where(i => i.Id == user.Id)
					.Include(i => i.UserGroups)
					.ThenInclude(k => k.Group)
					.SingleOrDefaultAsync();


				Assert.IsNotNull(retrievedUser);
				Assert.AreEqual(3, retrievedUser.UserGroups.Count);
				Assert.IsNotNull(retrievedUser.UserGroups[0].Group);
				Assert.IsNotNull(retrievedUser.UserGroups[1].Group);
				Assert.IsNotNull(retrievedUser.UserGroups[2].Group);
			}
		}
	}
}
