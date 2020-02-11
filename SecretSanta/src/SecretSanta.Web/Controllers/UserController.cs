using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Api;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SecretSanta.Web.Controllers
{
    public class UserController : Controller
    {
        public IHttpClientFactory HttpClientFactory { get; }

        public UserController(IHttpClientFactory httpClientFactory) =>
            HttpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));

        public async Task<ActionResult> IndexAsync()
        {
            HttpClient httpClient = HttpClientFactory.CreateClient("SecretSantaApi");
            UserClient client = new UserClient(httpClient);
            ICollection<User> users = await client.GetAllAsync();
            return View(users);
        }
    }
}