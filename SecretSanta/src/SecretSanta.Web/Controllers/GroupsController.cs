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
    public class GroupsController : Controller
    {
        public GroupsController(IHttpClientFactory clientFactory)
        {
            HttpClient httpClient = clientFactory?.CreateClient("SecretSantaApi") ?? throw new ArgumentNullException(nameof(clientFactory));
            Client = new GroupClient(httpClient);
        }

        private GroupClient Client { get; }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(GroupInput groupInput)
        {
            ActionResult result = View(groupInput);

            if (ModelState.IsValid)
            {
                var createdGroup = await Client.PostAsync(groupInput);
                result = RedirectToAction(nameof(Index));
            }
            return result;
        }

        public async Task<IActionResult> Index()
        {
            ICollection<Group> groups = await Client.GetAllAsync();
            return View(groups);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var fetchedGroup = await Client.GetAsync(id);

            return View(fetchedGroup);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, GroupInput groupInput)
        {
            ActionResult result = View();

            if (ModelState.IsValid)
            {
                await Client.PutAsync(id, groupInput);
                result = RedirectToAction(nameof(Index));
            }

            return result;
        }
    }
}
