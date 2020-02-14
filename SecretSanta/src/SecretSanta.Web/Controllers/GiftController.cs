﻿using System.Net.Http;
using System.Threading.Tasks;
using SecretSanta.Web.Api;
using Microsoft.AspNetCore.Mvc;


namespace SecretSanta.Web.Controllers
{
    public class GiftController : Controller
    {
        public IHttpClientFactory ClientFactory { get; }

        public GiftController(IHttpClientFactory clientFactory)
        {
            if (clientFactory is null) {
                throw new System.ArgumentNullException(nameof(clientFactory));
            }
            ClientFactory = clientFactory;
        }

        public async Task<ActionResult> Index() => View(await new GiftClient(ClientFactory.CreateClient("SecretSantaApi")).GetAllAsync());
    }
}