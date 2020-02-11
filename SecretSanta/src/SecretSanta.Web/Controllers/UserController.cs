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

        IHttpClientFactory ClientFactory { get; set; }
        public UserController(IHttpClientFactory clientFactory)
        {

            if (clientFactory is null)
            {
                throw new ArgumentNullException(nameof(clientFactory));
            }
            ClientFactory = clientFactory;
        }
        //Get: User
        public async Task<IActionResult> Index()
        {
            HttpClient client = ClientFactory.CreateClient("SecretSantaApi");
            var userClient = new UserClient(client);
            var users = await userClient.GetAllAsync();
            return View(users);
        }

    }

}
