using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Api;

namespace SecretSanta.Web.Controllers
{
    public class UserController : Controller
    {
        public IHttpClientFactory ClientFactory { get; }

        public UserController(IHttpClientFactory clientFactory)
        {
            if (clientFactory is null)
                throw new ArgumentNullException(nameof(clientFactory));

            ClientFactory = clientFactory;
        }

        public async Task<ActionResult> IndexAsync()
        {
            HttpClient httpClient = ClientFactory.CreateClient("SantaApi");
            UserClient client = new UserClient(httpClient);

            ICollection<User> users = await client.GetAllAsync();
            return View(users);
        }
    }
}