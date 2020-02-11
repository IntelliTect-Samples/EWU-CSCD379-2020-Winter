using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business.Services;
using SecretSanta.Business.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SecretSanta.Business;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public abstract class BaseApiControllerTests<TDto, TInputDto, TService> 
        where TService : InMemoryEntityService<TDto, TInputDto>, new()
        where TInputDto : class
        where TDto : class, TInputDto, IEntity
    {
        protected abstract BaseApiController<TDto, TInputDto> CreateController(TService service);

        protected abstract TInputDto CreateInputDto();

        protected abstract TDto CreateDto(int id);

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
            service.Items.Add(CreateDto(service.Items.Count + 1));
            service.Items.Add(CreateDto(service.Items.Count + 1));
            service.Items.Add(CreateDto(service.Items.Count + 1));

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
            TDto dto = CreateDto(service.Items.Count + 1);
            service.Items.Add(dto);
            BaseApiController<TDto, TInputDto> controller = CreateController(service);

            IActionResult result = await controller.Get(dto.Id);

            var okResult = result as OkObjectResult;
            
            Assert.AreEqual(dto, okResult?.Value);
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
            TDto entity = CreateDto(service.Items.Count + 1);
            service.Items.Add(entity);
            BaseApiController<TDto, TInputDto> controller = CreateController(service);

            IActionResult result = await controller.Delete(entity.Id);

            Assert.IsTrue(result is OkResult);
        }

        private class ThrowingController : BaseApiController<TDto, TInputDto>
        {
            public ThrowingController() : base(null!)
            { }
        }
    }

    public class InMemoryEntityService<TDto, TInputDto> : IEntityService<TDto, TInputDto> 
        where TInputDto : class
        where TDto : class, TInputDto, IEntity
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
            IMapper mapper = AutomapperConfigurationProfile.CreateMapper();
            TDto dto = mapper.Map<TInputDto, TDto>(entity);
            dto.Id = Items.Count + 1;
            Items.Add(dto);
            return Task.FromResult(dto);
        }

        public Task<TDto?> UpdateAsync(int id, TInputDto entity)
        {
            if (Items.FirstOrDefault(x => x.Id == id) is TDto found)
            {
                int index = Items.IndexOf(found);
                IMapper mapper = AutomapperConfigurationProfile.CreateMapper();
                id = Items[index].Id;
                Items[index] = mapper.Map<TInputDto, TDto>(entity);
                Items[index].Id = id;
                return Task.FromResult<TDto?>(Items[index]);

            }
            return Task.FromResult(default(TDto));
        }
    }
}
