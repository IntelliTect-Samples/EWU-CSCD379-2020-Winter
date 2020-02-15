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

        public async Task<IActionResult> Index()
        {
            ICollection<User> users = await Client.GetAllAsync();
            return View(users);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(User user)
        {
            var createdGift = await Client.PostAsync(user);
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Edit(int id)
        {
            var returnedUser = await Client.GetAsync(id);
            return View(returnedUser);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, UserInput userInput)
        {
            var updatedUser = await Client.PutAsync(id, userInput);
            return RedirectToAction(nameof(Index));
        }
    }
}
