using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Api;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecretSanta.Web.Controllers
{

    public class UsersController : Controller
    {

        public UsersController(IHttpClientFactory clientFactory)
        {
            var httpClient = clientFactory?.CreateClient("SecretSantaApi")
                          ?? throw new ArgumentNullException(nameof(clientFactory));
            Client = new UserClient(httpClient);
        }

        private UserClient Client { get; }

        public async Task<IActionResult> Index()
        {
            var users = await Client.GetAllAsync();
            return View(users);
        }

    }

}
