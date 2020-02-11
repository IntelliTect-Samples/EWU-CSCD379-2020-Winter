
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using SecretSanta.Web.Api;
using Microsoft.AspNetCore.Mvc;

namespace SecretSanta.Web.Controllers
{
    public class GiftController : Controller
    {
        public GiftController(IHttpClientFactory clientFactory)
        {
            if (clientFactory is null)
            {
                throw new System.ArgumentNullException(nameof(clientFactory));
            }

            ClientFactory = clientFactory;
        }

        public IHttpClientFactory ClientFactory { get; }

        // GET: Gift
        public async Task<ActionResult> Index()
        {
            HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

            var client = new GiftClient(httpClient);
            ICollection<Gift> gifts = await client.GetAllAsync();
            return View(gifts);
        }
    }
}