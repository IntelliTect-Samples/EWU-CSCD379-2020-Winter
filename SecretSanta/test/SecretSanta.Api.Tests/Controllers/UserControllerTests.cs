using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
	public class UserControllerTests
	{
		[TestMethod]
		public async Task UserController_Update_Success()
		{
			// Arrange
			UserTestService service = new UserTestService();
			User user = SampleData.CreateUserLauraRoslin();
			user = await service.InsertAsync(user);

			var controller = new UserController(service);

			// Act
			user.FirstName = "President";
			ActionResult<User> rv = await controller.Put(user.Id, user);

			// Assert
			Assert.AreEqual("President", rv.Value.FirstName);
		}

		[TestMethod]
		public async Task UserController_Delete_Success()
		{
			// Arrange
			UserTestService service = new UserTestService();
			User user = SampleData.CreateUserLauraRoslin();
			user = await service.InsertAsync(user);

			var controller = new UserController(service);

			// Act
			ActionResult<User> rv = await controller.Delete(user.Id);

			// Assert
			Assert.IsTrue(rv.Result is OkResult);
		}

		[TestMethod]
		public async Task UserController_GetByIdWithExistingGift_Success()
		{
			// Arrange
			UserTestService service = new UserTestService();
			User user = SampleData.CreateUserGaiusBaltar();
			user = await service.InsertAsync(user);

			var controller = new UserController(service);

			// Act
			ActionResult<User> rv = await controller.Get(user.Id);

			// Assert
			Assert.IsTrue(rv.Result is OkObjectResult);
		}

		[TestMethod]
		public void UserController_Create_Success()
		{
			// Arrange
			var service = new UserTestService();

			// Act
			_ = new UserController(service);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void UserController_NullService_ThrowsException()
		{
			// Arrange

			// Act
			_ = new UserController(null!);
		}

		private class UserTestService : IUserService
		{
			private Dictionary<int, User> Items { get; } = new Dictionary<int, User>();

			public Task<bool> DeleteAsync(int id)
			{
				return Task.FromResult(Items.Remove(id));
			}

			public Task<List<User>> FetchAllAsync()
			{
				List<User> items = Items.Values.ToList();
				return Task.FromResult(items);
			}

			public Task<User?> FetchByIdAsync(int id)
			{
				if (Items.TryGetValue(id, out var user))
				{
					Task<User?> t1 = Task.FromResult<User?>(user);
					return t1;
				}
				Task<User?> t2 = Task.FromResult<User?>(null);
				return t2;
			}

			public Task<User> InsertAsync(User entity)
			{
				int id = Items.Count + 1;
				Items[id] = new TestUser(entity, id);
				return Task.FromResult(Items[id]);
			}

			public Task<User[]> InsertAsync(params User[] entity)
			{
				throw new NotImplementedException();
			}

			public Task<User?> UpdateAsync(int id, User entity)
			{
				Items[id] = entity;
				return Task.FromResult<User?>(entity);
			}
			private class TestUser : User
			{
				public TestUser(User entity, int id)
					: base(entity.FirstName, entity.LastName)
				{
					Id = id;
				}
			}
		}
	}
}
