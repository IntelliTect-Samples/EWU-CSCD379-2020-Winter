using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Api;

namespace SecretSanta.Web.Controllers
{
    public class GroupController : Controller
    {
        public GroupController(IHttpClientFactory clientFactory)
        {
            if (clientFactory is null)
            {
                throw new System.ArgumentNullException(nameof(clientFactory));
            }

            ClientFactory = clientFactory;
        }

        public IHttpClientFactory ClientFactory { get; }

        // GET: Group
        public async Task<ActionResult> Index()
        {
            HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

            var client = new GroupClient(httpClient);
            ICollection<Group> groups = await client.GetAllAsync();
            return View(groups);
        }
    }
}