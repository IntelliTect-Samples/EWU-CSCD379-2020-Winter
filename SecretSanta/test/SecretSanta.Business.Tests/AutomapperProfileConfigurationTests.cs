using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Data;


namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class AutomapperProfileConfigurationTests
    {
        class MockAuthor : User
        {
            public MockAuthor(int id, string firstName, string lastName) :
                base(firstName, lastName)
            {
                base.Id = id;
            }
        }
      
        [TestMethod]
        public void Map_Author_SuccessWithNoIdMapped()
        {
            (User source, User target) = (
                new MockAuthor(42, "Inigo", "Montoya"), new MockAuthor(0, "Invalid", "Invalid"));
            var mapper = AutomapperProfileConfiguration.CreateMapper();
            mapper.Map(source, target);
            Assert.AreNotEqual<int?>(source.Id, target.Id);
            Assert.AreEqual<string>(source.LastName, target.LastName);
        }

    }
}
