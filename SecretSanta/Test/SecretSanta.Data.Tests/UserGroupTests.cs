using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

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
	}
}
