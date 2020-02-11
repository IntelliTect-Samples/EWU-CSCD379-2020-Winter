﻿using SecretSanta.Web.Api;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SecretSanta.Web.Controllers
{
    public class UserController : Controller
    {
        public UserController(IHttpClientFactory clientFactory)
        {
            if (clientFactory is null)
            {
                throw new System.ArgumentNullException(nameof(clientFactory));
            }

            ClientFactory = clientFactory;
        }

        public IHttpClientFactory ClientFactory { get; }

        // GET: User
        public async Task<ActionResult> Index()
        {
            HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

            var client = new UserClient(httpClient);
            ICollection<User> users = await client.GetAllAsync();
            return View(users);
        }
    }
}