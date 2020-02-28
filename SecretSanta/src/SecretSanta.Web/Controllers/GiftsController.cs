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

        public async Task<ActionResult> Details(int id)
        {
            Gift gift = await Client.GetAsync(id);

            return View(gift);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(GiftInput giftInput)
        {
            ActionResult result = View(giftInput);

            if (ModelState.IsValid)
            {
                var createdUser = await Client.PostAsync(giftInput);

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
            ActionResult result = null!;

            if (ModelState.IsValid)
            {
                var updatedGift = await Client.PutAsync(id, giftInput);

                result = RedirectToAction(nameof(Index));
            }
            else
            {
                var gift = new Gift
                {
                    Id = id,
                    Description = giftInput.Description,
                    Title = giftInput.Title,
                    Url = giftInput.Url,
                    UserId = giftInput.UserId
                };

                result = View(gift);
            }

            return result;
        }

        public async Task<ActionResult> Delete(int id)
        {
            var fetchedUser = await Client.GetAsync(id);

            return View(fetchedUser);
        }
        [HttpPost]
        public async Task<ActionResult> Delete(Gift gift)
        {
            ActionResult result = null!;
            if (gift != null)
            {
                await Client.DeleteAsync(gift.Id);

                result = RedirectToAction(nameof(Index));
            }
            else
            {
                result = View(gift);
            }

            return result;
        }

        public ActionResult ListGifts()
        {
            return View();
        }
    }
}