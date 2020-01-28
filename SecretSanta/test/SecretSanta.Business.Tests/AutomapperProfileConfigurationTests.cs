using AutoMapper;
using BlogEngine.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace BlogEngine.Business.Tests
{
    [TestClass]
    public class AutomapperProfileConfigurationTests
    {
        class MockAuthor : Author
        {
            public MockAuthor(int id, string firstName, string lastName) :
                base(firstName, lastName, "email@address.com")
            {
                base.Id = id;
            }
        }
        class MockTag : Tag
        {
            public MockTag(int id, string name) :
                base(name)
            {
                base.Id = id;
            }
        }
        [TestMethod]
        public void Map_Author_SuccessWithNoIdMapped()
        {
            (Author source, Author target) = (
                new MockAuthor(42, "Inigo", "Montoya"), new MockAuthor(0, "Invalid", "Invalid"));
            IMapper mapper = AutomapperProfileConfiguration.CreateMapper();
            mapper.Map(source, target);
            Assert.AreNotEqual<int?>(source.Id, target.Id);
            Assert.AreEqual<string>(source.LastName, target.LastName);
        }

        [TestMethod]
        public void Map_Tag_SuccessWithNoIdMapped()
        {
            (Tag source, Tag target) = (
                new MockTag(42, "<tag>"), new MockTag(0, "Invalid"));
            IMapper mapper = AutomapperProfileConfiguration.CreateMapper();
            mapper.Map(source, target);
            Assert.AreNotEqual<int?>(source.Id, target.Id);
            Assert.AreEqual<string>(source.Name, target.Name);
        }
    }
}
