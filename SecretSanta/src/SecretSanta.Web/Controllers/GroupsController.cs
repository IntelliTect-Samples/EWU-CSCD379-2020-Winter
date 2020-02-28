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

        public async Task<IActionResult> Index()
        {
            ICollection<Group> groups = await Client.GetAllAsync();
            return View(groups);
        }

        public async Task<ActionResult> Details(int id)
        {
            Group group = await Client.GetAsync(id);

            return View(group);
        }

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
                var createdUser = await Client.PostAsync(groupInput);

                result = RedirectToAction(nameof(Index));
            }

            return result;
        }

        public async Task<ActionResult> Edit(int id)
        {
            var fetchedGift = await Client.GetAsync(id);

            return View(fetchedGift);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, GroupInput groupInput)
        {
            ActionResult result = null!;

            if (ModelState.IsValid)
            {
                var updatedGift = await Client.PutAsync(id, groupInput);

                result = RedirectToAction(nameof(Index));
            }
            else
            {
                var group = new Group
                {
                    Id = id,
                    Title = groupInput.Title
                };

                result = View(group);
            }

            return result;
        }

        public async Task<ActionResult> Delete(int id)
        {
            var fetchedGroup = await Client.GetAsync(id);

            return View(fetchedGroup);
        }
        [HttpPost]
        public async Task<ActionResult> Delete(Group group)
        {
            ActionResult result = null!;
            if (group != null)
            {
                await Client.DeleteAsync(group.Id);

                result = RedirectToAction(nameof(Index));
            }
            else
            {
                result = View(group);
            }

            return result;
        }
    }
}
