﻿using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using System.Diagnostics;
using AutoMapper;
using System.Collections.Generic;
using static SampleData;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GroupServiceTest : TestBase
    {
        // MethodBeingTested_ConditionBeingTested_WhatWeExpectedToHappen
        [TestMethod]
        public async Task CreateNewGroupInDbContext_ShouldBeSavedToDatabase_ExpectingUpdatedDbData()
        {
            //arrange
            using var dbContext = new ApplicationDbContext(Options);
            var mapper = AutoMapperProfileConfiguration.CreateMapper();
            var groupService = new GroupService(dbContext, mapper);
            var sampleGroup1 = SampleData.CreateGroup1();
            var sampleGroup2 = SampleData.CreateGroup2();
            await groupService.InsertAsync(sampleGroup1);
            await groupService.InsertAsync(sampleGroup2);
            

            //act
            using var dbContextFetch = new ApplicationDbContext(Options);
            var fetchedGroup1 = await dbContextFetch.Groups.SingleAsync(item => item.Id == sampleGroup1.Id);
            const string newTitle = "updated_title";
            fetchedGroup1.Title = newTitle;
            await groupService.UpdateAsync(sampleGroup2.Id!, fetchedGroup1);

            //assert
            using var dbContextAssert = new ApplicationDbContext(Options);
            fetchedGroup1 = await dbContextAssert.Groups.SingleAsync(item => item.Id == fetchedGroup1.Id);
            var fetchedGroup2 = await dbContextAssert.Groups.SingleAsync(item => item.Id == 2);
            Assert.AreEqual(newTitle, fetchedGroup2.Title);
            Assert.AreEqual(sampleGroup1.Title, fetchedGroup1.Title);
        }


        [TestMethod]
        public async Task InsertGroup_GroupPropertiesCorrectAndIdSet_()
        {
            //arrange
            using var dbContext = new ApplicationDbContext(Options);
            var mapper = AutoMapperProfileConfiguration.CreateMapper();
            var groupService = new GroupService(dbContext, mapper);
            var sampleGroup1 = SampleData.CreateGroup1();

            var notInsertedGroup = new Group();


            Trace.WriteLine("sampleId before insert: " + sampleGroup1.Id);
            //act
            Group insertedGroup = await groupService.InsertAsync(sampleGroup1);

            Trace.WriteLine("sampleId after insert: " + sampleGroup1.Id);
            //assert
            Assert.AreEqual<string>(sampleGroup1.Title, insertedGroup.Title);
            Assert.IsTrue(sampleGroup1.Id != notInsertedGroup.Id);

        }


        [TestMethod]
        public async Task AddUsers()
        {
            //arrange
            using var dbContext = new ApplicationDbContext(Options);
            var mapper = AutoMapperProfileConfiguration.CreateMapper();
            var groupService = new GroupService(dbContext, mapper);
            var sampleGroup1 = SampleData.CreateGroup1();

            var notInsertedGroup = new Group();


            Trace.WriteLine("sampleId before insert: " + sampleGroup1.Id);
            //act
            Group insertedGroup = await groupService.InsertAsync(sampleGroup1);

            Trace.WriteLine("sampleId after insert: " + sampleGroup1.Id);
            //assert
            Assert.AreEqual<string>(sampleGroup1.Title, insertedGroup.Title);
            Assert.IsTrue(sampleGroup1.Id != notInsertedGroup.Id);

        }





        [TestMethod]
        public async Task DeleteGroupFromGroupService_ByAsyncId_ThenDeleteAgainExpectedFalse()
        {
            //arrange
            using var dbContext = new ApplicationDbContext(Options);

            var mapper = AutoMapperProfileConfiguration.CreateMapper();
            var groupService = new GroupService(dbContext, mapper);
            await groupService.InsertAsync(CreateGroup1());

            //act & assert
            Assert.IsTrue(await groupService.DeleteAsync(1));
            Assert.IsFalse(await groupService.DeleteAsync(1));
        }

        [TestMethod]
        public async Task InsertGroupListIntoGroupService_ByFetchingAllAsync_ExpectingSameCountAndNotNull()
        {
            //arrange

            using (var dbContext = new ApplicationDbContext(Options))
            {
                //setup
                var mapper = AutoMapperProfileConfiguration.CreateMapper();

                GroupService groupService = new GroupService(dbContext, mapper);

                List<Group> groups = new List<Group>();

                int numberOfGroups = 3;

                for (int i = 0; i < numberOfGroups; i++)
                {
                    groups.Add(CreateGroup1());

                }

                Group[] result = await groupService.InsertAsync(groups.ToArray());



                //assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Length == numberOfGroups);





            }
        }

        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public async Task UpdateGroupProperty_WithInvalidId_ThrowsException()
        {
            //arrange
            using var dbContext = new ApplicationDbContext(Options);
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Group, Group>();
            }).CreateMapper();
            var groupService = new GroupService(dbContext, mapper);
            var sampleGroup1 = CreateGroup1();
            var sampleGroup2 = CreateGroup2();
            await groupService.InsertAsync(sampleGroup1);
            await groupService.InsertAsync(sampleGroup2);

            //act
            sampleGroup2 = await groupService.FetchByIdAsync(2);
            sampleGroup2.Title = "updated_title";
            await groupService.UpdateAsync(1, sampleGroup2);

            //assert
            // (check method attribute)
        }


        [TestMethod]

        public async Task GroupService_Created_With_Mapper_That_Doesnt_Map_Id_No_EF_Exception_On_UpdateAsync()
        {

            using (var dbContext = new ApplicationDbContext(Options))
            {

                //setup
                var mapper = AutoMapperProfileConfiguration.CreateMapper();

                GroupService groupService = new GroupService(dbContext, mapper);


                var sampleGroup = CreateGroup1();

                var sampleGroup2 = CreateGroup2();

                await groupService.InsertAsync(sampleGroup);

                await groupService.InsertAsync(sampleGroup2);



                //act
                sampleGroup2 = await groupService.FetchByIdAsync(2);

                sampleGroup2.Title = "New Title";
                //assert

                await groupService.UpdateAsync(1, sampleGroup2);



            }


        }
    }
}
