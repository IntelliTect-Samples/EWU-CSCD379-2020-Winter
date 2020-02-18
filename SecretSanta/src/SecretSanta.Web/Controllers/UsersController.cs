using System;
using System.Collections.Generic;
using System.Linq;
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
            //HttpClient httpClient = clientFactory?.CreateClient("SecretSantaApi") ?? throw new ArgumentNullException(nameof(clientFactory));
            //Client = new UserClient(httpClient);

            ClientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        }

        //private UserClient Client { get; }
        public IHttpClientFactory ClientFactory { get; }

        public async Task<ActionResult> Index()
        {
            HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

            var client = new UserClient(httpClient);
            ICollection<User> users = await client.GetAllAsync();
            return View(users);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserInput userInput)
        {
            ActionResult result = View(userInput);

            if (ModelState.IsValid)
            {
                HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

                var client = new UserClient(httpClient);
                var createdUser = await client.PostAsync(userInput);

                result = RedirectToAction(nameof(Index));
            }

            return result;
        }

        public async Task<ActionResult> Edit(int id)
        {
            HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

            var client = new UserClient(httpClient);

            ActionResult result = View(id);

            //if (ModelState.IsValid)
            //{
            //    var fetchedGift = await client.GetAsync(id);
            //    result = View(fetchedGift);
            //}

            return result;
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, UserInput userInput)
        {
            HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

            var client = new UserClient(httpClient);
            var updatedUser = await client.PutAsync(id, userInput);

            return RedirectToAction(nameof(Index));
        }
    }
}
