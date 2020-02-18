using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Api;
using System;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecretSanta.Web.Controllers
{
    public class UsersController : Controller
    {
        public IHttpClientFactory ClientFactory { get; }

        public UsersController(IHttpClientFactory clientFactory) =>
            ClientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));

        public async Task<IActionResult> Index() =>
            View(
                await new UserClient(
                    ClientFactory.CreateClient("SecretSantaApi")
                ).GetAllAsync()
            );

        public ActionResult Create() => View();

        [HttpPost]
        public async Task<ActionResult> Create(UserInput userInput)
        {
            ActionResult result = View(userInput);

            if (ModelState.IsValid)
            {
                HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

                var client = new UserClient(httpClient);
                var createduser = await client.PostAsync(userInput);

                result = RedirectToAction(nameof(Index));
            }

            return result;
        }

        public async Task<ActionResult> Edit(int id)
        {
            ActionResult result = View(id);

            if (ModelState.IsValid)
            {
                HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

                var client = new UserClient(httpClient);
                var fetchedUser = await client.GetAsync(id);

                result = View(fetchedUser);
            }

            return result;
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, UserInput userInput)
        {
            ActionResult result = View();

            if (ModelState.IsValid)
            {
                HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

                var client = new UserClient(httpClient);
                var updatedUser = await client.PutAsync(id, userInput);

                result = RedirectToAction(nameof(Index));
            }

            return result;
        }
    }
}
