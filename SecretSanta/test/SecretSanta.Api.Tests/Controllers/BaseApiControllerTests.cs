using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
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
        where TDto : class,TInputDto
        where TInputDto : class
        where TService : InMemoryEntityService<TEntity,TDto,TInputDto>, new()
    {
        private IMapper Mapper { get; } = AutomapperConfigurationProfile.CreateMapper();
        protected abstract BaseApiController<TDto, TInputDto> CreateController(TService service);
        protected abstract TEntity CreateEntity();
        protected abstract TDto CreateDto();
        protected abstract TInputDto CreateInputDto();
        protected abstract bool DTosAreEqual(TDto dto1, TDto dto2);

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

            BaseApiController<TDto,TInputDto> controller = CreateController(service);

            IEnumerable<TDto> itemsReturned = await controller.Get();
            IEnumerable<TDto> items = Mapper.Map<List<TEntity>, List<TDto>>(service.Items.ToList());
            bool DtosAreTheSame = Enumerable.SequenceEqual<TDto>(items, itemsReturned);
            Assert.IsTrue(DtosAreTheSame);
        }

        [TestMethod]
        public async Task Get_WhenEntityDoesNotExist_ReturnsNotFound()
        {
            TService service = new TService();
            BaseApiController<TDto,TInputDto> controller = CreateController(service);

            IActionResult result = await controller.Get(1);

            Assert.IsTrue(result is NotFoundResult);
        }


        [TestMethod]
        public async Task Get_WhenEntityExists_ReturnsItem()
        {
            TService service = new TService();
            TEntity entity = CreateEntity();
            service.Items.Add(entity);
            BaseApiController<TDto,TInputDto> controller = CreateController(service);

            IActionResult result = await controller.Get(entity.Id);

            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            TDto? DtoResult = okResult!.Value as TDto;
            Assert.IsNotNull(DtoResult);
            Assert.AreEqual<TDto>(Mapper.Map<TEntity,TDto>(entity), DtoResult!);
        }

        [TestMethod]
        public async Task Put_UpdatesItem()
        {
            TService service = new TService();
            TEntity entity1 = CreateEntity();
            service.Items.Add(entity1);
            TInputDto entity2 = CreateInputDto();
            BaseApiController<TDto,TInputDto> controller = CreateController(service);

            TDto? DtoResult = await controller.Put(entity1.Id, entity2);
            if (DtoResult is null)
                Assert.Fail("Put returned null");
            TEntity result = Mapper.Map<TDto,TEntity>(DtoResult!);

            TDto dto = Mapper.Map<TEntity, TDto>(Mapper.Map<TInputDto, TEntity>(entity2));
            TDto resultDto = Mapper.Map<TEntity, TDto>(result);

            Assert.AreEqual<TDto>(dto, resultDto);
        }

        [TestMethod]
        public async Task Post_InsertsItem()
        {
            TService service = new TService();
            TInputDto entity = CreateInputDto();
            BaseApiController<TDto,TInputDto> controller = CreateController(service);

            TEntity? result = Mapper.Map<TDto,TEntity>(await controller.Post(entity));

            TDto dto = Mapper.Map<TEntity,TDto>(Mapper.Map<TInputDto, TEntity>(entity));
            TDto resultDto = Mapper.Map<TEntity, TDto>(result);
            Assert.AreEqual<TDto>(dto, resultDto);
        }

        [TestMethod]
        public async Task Delete_WhenItemDoesNotExist_ReturnsNotFound()
        {
            TService service = new TService();
            BaseApiController<TDto,TInputDto> controller = CreateController(service);

            IActionResult result = await controller.Delete(1);

            Assert.IsTrue(result is NotFoundResult);
        }

        [TestMethod]
        public async Task Delete_WhenItemExists_ReturnsOk()
        {
            TService service = new TService();
            TEntity entity = CreateEntity();
            service.Items.Add(entity);
            BaseApiController<TDto,TInputDto> controller = CreateController(service);

            IActionResult result = await controller.Delete(entity.Id);

            Assert.IsTrue(result is OkResult);
        }

        private class ThrowingController : BaseApiController<TDto,TInputDto>
        {
            public ThrowingController() : base(null!)
            { }
        }
    }

    public class InMemoryEntityService<TEntity,TDto,TInputDto> : IEntityService<TDto,TInputDto>
        where TEntity : EntityBase
        where TInputDto : class
        where TDto : class, TInputDto
    {
        public IList<TEntity> Items { get; } = new List<TEntity>();
        private IMapper Mapper { get; } = AutomapperConfigurationProfile.CreateMapper();

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
            return Task.FromResult(Mapper.Map<List<TEntity>,List<TDto>>(Items.ToList()));
        }

        public Task<TDto> FetchByIdAsync(int id)
        {
            return Task.FromResult(Mapper.Map<TEntity,TDto>(Items.FirstOrDefault(x => x.Id == id)));
        }

        public Task<TDto> InsertAsync(TInputDto input)
        {
            TEntity entity = Mapper.Map<TInputDto, TEntity>(input);
            Items.Add(entity);
            return Task.FromResult(Mapper.Map<TEntity,TDto>(entity));
        }

        public Task<TDto?> UpdateAsync(int id, TInputDto input)
        {
            TEntity entity = Mapper.Map<TInputDto, TEntity>(input);
            if (Items.FirstOrDefault(x => x.Id == id) is { } found)
            {
                Items[Items.IndexOf(found)] = entity;
                return Task.FromResult<TDto?>(Mapper.Map<TEntity,TDto>(entity));
            }
            return Task.FromResult(default(TDto));
        }
    }
}
