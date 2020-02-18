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
            HttpClient httpClient = clientFactory?.CreateClient("SecretSantaApi") ?? throw new ArgumentNullException(nameof(clientFactory));
            Client = new UserClient(httpClient);
        }

        private UserClient Client { get; }
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
                var createdUser = await Client.PostAsync(userInput);
                result = RedirectToAction(nameof(Index));
            }
            return result;
        }

        public async Task<IActionResult> Index()
        {
            ICollection<User> users = await Client.GetAllAsync();
            return View(users);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var fetchedUser = await Client.GetAsync(id);

            return View(fetchedUser);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, UserInput userInput)
        {
            ActionResult result = View();

            if (ModelState.IsValid)
            {
                await Client.PutAsync(id, userInput);
                result = RedirectToAction(nameof(Index));
            }

            return result;
        }
    }
}
