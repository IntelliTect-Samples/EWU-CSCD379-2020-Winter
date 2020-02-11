using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public abstract class BaseApiControllerTests<TDto, TInputDto, TService>
        where TDto : class, TInputDto, Business.Dto.IEntity
        where TInputDto : class
        where TService : class, IEntityService<TDto, TInputDto>
    {
        protected abstract BaseApiController<TDto, TInputDto> CreateController(TService service);
        private IMapper Mapper { get; } = AutomapperConfigurationProfile.CreateMapper();
      //  protected abstract TDto CreateEntity();
        protected abstract TDto CreateDto();

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_RequiresService()
        {
            new ThrowingController();
        }

        [TestMethod]
        public async Task Get_FetchesAllItems()
        {
            Mock<TService> service = new Mock<TService>();
            //service.Items.Add(CreateDto());
            //service.Items.Add(CreateDto());
            //service.Items.Add(CreateDto());

            BaseApiController<TDto, TInputDto> controller = CreateController(service.Object);

            IEnumerable<TDto> items = await controller.Get();

          //  CollectionAssert.AreEqual(service.Items.ToList(), items.ToList());
        }

        [TestMethod]
        public async Task Get_WhenEntityDoesNotExist_ReturnsNotFound()
        {
            Mock<TService> service = new Mock<TService>();
            BaseApiController<TDto, TInputDto> controller = CreateController(service.Object);

            IActionResult result = await controller.Get(1);

            Assert.IsTrue(result is NotFoundResult);
        }


        [TestMethod]
        public async Task Get_WhenEntityExists_ReturnsItem()
        {
            Mock<TService> service = new Mock<TService>();
            TDto entity = CreateDto();
            service.Setup(service => service.FetchByIdAsync(42))
                .Returns(Task.FromResult(entity));
            BaseApiController<TDto, TInputDto> controller = CreateController(service.Object);

            IActionResult result = await controller.Get(42);

            var okResult = result as OkObjectResult;
            
            Assert.AreEqual(entity, okResult?.Value);
        }

        [TestMethod]
        public async Task Put_UpdatesItem()
        {
            Mock<TService> service = new Mock<TService>();
            TDto entity1 = CreateDto();
            TDto entity2 = CreateDto();
            TInputDto entityInput = Mapper.Map<TDto, TInputDto>(entity2);

            service.Setup(service => service.UpdateAsync(entity1.Id, entityInput))
                .Returns(Task.FromResult<TDto?>(entity2));
            
            BaseApiController<TDto, TInputDto> controller = CreateController(service.Object);

            ActionResult<TDto?> result = await controller.Put(entity1.Id, entityInput);

            Assert.IsTrue(result.Result is OkObjectResult);
            OkObjectResult ok = (OkObjectResult)result.Result;
            Assert.AreEqual(entity2, ok.Value);
        }

        [TestMethod]
        public async Task Post_InsertsItem()
        {
            Mock<TService> service = new Mock<TService>();
            TDto entity = CreateDto();
            TInputDto entityInput = Mapper.Map<TDto, TInputDto>(entity);
            service.Setup(service => service.InsertAsync(entityInput))
                .Returns(Task.FromResult(entity));
            BaseApiController<TDto, TInputDto> controller = CreateController(service.Object);

            TDto result = await controller.Post(entityInput);

            Assert.AreEqual(entity, result);
        }

        [TestMethod]
        public async Task Delete_WhenItemDoesNotExist_ReturnsNotFound()
        {
            Mock<TService> service = new Mock<TService>();
            BaseApiController<TDto, TInputDto> controller = CreateController(service.Object);

            IActionResult result = await controller.Delete(1);

            Assert.IsTrue(result is NotFoundResult);
        }

        [TestMethod]
        public async Task Delete_WhenItemExists_ReturnsOk()
        {
            Mock<TService> service = new Mock<TService>();
            TDto entity = CreateDto();
            service.Setup(service => service.DeleteAsync(entity.Id))
                .Returns(Task.FromResult(true));
            BaseApiController<TDto, TInputDto> controller = CreateController(service.Object);

            IActionResult result = await controller.Delete(entity.Id);

            Assert.IsTrue(result is OkResult);
        }

        private class ThrowingController : BaseApiController<TDto, TInputDto>
        {
            public ThrowingController() : base(null!)
            { }
        }
    }

//    public class InMemoryEntityService<TEntity> : IEntityService<TEntity> where TEntity : EntityBase
        

//    {
//        public IList<TEntity> Items { get; } = new List<TEntity>();

//        public Task<bool> DeleteAsync(int id)
//        {
//            if (Items.FirstOrDefault(x => x.Id == id) is { } found)
//            {
//                return Task.FromResult(Items.Remove(found));
//            }
//            return Task.FromResult(false);
//        }

//        public Task<List<TDto>> FetchAllAsync()
//        {
//            return Task.FromResult(Items.ToList());
//        }

//        public Task<TDto> FetchByIdAsync(int id)
//        {
//            return Task.FromResult(Items.FirstOrDefault(x => x.Id == id));
//        }

//        public Task<TDto> InsertAsync(TInputDto entityInput)
//        {
//            TEntity entity = Mapper.Map<TInputDto, TEntity>(entityInput);
//            Items.Add(entity);
//            return Task.FromResult(Mapper.Map<TEntity, TDto>(entity));
//        }

//public Task<TDto?> UpdateAsync(int id, TInputDto entity)
//{
//    if (Items.FirstOrDefault(x => x.Id == id) is { } found)
//    {
//        Items[Items.IndexOf(found)] = entity;
//        return Task.FromResult<TDto?>(entity);
//    }
//    return Task.FromResult(default(TDto));
//}

//public Task<TDto> UpdateAsync(int id, TInputDto entity)
//{
//    throw new NotImplementedException();
//}
//      }
}
