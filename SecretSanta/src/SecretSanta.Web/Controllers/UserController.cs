using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Api;

namespace SecretSanta.Web.Controllers
{

    public class UserController : Controller
    {

        private IHttpClientFactory ClientFactory { get; }

        public UserController(IHttpClientFactory clientFactory)
        {
            ClientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        }

        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

            var               client = new UserClient(httpClient);
            ICollection<User> users  = await client.GetAllAsync();
            return View(users);
        }

    }

}
