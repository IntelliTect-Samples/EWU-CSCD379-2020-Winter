using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Api;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SecretSanta.Web.Controllers
{
    public class GiftsController : Controller
    {
        public GiftsController(IHttpClientFactory clientFactory)
        {
            HttpClient httpClient = clientFactory?.CreateClient("SecretSantaApi") ?? throw new ArgumentNullException(nameof(clientFactory));
            Client = new GiftClient(httpClient);
        }

        private GiftClient Client { get; }

        public async Task<IActionResult> Index()
        {
            ICollection<Gift> gifts = await Client.GetAllAsync();
            return View(gifts);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Gift gift)
        {
            var createdGift = await Client.PostAsync(gift);
            return RedirectToAction(nameof(Index)); 
        }

        public async Task<ActionResult> Edit(int id)
        {
            var returnedGift = await Client.GetAsync(id);
            return View(returnedGift);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, GiftInput giftInput)
        {
            var updatedGift = await Client.PutAsync(id, giftInput);
            return RedirectToAction(nameof(Index));
        }
    }
}