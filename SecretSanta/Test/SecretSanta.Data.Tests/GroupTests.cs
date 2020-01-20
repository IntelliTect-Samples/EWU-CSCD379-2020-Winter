using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data.Tests
{
	[TestClass]
	public class GroupTests : TestBase
	{
		[TestMethod]
		public void GroupCreate_ValidData_ValidData()
		{
			// Arrange
			Group group = new Group
			{
				Name = "My Group",
				UserGroups = new List<UserGroup>()
			};

			// Act

			// Assert
			Assert.AreEqual("My Group", group.Name);
			Assert.IsNotNull(group.UserGroups);
		}
	}
}
