using System;
using System.Collections.Generic;
using System.Linq;
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
                throw new ArgumentNullException(nameof(clientFactory));

            ClientFactory = clientFactory;
        }

        public async Task<ActionResult> IndexAsync()
        {
            HttpClient httpClient = ClientFactory.CreateClient("SantaApi");
            GroupClient client = new GroupClient(httpClient);

            ICollection<Group> users = await client.GetAllAsync();
            return View(users);
        }
    }
}