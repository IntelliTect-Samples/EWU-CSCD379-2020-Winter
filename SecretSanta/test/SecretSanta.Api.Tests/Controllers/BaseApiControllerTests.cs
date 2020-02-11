using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Business.Dto;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public abstract class BaseApiControllerTests<TEntity, TDto, TInputDto, TService> 
        where TEntity : EntityBase
        where TInputDto : class
        where TDto : class, TInputDto, IEntity
        where TService : InMemoryEntityService<TEntity, TInputDto, TDto>, new()
    {
        protected abstract BaseApiController<TInputDto, TDto> CreateController(TService service);
        private IMapper Mapper { get; } = AutomapperConfigurationProfile.CreateMapper();

        protected abstract TEntity CreateEntity();

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_RequiresService()
        {
            new ThrowingController();
        }

        [TestMethod]
        public async Task Get_FetchesAllItems()
        {
            TService service = new TService();
            service.Items.Add(CreateEntity());
            service.Items.Add(CreateEntity());
            service.Items.Add(CreateEntity());

            BaseApiController<TInputDto, TDto> controller = CreateController(service);

            IEnumerable<TDto> items = await controller.Get();

            CollectionAssert.AreEqual(Mapper.Map<List<TEntity>, List<TDto>>(service.Items.ToList()), items.ToList());
        }

        [TestMethod]
        public async Task Get_WhenEntityDoesNotExist_ReturnsNotFound()
        {
            TService service = new TService();
            BaseApiController<TInputDto, TDto> controller = CreateController(service);

            IActionResult result = await controller.Get(1);

            Assert.IsTrue(result is NotFoundResult);
        }


        [TestMethod]
        public async Task Get_WhenEntityExists_ReturnsItem()
        {
            TService service = new TService();
            TEntity entity = CreateEntity();
            service.Items.Add(entity);
            BaseApiController<TInputDto, TDto> controller = CreateController(service);

            IActionResult result = await controller.Get(entity.Id);

            var okResult = result as OkObjectResult;
            
            Assert.AreEqual(entity, okResult?.Value);
        }

        [TestMethod]
        public async Task Put_UpdatesItem()
        {
            TService service = new TService();
            TEntity entity1 = CreateEntity();
            service.Items.Add(entity1);
            TEntity entity2 = CreateEntity();
            BaseApiController<TInputDto, TDto> controller = CreateController(service);

            TDto? result = await controller.Put(entity1.Id, Mapper.Map<TEntity, TInputDto>(entity2));

            Assert.AreEqual(entity2, result);
            Assert.AreEqual(entity2, service.Items.Single());
        }

        [TestMethod]
        public async Task Post_InsertsItem()
        {
            TService service = new TService();
            TEntity entity = CreateEntity();
            BaseApiController<TInputDto, TDto> controller = CreateController(service);

            TDto? result = await controller.Post(Mapper.Map<TEntity, TInputDto>(entity));

            Assert.AreEqual(entity, result);
            Assert.AreEqual(entity, service.Items.Single());
        }

        [TestMethod]
        public async Task Delete_WhenItemDoesNotExist_ReturnsNotFound()
        {
            TService service = new TService();
            BaseApiController<TInputDto, TDto> controller = CreateController(service);

            IActionResult result = await controller.Delete(1);

            Assert.IsTrue(result is NotFoundResult);
        }

        [TestMethod]
        public async Task Delete_WhenItemExists_ReturnsOk()
        {
            TService service = new TService();
            TEntity entity = CreateEntity();
            service.Items.Add(entity);
            BaseApiController<TInputDto, TDto> controller = CreateController(service);

            IActionResult result = await controller.Delete(entity.Id);

            Assert.IsTrue(result is OkResult);
        }

        private class ThrowingController : BaseApiController<TInputDto, TDto>
        {
            public ThrowingController() : base(null!)
            { }
        }
    }

    public class InMemoryEntityService<TEntity, TInputDto, TDto> : IEntityService<TDto, TInputDto>
        where TEntity : EntityBase
        where TInputDto : class
        where TDto : class, TInputDto, IEntity
    {
        private IMapper Mapper { get; } = AutomapperConfigurationProfile.CreateMapper();

        public IList<TEntity> Items { get; } = new List<TEntity>();

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
            return Task.FromResult(Mapper.Map<List<TEntity>, List<TDto>>(Items.ToList()));
        }

        public Task<TDto> FetchByIdAsync(int id)
        {
            return Task.FromResult(Mapper.Map<TEntity, TDto>(Items.FirstOrDefault(x => x.Id == id)));
        }

        public Task<TDto> InsertAsync(TInputDto dto)
        {
            TEntity entity = Mapper.Map<TInputDto, TEntity>(dto);
            Items.Add(entity);
            return Task.FromResult(Mapper.Map<TEntity, TDto>(entity));
        }

        public Task<TDto?> UpdateAsync(int id, TInputDto dto)
        {
            if (Items.FirstOrDefault(x => x.Id == id) is TEntity found)
            {
                TEntity entity = Mapper.Map<TInputDto, TEntity>(dto);
                Items[Items.IndexOf(found)] = entity;
                return Task.FromResult<TDto?>(Mapper.Map<TEntity, TDto>(entity));
            }
            return Task.FromResult(default(TDto));
        }
    }
}
