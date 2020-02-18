using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Api;
using System;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecretSanta.Web.Controllers
{
    public class GroupsController : Controller
    {
        public IHttpClientFactory ClientFactory { get; }

        public GroupsController(IHttpClientFactory clientFactory) =>
            ClientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));

        public async Task<IActionResult> Index() =>
            View(
                await new GroupClient(
                    ClientFactory.CreateClient("SecretSantaApi")
                ).GetAllAsync()
            );

        public ActionResult Create() => View();

        [HttpPost]
        public async Task<ActionResult> Create(GroupInput groupInput)
        {
            ActionResult result = View(groupInput);

            if (ModelState.IsValid)
            {
                HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

                var client = new GroupClient(httpClient);
                var createdgroup = await client.PostAsync(groupInput);

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

                var client = new GroupClient(httpClient);
                var fetchedGroup = await client.GetAsync(id);

                result = View(fetchedGroup);
            }

            return result;
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, GroupInput groupInput)
        {
            ActionResult result = View();

            if (ModelState.IsValid)
            {
                HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

                var client = new GroupClient(httpClient);
                var updatedGroup = await client.PutAsync(id, groupInput);

                result = RedirectToAction(nameof(Index));
            }

            return result;
        }
    }
}
