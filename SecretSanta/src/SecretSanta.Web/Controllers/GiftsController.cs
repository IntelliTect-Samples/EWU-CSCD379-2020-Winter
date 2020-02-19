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
            //HttpClient httpClient = clientFactory?.CreateClient("SecretSantaApi") ?? throw new ArgumentNullException(nameof(clientFactory));
            //Client = new GiftClient(httpClient);


            ClientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        }

        public IHttpClientFactory ClientFactory { get; }

        //private GiftClient Client { get; }

        public async Task<ActionResult> Index()
        {
            //ICollection<Gift> gifts = await Client.GetAllAsync();
            //return View(gifts);

            HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

            var client = new GiftClient(httpClient);
            ICollection<Gift> gifts = await client.GetAllAsync();
            return View(gifts);
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
                HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

                var client = new GiftClient(httpClient);
                var createdAuthor = await client.PostAsync(giftInput);

                result = RedirectToAction(nameof(Index));
            }

            return result;
        }

        public async Task<ActionResult> Edit(int id)
        {
            HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

            var client = new GiftClient(httpClient);
            var fetchedGift = await client.GetAsync(id);

            return View(fetchedGift);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, GiftInput giftInput)
        {
            HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

            var client = new GiftClient(httpClient);
            var updatedAuthor = await client.PutAsync(id, giftInput);

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete(int id)
        {
            HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

            var client = new GiftClient(httpClient);

            await client.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}