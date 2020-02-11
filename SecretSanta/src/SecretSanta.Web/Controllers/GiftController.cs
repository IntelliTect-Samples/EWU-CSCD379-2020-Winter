using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Api;

namespace SecretSanta.Web.Controllers
{
    public class GiftController : Controller
    {
        public IHttpClientFactory ClientFactory { get; }

        public GiftController(IHttpClientFactory clientFactory)
        {
            if (clientFactory is null)
                throw new ArgumentNullException(nameof(clientFactory));

            ClientFactory = clientFactory;
        }
        
        public async Task<ActionResult> IndexAsync()
        {
            HttpClient httpClient = ClientFactory.CreateClient("SantaApi");
            GiftClient client = new GiftClient(httpClient);

            ICollection<Gift> users = await client.GetAllAsync();
            return View(users);
        }
    }
}