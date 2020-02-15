using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Business.Services;
using Dto = SecretSanta.Business.Dto;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using AutoMapper;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public abstract class BaseApiControllerTests<TEntity, TDto, TInputDto, TService> 
        where TEntity : EntityBase
        where TInputDto : class
        where TDto : class, TInputDto, Dto.IEntity
        where TService : InMemoryEntityService<TEntity, TDto, TInputDto>, new()
    {
#nullable disable // this sucks... but must be done cause test setup does them
        protected SecretSantaWebApplicationFactory Factory { get; set; }
        protected HttpClient Client { get; set; }
        protected IMapper Mapper { get; set; } = AutomapperConfigurationProfile.CreateMapper();
#nullable enable

        protected abstract BaseApiController<TDto, TInputDto> CreateController(TService service);

        protected abstract TEntity CreateEntity();

        /*protected DbSet<TEntity> GetDbContextField(ApplicationDbContext context)
        {
            // hacky ass reflective way to generically get the appropriate field to add to
            PropertyInfo[] props = typeof(ApplicationDbContext).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (prop.PropertyType == typeof(DbSet<TEntity>))
                    return (DbSet<TEntity>) prop.GetValue(context);
            }
            return null!; // mandatory so "all paths return"
        }*/

        [TestInitialize]
        public void TestSetup()
        {
            Factory = new SecretSantaWebApplicationFactory();

            using ApplicationDbContext context = Factory.GetDbContext();
            context.Database.EnsureCreated();

            Client = Factory.CreateClient();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Factory.Dispose();
        }

        /*[TestMethod]
        public async Task Get_FetchesAllItems()
        {
            TService service = new TService();
            service.Items.Add(CreateEntity());
            service.Items.Add(CreateEntity());
            service.Items.Add(CreateEntity());

            BaseApiController<TDto, TInputDto> controller = CreateController(service);

            IEnumerable<TDto> items = await controller.Get();
            CollectionAssert.AreEqual(service.Items.ToList(), items.ToList());
        }*/

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
            TEntity entity = CreateEntity();
            service.Items.Add(entity);
            BaseApiController<TDto, TInputDto> controller = CreateController(service);

            IActionResult result = await controller.Get(entity.Id);

            var okResult = result as OkObjectResult;
            var entityDto = Mapper.Map<TEntity, TDto>(entity);

            PropertyInfo[] props = typeof(TDto).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                Assert.AreEqual(prop.GetValue(entityDto), prop.GetValue(okResult?.Value));
            }
        }

        [TestMethod]
        public async Task Put_UpdatesItem()
        {
            TService service = new TService();
            TEntity entity1 = CreateEntity();
            service.Items.Add(entity1);
            TEntity entity2 = CreateEntity();
            BaseApiController<TDto, TInputDto> controller = CreateController(service);
            TDto dto = Mapper.Map<TEntity, TDto>(entity2);

            ActionResult<TDto?> result = await controller.Put(entity1.Id, dto);

            PropertyInfo[] props = typeof(TDto).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                Assert.AreEqual(prop.GetValue(dto), prop.GetValue(result?.Value));
            }
            // will look back into. with all this DTo stuff I need to decide what to check
            //Assert.AreEqual(entity2, service.Items.Single());
        }

        /*[TestMethod]
        public async Task Post_InsertsItem()
        {
            TService service = new TService();
            TEntity entity = CreateEntity();
            BaseApiController<TDto, TInputDto> controller = CreateController(service);

            TEntity? result = await controller.Post(entity);

            Assert.AreEqual(entity, result);
            Assert.AreEqual(entity, service.Items.Single());
        }*/

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
            TEntity entity = CreateEntity();
            service.Items.Add(entity);
            BaseApiController<TDto, TInputDto> controller = CreateController(service);

            IActionResult result = await controller.Delete(entity.Id);

            Assert.IsTrue(result is OkResult);
        }
    }

    public class InMemoryEntityService<TEntity, TDto, TInputDto>
        : IEntityService<TDto, TInputDto>
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
            return Task.FromResult(Mapper.Map<List<TEntity>, List<TDto>>(Items.ToList()));
        }

        public Task<TDto> FetchByIdAsync(int id)
        {
            return Task.FromResult(Mapper.Map<TEntity, TDto>(Items.FirstOrDefault(x => x.Id == id)));
        }

        public Task<TDto> InsertAsync(TInputDto input)
        {
            Items.Add(Mapper.Map<TInputDto, TEntity>(input));
            return Task.FromResult(Mapper.Map<TEntity, TDto>(Mapper.Map<TInputDto, TEntity>(input)));
        }

        public Task<TDto?> UpdateAsync(int id, TInputDto input)
        {
            TEntity entity = Mapper.Map<TInputDto, TEntity>(input);
            if (Items.FirstOrDefault(x => x.Id == id) is { } found)
            {
                Items[Items.IndexOf(found)] = entity;
                return Task.FromResult<TDto?>(Mapper.Map<TEntity, TDto>(entity));
            }
            return Task.FromResult(default(TDto));
        }
    }
}
