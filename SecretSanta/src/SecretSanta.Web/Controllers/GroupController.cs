﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Api;

namespace SecretSanta.Web.Controllers
{
    public class GroupController : Controller
    {
        public IHttpClientFactory ClientFactory { get; }

        public GroupController(IHttpClientFactory clientFactory)
        {
            if (clientFactory is null)
            {
                throw new ArgumentNullException(nameof(clientFactory));
            }

            ClientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

            var client = new GroupClient(httpClient);
            ICollection<Group> groups = await client.GetAllAsync();
            return View(groups);
        }
    }
}