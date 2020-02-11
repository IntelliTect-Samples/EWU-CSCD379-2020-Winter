using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Api;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SecretSanta.Web.Controllers
{
    public class GiftController : Controller
    {
        public IHttpClientFactory HttpClientFactory { get; }

        public GiftController(IHttpClientFactory httpClientFactory) =>
            HttpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));

        public async Task<ActionResult> IndexAsync()
        {
            HttpClient httpClient = HttpClientFactory.CreateClient("SecretSantaApi");
            GiftClient client = new GiftClient(httpClient);
            ICollection<Gift> gifts = await client.GetAllAsync();
            return View(gifts);
        }
    }
}