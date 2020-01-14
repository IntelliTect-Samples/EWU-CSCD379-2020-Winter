using System;
using Xunit;

namespace SecretSanta.Business.Tests
{
    public class GiftTests
    {
        [Fact]
        public void Ctor_Id_RetainsValue()
        {
            int id = 42;

            var gift = new Gift(id, "Title", "Description", "Url", new User(0, "FirstName", "LastName"));

            Assert.Equal<int>(id, gift.Id);
        }

        [Fact]
        public void Ctor_Title_RetainsValue()
        {
            string title = "Title";

            var gift = new Gift(0, title, "Description", "Url", new User(0, "FirstName", "LastName"));

            Assert.Equal(title, gift.Title);
        }

        [Fact]
        public void Ctor_Description_RetainsValue()
        {
            string desc = "Description";

            var gift = new Gift(0, "Title", desc, "Url", new User(0, "FirstName", "LastName"));

            Assert.Equal(desc, gift.Description);
        }

        [Fact]
        public void Ctor_Url_RetainsValue()
        {
            string url = "Url";

            var gift = new Gift(0, "Title", "Description", url, new User(0, "FirstName", "LastName"));

            Assert.Equal(url, gift.Url);
        }

        [Fact]
        public void Ctor_User_RetainsValue()
        {
            User user = new User(0, "FirstName", "LastName");

            var gift = new Gift(0, "Title", "Description", "Url", user);

            Assert.True(ReferenceEquals(user, gift.User));
        }

        [Fact]
        public void Ctor_NullTitle_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Gift(0, null!, "Description", "Url", new User(0, "FirstName", "LastName")));

        [Fact]
        public void Ctor_NullDescription_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Gift(0, "Title", null!, "Url", new User(0, "FirstName", "LastName")));

        [Fact]
        public void Ctor_NullUrl_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Gift(0, "Title", "Description", null!, new User(0, "FirstName", "LastName")));

        [Fact]
        public void Ctor_NullUser_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new Gift(0, "Title", "Description", "Url", null!));
    }
}
