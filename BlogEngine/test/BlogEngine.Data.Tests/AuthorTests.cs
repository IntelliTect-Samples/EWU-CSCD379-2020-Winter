using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogEngine.Data.Tests
{
    [TestClass]
    public class AuthorTests : TestBase
    {
        [NotNull]
        [DisallowNull]
        public TestContext? TestContext { get; set; }

        bool TryParse(int number,[NotNullWhen(true)] out string? digitName)
        {
            if (TryParse(4, out string? digitText))
            {

            }

            
        }

        Author? GetAuthor(int id)
        {
            if(GetAuthor(42) is Author author)
            {
                // use author;
            }
            return null;
        }


        [TestMethod]
        public async Task CreateAuthor_ShouldSaveIntoDatabase()
        {
            int? authorId = null;
            // Arrange
            using (var applicationDbContext = new ApplicationDbContext(Options))
            {
                var author = SampleData.CreateInigoMontoya();
                applicationDbContext.Authors.Add(author);

                var author2 = SampleData.CreateInigoMontoya();

                applicationDbContext.Authors.Add(author2);

                await applicationDbContext.SaveChangesAsync().ConfigureAwait(false);

                authorId = author.Id;
            }

            // Act
            // Assert
            using (var applicationDbContext = new ApplicationDbContext(Options))
            {
                Author author = 
                    await applicationDbContext.Authors.Where(a => a.Id == authorId).SingleOrDefaultAsync();

                Assert.IsNotNull(author);
                Assert.AreEqual(SampleData.Inigo, author.FirstName);
            }
        }

        [TestMethod]
        public async Task CreateAuthor_ShouldSetFingerPrintDataOnInitialSave()  
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "imontoya"));

            int? authorId;
            // Arrange
            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var author = SampleData.CreateInigoMontoya();
                applicationDbContext.Authors.Add(author);

                var author2 = SampleData.CreateInigoMontoya();
                applicationDbContext.Authors.Add(author2);

                await applicationDbContext.SaveChangesAsync();

                authorId = author.Id;
            }

            // Act
            // Assert
            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var author = await applicationDbContext.Authors.Where(a => a.Id == authorId).SingleOrDefaultAsync();

                Assert.IsNotNull(author);
                Assert.AreEqual("imontoya", author.CreatedBy);
                Assert.AreEqual("imontoya", author.ModifiedBy);
                Assert.AreEqual("Inigo", author.FirstName);

            }
        }

        [TestMethod]
        public async Task CreateAuthor_ShouldSetFingerPrintDataOnUpdate()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "imontoya"));

            int? authorId;
            // Arrange
            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var author = new Author("Inigo", "Montoya", "inigo@montoya.me");

                applicationDbContext.Authors.Add(author);

                var author2 = new Author("Inigo", "Montoya", "inigo@montoya.me");

                applicationDbContext.Authors.Add(author2);

                await applicationDbContext.SaveChangesAsync();

                authorId = author.Id;
            }

            // Act
            // change the user that is updating the record
            httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "pbuttercup"));
            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                // Since we are pulling back the record from the database and making changes to it, we don't need to re-add it to the collection
                // thus no Authors.Add call, that is only needed when new records are inserted
                var author = await applicationDbContext.Authors.Where(a => a.Id == authorId).SingleOrDefaultAsync();
                author.FirstName = "Princess";
                author.LastName = "Buttercup";

                await applicationDbContext.SaveChangesAsync();
            }
            // Assert
            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var author = await applicationDbContext.Authors.Where(a => a.Id == authorId).SingleOrDefaultAsync();

                Assert.IsNotNull(author);
                Assert.AreEqual("imontoya", author.CreatedBy);
                Assert.AreEqual("pbuttercup", author.ModifiedBy);
            }
        }
    }
}
