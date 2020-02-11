using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Business.Dto;
using SecretSanta.Business.Services;
using SecretSanta.Data;

namespace SecretSanta.Api.Tests.Controllers
{

    [TestClass]
    public abstract class BaseApiControllerTests<TDto, TInputDto, TService>
        where TInputDto : class
        where TDto : class, TInputDto, IEntity
        where TService : InMemoryEntityService<TDto, TInputDto>, new()
    {

        protected abstract BaseApiController<TDto, TInputDto> CreateController(TService service);

        protected abstract TDto CreateDto();

        private SecretSantaWebApplicationFactory Factory { get; set; }

        private   HttpClient Client { get; set; }
        protected IMapper    Mapper { get; } = AutomapperConfigurationProfile.CreateMapper();

        [TestInitialize]
        public void Setup()
        {
            Factory = new SecretSantaWebApplicationFactory();

            using ApplicationDbContext context = Factory.GetDbContext();
            context.Database.EnsureCreated();

            Client = Factory.CreateClient();
        }

        [TestCleanup]
        public void Cleanup()
        {
            Factory.Dispose();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_RequiresService()
        {
            new ThrowingController();
        }

        [TestMethod]
        public async Task Get_FetchesAllItems()
        {
            var service = new TService();
            service.Items.Add(CreateDto());
            service.Items.Add(CreateDto());
            service.Items.Add(CreateDto());

            BaseApiController<TDto, TInputDto> controller = CreateController(service);

            IEnumerable<TDto> items = await controller.Get();

            CollectionAssert.AreEqual(service.Items.ToList(), items.ToList());
        }

        [TestMethod]
        public async Task Get_WhenEntityDoesNotExist_ReturnsNotFound()
        {
            TService service = new TService();

            BaseApiController<TDto, TInputDto> controller = CreateController(service);

            IActionResult result = await controller.Get(1);

            Assert.IsTrue(result is NotFoundResult);
        }

        [TestMethod]
        public async Task Get_WhenEntityExists_ReturnsItem()
        {
            TService service = new TService();
            TDto     entity  = CreateDto();
            service.Items.Add(entity);
            BaseApiController<TDto, TInputDto> controller = CreateController(service);

            IActionResult result = await controller.Get(entity.Id);

            var okResult = result as OkObjectResult;

            Assert.AreEqual(entity, okResult?.Value);
        }

        [TestMethod]
        public async Task Put_UpdatesItem()
        {
            TService service = new TService();
            TDto     entity1 = CreateDto();
            service.Items.Add(entity1);
            TDto entity2 = CreateDto();

            BaseApiController<TDto, TInputDto> controller = CreateController(service);

            TDto? result = await controller.Put(entity1.Id, entity2);

            Assert.AreEqual(entity2, result);
            Assert.AreEqual(entity2, service.Items.Single());
        }

        [TestMethod]
        public async Task Post_InsertsItem()
        {
            TService service = new TService();
            TDto     entity  = CreateDto();

            BaseApiController<TDto, TInputDto> controller = CreateController(service);

            TDto? result = await controller.Post(entity);

            Assert.AreEqual(entity, result);
            Assert.AreEqual(entity, service.Items.Single());
        }

        [TestMethod]
        public async Task Delete_WhenItemDoesNotExist_ReturnsNotFound()
        {
            TService service = new TService();

            BaseApiController<TDto, TInputDto> controller = CreateController(service);

            IActionResult result = await controller.Delete(1);

            Assert.IsTrue(result is NotFoundResult);
        }

        [TestMethod]
        public async Task Delete_WhenItemExists_ReturnsOk()
        {
            TService service = new TService();
            TDto     entity  = CreateDto();
            service.Items.Add(entity);
            BaseApiController<TDto, TInputDto> controller = CreateController(service);

            IActionResult result = await controller.Delete(entity.Id);

            Assert.IsTrue(result is OkResult);
        }

        private class ThrowingController : BaseApiController<TDto, TInputDto>
        {

            public ThrowingController() : base(null!)
            {
            }

        }

    }

    public class InMemoryEntityService<TDto, TInputDto> : IEntityService<TDto, TInputDto>
        where TInputDto : class where TDto : class, TInputDto, IEntity
    {

        public IList<TDto> Items { get; } = new List<TDto>();

        public Task<bool> DeleteAsync(int id)
        {
            if (Items.FirstOrDefault(x => x.Id == id) is { } found)
            {
                return Task.FromResult(Items.Remove(found));
            }

            return Task.FromResult(false);
        }

        public Task<List<TDto>> FetchAllAsync()
        {
            return Task.FromResult(Items.ToList());
        }

        public Task<TDto> FetchByIdAsync(int id)
        {
            return Task.FromResult(Items.FirstOrDefault(x => x.Id == id));
        }

        public Task<TDto> InsertAsync(TInputDto entity)
        {
            var add = entity as TDto;
            Items.Add(add);
            return Task.FromResult(add);
        }

        public Task<TDto?> UpdateAsync(int id, TInputDto entity)
        {
            if (Items.FirstOrDefault(x => x.Id == id) is { } found)
            {
                var update = entity as TDto;
                Items[Items.IndexOf(found)] = update;
                return Task.FromResult(update);
            }

            return Task.FromResult(default(TDto));
        }

    }

}
