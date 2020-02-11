using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Api;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SecretSanta.Web.Controllers
{
    public class GroupController : Controller
    {
        public IHttpClientFactory HttpClientFactory { get; }

        public GroupController(IHttpClientFactory httpClientFactory) =>
            HttpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));

        public async Task<ActionResult> IndexAsync()
        {
            HttpClient httpClient = HttpClientFactory.CreateClient("SecretSantaApi");
            GroupClient client = new GroupClient(httpClient);
            ICollection<Group> groups = await client.GetAllAsync();
            return View(groups);
        }

    }
}