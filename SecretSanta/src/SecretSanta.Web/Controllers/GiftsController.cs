using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Api;
using System;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecretSanta.Web.Controllers
{
    public class GiftsController : Controller
    {
        public IHttpClientFactory ClientFactory { get; }

        public GiftsController(IHttpClientFactory clientFactory) =>
            ClientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));

        public async Task<IActionResult> Index() =>
            View(
                await new GiftClient(
                    ClientFactory.CreateClient("SecretSantaApi")
                ).GetAllAsync()
            );

        public ActionResult Create() => View();

        [HttpPost]
        public async Task<ActionResult> Create(GiftInput giftInput)
        {
            ActionResult result = View(giftInput);

            if (ModelState.IsValid)
            {
                HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

                var client = new GiftClient(httpClient);
                var createdgift = await client.PostAsync(giftInput);

                result = RedirectToAction(nameof(Index));
            }

            return result;
        }

        public async Task<ActionResult> Edit(int id)
        {
            ActionResult result = View(id);

            if (ModelState.IsValid)
            {
                HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

                var client = new GiftClient(httpClient);
                var fetchedGift = await client.GetAsync(id);

                result = View(fetchedGift);
            }

            return result;
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, GiftInput giftInput)
        {
            ActionResult result = View();

            if (ModelState.IsValid)
            {
                HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

                var client = new GiftClient(httpClient);
                var updatedGift = await client.PutAsync(id, giftInput);

                result = RedirectToAction(nameof(Index));
            }

            return result;
        }
    }
}
