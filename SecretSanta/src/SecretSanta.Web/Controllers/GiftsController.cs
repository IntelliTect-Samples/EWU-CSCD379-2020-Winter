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

        public IHttpClientFactory ClientFactory { get; }

        private GiftClient Client { get; }

        public ActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> Index()
        {
            ICollection<Gift> gifts = await Client.GetAllAsync();
            return View(gifts);
        }

        [HttpPost]
        public async Task<ActionResult> Create(GiftInput giftInput)
        {
            ActionResult result = View(giftInput);

            if (ModelState.IsValid)
            {
                var createdGift = await Client.PostAsync(giftInput);
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
        public async Task<ActionResult> Edit(int id, GiftInput giftInput)
        {
            ActionResult result = View();

            if (ModelState.IsValid)
            {
                await Client.PutAsync(id, giftInput);
                result = RedirectToAction(nameof(Index));
            }

            return result;
        }
    }
}