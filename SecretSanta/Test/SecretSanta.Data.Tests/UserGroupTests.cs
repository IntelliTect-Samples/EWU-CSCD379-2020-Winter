using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
	}
}
