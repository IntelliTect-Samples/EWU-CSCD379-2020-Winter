using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SecretSanta.Data.Tests
{

    [TestClass]
    public class UserGroupRelationTests : TestBase
    {

        [TestMethod]
        public async Task UserGroupRelation_DbSetsGetsAllProps()
        {
            int id;
            await using (var context = new ApplicationDbContext(Options, Accessor))
            {
                var group = new Group {Name = "Group A"};
                context.Groups.Add(group);

                var user = new User {Id = 1, FirstName = "Brett", LastName = "Henning"};
                context.Users.Add(user);

                var gift = new Gift {Title = "Title", Description = "Description", Url = "URL", User = user};
                context.Gifts.Add(gift);

                await context.SaveChangesAsync();
                id = gift.Id;
            }

            await using (var context = new ApplicationDbContext(Options, Accessor))
            {
                var gifts = await context.Gifts.Where(g => g.Id == id)
                                         .Include(g => g.User)
                                         .ThenInclude(u => u.Relations)
                                         .ThenInclude(r => r.Group)
                                         .SingleOrDefaultAsync();

                Assert.IsNotNull(gifts);
                Assert.AreEqual(1, gifts.User.Relations.Count);

                var relation = gifts.User.Relations[0];
                Assert.AreEqual(relation.Group.Name, "Group A");
                Assert.AreEqual(relation.UserId, 1);
                Assert.AreEqual(relation.User.FirstName, "Brett");
            }
        }

    }

}
