using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecretSanta.Business;
using SecretSanta.Data;
using SecretSanta.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using SecretSanta.Data.Tests;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace SecretSanta.Api.Tests.Controllers
{
	[TestClass]
	public class GiftControllerTests
	{
		[TestMethod]
		public async Task GiftController_Update_Success()
		{
			// Arrange
			GiftTestService service = new GiftTestService();
			Gift gift = SampleData.CreateGiftFTLDrive();
			gift = await service.InsertAsync(gift);

			var controller = new GiftController(service);

			// Act
			gift.Title = "FTL Drive V2";
			ActionResult<Gift> rv = await controller.Put(gift.Id, gift);

			// Assert
			Assert.AreEqual("FTL Drive V2", rv.Value.Title);
		}

		[TestMethod]
		public async Task GiftController_Delete_Success()
		{
			// Arrange
			GiftTestService service = new GiftTestService();
			Gift gift = SampleData.CreateGiftCylonDetector();
			gift = await service.InsertAsync(gift);

			var controller = new GiftController(service);

			// Act
			ActionResult<Gift> rv = await controller.Delete(gift.Id);

			// Assert
			Assert.IsTrue(rv.Result is OkResult);
		}

		[TestMethod]
		public async Task GetById_WithExistingGift_Success()
		{
			// Arrange
			GiftTestService service = new GiftTestService();
			Gift gift = SampleData.CreateGiftCylonDetector();
			gift = await service.InsertAsync(gift);

			var controller = new GiftController(service);

			// Act
			ActionResult<Gift> rv = await controller.Get(gift.Id);

			// Assert
			Assert.IsTrue(rv.Result is OkObjectResult);
		}

		[TestMethod]
		public void GiftController_Create_Success()
		{
			// Arrange
			var service = new GiftTestService();

			// Act
			_ = new GiftController(service);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GiftController_NullService_ThrowsException()
		{
			// Arrange
			
			// Act
			_ = new GiftController(null!);
		}

		private class GiftTestService : IGiftService
		{
			private Dictionary<int, Gift> Items { get; } = new Dictionary<int, Gift>();

			public Task<bool> DeleteAsync(int id)
			{
				return Task.FromResult(Items.Remove(id));
			}

			public Task<List<Gift>> FetchAllAsync()
			{
				List<Gift> items = Items.Values.ToList();
				return Task.FromResult(items);
			}

			public Task<Gift?> FetchByIdAsync(int id)
			{
				if (Items.TryGetValue(id, out var gift))
				{
					Task<Gift?> t1 = Task.FromResult<Gift?>(gift);
					return t1;
				}
				Task<Gift?> t2 = Task.FromResult<Gift?>(null);
				return t2;
			}

			public Task<Gift> InsertAsync(Gift entity)
			{
				int id = Items.Count + 1;
				Items[id] = new TestGift(entity, id);
				return Task.FromResult(Items[id]);
			}

			public Task<Gift[]> InsertAsync(params Gift[] entity)
			{
				throw new NotImplementedException();
			}

			public Task<Gift?> UpdateAsync(int id, Gift entity)
			{
				Items[id] = entity;
				return Task.FromResult<Gift?>(entity);
			}

			private class TestGift : Gift
			{
				public TestGift(Gift entity, int id)
					: base(entity.Title, entity.Url, entity.Description, entity.User)
				{
					Id = id;
				}
			}
		}
	}
}
