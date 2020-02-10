using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Api;

namespace SecretSanta.Web.Controllers
{
    public class GroupController : Controller
    {
        public IHttpClientFactory ClientFactory { get; }

        public GroupController(IHttpClientFactory clientFactory)
        {
            if (clientFactory is null)
                throw new System.ArgumentNullException(nameof(clientFactory));

            ClientFactory = clientFactory;
        }
        // GET: Group
        public async Task<ActionResult> IndexAsync()
        {
            HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

            var client = new GroupClient(httpClient);
            var groups = await client.GetAllAsync();

            return View(groups);
        }
    }
}