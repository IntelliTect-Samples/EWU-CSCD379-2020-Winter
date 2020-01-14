using System;
using Xunit;

namespace SecretSanta.Business.Tests
{
    public class UserTests
    {
        [Fact]
        public void Ctor_Id_RetainsValue()
        {
            int id = 42;

            var user = new User(id, "FirstName", "LastName");

            Assert.Equal<int>(id, user.Id);
        }

        [Fact]
        public void Ctor_FirstName_RetainsValue()
        {
            string name = "Inigo";

            var user = new User(0, name, "LastName");

            Assert.Equal(name, user.FirstName);
        }

        [Fact]
        public void Ctor_LastName_RetainsValue()
        {
            string name = "Montoya";

            var user = new User(0, "FirstName", name);

            Assert.Equal(name, user.LastName);
        }

        [Fact]
        public void Ctor_NullFirstName_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new User(0, null!, "LastName"));

        [Fact]
        public void Ctor_NullLastName_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new User(0, "FirstName", null!));
    }
}
