using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GiftControllerTests : BaseContollerTests
    {
        [TestMethod]
        public async Task Put_WithMissingId_NotFound()
        {
            Business.Dto.GiftInput gift = Mapper.Map<Gift, Business.Dto.GiftInput>(SampleData.Gift1);
            string jsonData = JsonSerializer.Serialize(gift);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await Client.PutAsync("api/Author/42", stringContent);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
