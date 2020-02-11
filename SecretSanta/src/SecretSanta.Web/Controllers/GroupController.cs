using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Api;

namespace SecretSanta.Web.Controllers
{

    public class GroupController : Controller
    {

        private IHttpClientFactory ClientFactory { get; }

        public GroupController(IHttpClientFactory clientFactory)
        {
            ClientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        }

        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

            var               client = new GroupClient(httpClient);
            ICollection<Group> groups  = await client.GetAllAsync();
            return View(groups);
        }

    }

}
