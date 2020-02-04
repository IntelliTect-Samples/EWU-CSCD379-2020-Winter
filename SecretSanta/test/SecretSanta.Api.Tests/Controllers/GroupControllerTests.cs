using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests.Controllers
{
	[TestClass]
	public class GroupControllerTests
	{
		[TestMethod]
		public async Task GroupController_GetByIdWithExistingGift_Success()
		{
			// Arrange
			GroupTestService service = new GroupTestService();
			Group group = SampleData.CreateGroupColonialFleet();
			group = await service.InsertAsync(group);

			var controller = new GroupController(service);

			// Act
			ActionResult<Group> rv = await controller.Get(group.Id);

			// Assert
			Assert.IsTrue(rv.Result is OkObjectResult);
		}

		[TestMethod]
		public void GroupController_Create_Success()
		{
			// Arrange
			var service = new GroupTestService();

			// Act
			_ = new GroupController(service);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GroupController_NullService_ThrowsException()
		{
			// Arrange

			// Act
			_ = new GroupController(null!);
		}

		private class GroupTestService : IGroupService
		{
			private Dictionary<int, Group> Items { get; } = new Dictionary<int, Group>();

			public Task<bool> DeleteAsync(int id)
			{
				return Task.FromResult(Items.Remove(id));
			}

			public Task<List<Group>> FetchAllAsync()
			{
				List<Group> items = Items.Values.ToList();
				return Task.FromResult(items);
			}

			public Task<Group?> FetchByIdAsync(int id)
			{
				if (Items.TryGetValue(id, out var group))
				{
					Task<Group?> t1 = Task.FromResult<Group?>(group);
					return t1;
				}
				Task<Group?> t2 = Task.FromResult<Group?>(null);
				return t2;
			}

			public Task<Group> InsertAsync(Group entity)
			{
				int id = Items.Count + 1;
				Items[id] = new TestGroup(entity, id);
				return Task.FromResult(Items[id]);
			}

			public Task<Group[]> InsertAsync(params Group[] entity)
			{
				throw new NotImplementedException();
			}

			public Task<Group?> UpdateAsync(int id, Group entity)
			{
				Items[id] = entity;
				return Task.FromResult<Group?>(entity);
			}

			

			private class TestGroup : Group
			{
				public TestGroup(Group entity, int id)
					: base(entity.Title)
				{
					Id = id;
				}
			}
		}
	}
}
