using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SecretSanta.Data.Tests
{
	[TestClass]
	public class GiftTests : TestBase
	{
		[TestMethod]
		public async Task Gift_CanBeSavedToDatabase()
		{
			// Arrange
			using (var dbContext = new ApplicationDbContext(Options))
			{
				var testGift = SampleData.CreateGiftViper();
				testGift.User = SampleData.CreateUserWilliamAdama();
				dbContext.Gifts.Add(testGift);
				await dbContext.SaveChangesAsync().ConfigureAwait(false);
			}
			// Act
			// Assert
			using (var dbContext = new ApplicationDbContext(Options))
			{
				var gifts = await dbContext.Gifts.ToListAsync();

				Assert.AreEqual(1, gifts.Count);
				Assert.AreEqual(SampleData.CreateGiftViper().Title, gifts[0].Title);
				Assert.AreEqual(SampleData.CreateGiftViper().Url, gifts[0].Url);
				Assert.AreEqual(SampleData.CreateGiftViper().Description, gifts[0].Description);
			}
		}

		[TestMethod]
		public async Task Gift_UpdateExisting_CorrectData()
		{
			// Arrange
			using ApplicationDbContext dbContext = new ApplicationDbContext(Options);

			var testGift = SampleData.CreateGiftFTLDrive();
			testGift.User = SampleData.CreateUserWilliamAdama();
			dbContext.Gifts.Add(testGift);
			await dbContext.SaveChangesAsync().ConfigureAwait(false);

			// Act
			Gift giftFromDb = await dbContext.Gifts.SingleAsync(i => i.Id == testGift.Id);
			giftFromDb.Title += " update";
			await dbContext.SaveChangesAsync().ConfigureAwait(false);

			giftFromDb = await dbContext.Gifts.SingleAsync(i => i.Id == testGift.Id);

			// Assert
			Assert.AreEqual(testGift.Title, giftFromDb.Title);
		}

		[TestMethod]
		public async Task Gift_HasFingerPrintDataAddedOnInitialSave()
		{
			// Arrange
			IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
				hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "dsergio"));

			
			using var dbContext = new ApplicationDbContext(Options, httpContextAccessor);

			dbContext.Gifts.Add(SampleData.CreateGiftCylonDetector());
			await dbContext.SaveChangesAsync().ConfigureAwait(false);

			// Act
			var gifts = await dbContext.Gifts.ToListAsync();

			// Assert
			Assert.AreEqual(1, gifts.Count);
			Assert.AreEqual("dsergio", gifts[0].CreatedBy);
			Assert.AreEqual("dsergio", gifts[0].ModifiedBy);

		}

		[TestMethod]
		public async Task Gift_HasFingerPrintDataUpdateOnUpdate()
		{
			IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
				hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "dsergio"));

			// Arrange
			using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
			{
				dbContext.Gifts.Add(SampleData.CreateGiftViper());
				await dbContext.SaveChangesAsync().ConfigureAwait(false);
			}
			// Act
			httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
					hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "someoneelse"));

			using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
			{
				var gifts = await dbContext.Gifts.ToListAsync();

				Assert.AreEqual(1, gifts.Count);
				gifts[0].Title = "Rogue Viper";

				await dbContext.SaveChangesAsync().ConfigureAwait(false);
			}

			// Assert
			using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
			{
				var gifts = await dbContext.Gifts.ToListAsync();

				Assert.AreEqual(1, gifts.Count);
				Assert.AreEqual("dsergio", gifts[0].CreatedBy);
				Assert.AreEqual("someoneelse", gifts[0].ModifiedBy);
			}
		}

		[DataTestMethod]
		[DataRow(null!, "description", "url")]
		[DataRow("title", null!, "url")]
		[DataRow("title", "description", null!)]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Gift_SetDataToNull_ThrowsArgumentNullException(string title, string description, string url)
		{
			_ = new Gift(title, description, url, SampleData.CreateUserWilliamAdama());
		}

	}
}
