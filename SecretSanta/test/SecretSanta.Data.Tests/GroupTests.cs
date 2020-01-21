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

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[ExcludeFromCodeCoverage]
		public void GroupCreate_NullNameData_ThrowsException()
		{
			// Arrange
			Group group = new Group
			{
				Name = null!,
				UserGroups = new List<UserGroup>()
			};

			// Act

			// Assert
		}
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[ExcludeFromCodeCoverage]
		public void GroupCreate_NullUserGroupData_ThrowsException()
		{
			// Arrange
			Group group = new Group
			{
				Name = "group",
				UserGroups = null!
			};

			// Act

			// Assert
		}


		[TestMethod]
        public async Task GroupCreate_SaveToDatabase_GroupInserted()
        {
            // Arrange
            int groupId = -1;
            using (var applicationDbContext = new ApplicationDbContext(Options, HttpContextAccessor))
            {
                Group group = new Group
                {
                    Name = "My Group",
                    UserGroups = new List<UserGroup>()
                };
                applicationDbContext.Groups?.Add(group);



                await applicationDbContext.SaveChangesAsync();
                groupId = group.Id;
            }

            // Act

            // Assert
            using (var applicationDbContext = new ApplicationDbContext(Options, HttpContextAccessor))
            {
                var group = await applicationDbContext.Groups.Where(i => i.Id == groupId).SingleOrDefaultAsync();

                Assert.IsNotNull(group);
                Assert.AreEqual("My Group", group.Name);
            }
        }
    }
}
