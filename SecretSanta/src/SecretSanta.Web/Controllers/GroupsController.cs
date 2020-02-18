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
            //HttpClient httpClient = clientFactory?.CreateClient("SecretSantaApi") ?? throw new ArgumentNullException(nameof(clientFactory));
            //Client = new GroupClient(httpClient);

            ClientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        }

        //private GroupClient Client { get; }
        public IHttpClientFactory ClientFactory { get; }

        public async Task<ActionResult> Index()
        {
            HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

            var client = new GroupClient(httpClient);
            ICollection<Group> groups = await client.GetAllAsync();
            return View(groups);
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
                HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

                var client = new GroupClient(httpClient);
                var createdGroup = await client.PostAsync(groupInput);

                result = RedirectToAction(nameof(Index));
            }

            return result;
        }

        public async Task<ActionResult> Edit(int id)
        {
            HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

            var client = new GroupClient(httpClient);

            ActionResult result = View(id);

            //if (ModelState.IsValid)
            //{
            //    var fetchedGift = await client.GetAsync(id);
            //    result = View(fetchedGift);
            //}

            return result;
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, GroupInput groupInput)
        {
            HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

            var client = new GroupClient(httpClient);
            var updatedGroup = await client.PutAsync(id, groupInput);

            return RedirectToAction(nameof(Index));
        }
    }
}
