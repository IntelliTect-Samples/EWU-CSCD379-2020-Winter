﻿using System;
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
        private IHttpClientFactory ClientFactory { get; }
        private const string ApiName = "SecretSantaApi";
        public GroupsController(IHttpClientFactory clientFactory)
        {
            if (clientFactory is null) throw new ArgumentNullException(nameof(clientFactory));
            ClientFactory = clientFactory;
        }


        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = ClientFactory.CreateClient(ApiName);
            ICollection<Group> groups = await new GroupClient(httpClient).GetAllAsync();
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
                HttpClient httpClient = ClientFactory.CreateClient(ApiName);

                var client = new GroupClient(httpClient);
                var createdGroup = await client.PostAsync(groupInput);

                result = RedirectToAction(nameof(Index));
            }

            return result;
        }

        public async Task<ActionResult> Edit(int id)
        {
            HttpClient httpClient = ClientFactory.CreateClient(ApiName);

            var client = new GroupClient(httpClient);
            var fetchedGroup = await client.GetAsync(id);

            return View(fetchedGroup);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, GroupInput groupInput)
        {
            HttpClient httpClient = ClientFactory.CreateClient(ApiName);

            var client = new GroupClient(httpClient);
            var updatedGroup = await client.PutAsync(id, groupInput);

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete(int id)
        {
            HttpClient httpClient = ClientFactory.CreateClient(ApiName);

            var client = new GroupClient(httpClient);

            await client.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
