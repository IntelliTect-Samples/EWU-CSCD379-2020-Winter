using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Api;

namespace SecretSanta.Web.Controllers
{

    public class GiftsController : Controller
    {

        public GiftsController(IHttpClientFactory clientFactory)
        {
            var httpClient = clientFactory?.CreateClient("SecretSantaApi")
                          ?? throw new ArgumentNullException(nameof(clientFactory));
            Client = new GiftClient(httpClient);
        }

        private GiftClient Client { get; }

        public async Task<IActionResult> Index()
        {
            var gifts = await Client.GetAllAsync();

            return View(gifts);
        }

    }

}
