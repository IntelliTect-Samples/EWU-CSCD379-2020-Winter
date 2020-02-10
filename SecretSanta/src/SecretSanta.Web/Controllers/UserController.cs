using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Api;

namespace SecretSanta.Web.Controllers
{
    public class UserController : Controller
    {
        public IHttpClientFactory ClientFactory { get; }

        public UserController(IHttpClientFactory clientFactory)
        {
            if (clientFactory is null)
                throw new System.ArgumentNullException(nameof(clientFactory));

            ClientFactory = clientFactory;
        }

        // GET: User
        public async Task<ActionResult> IndexAsync()
        {
            HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

            var client = new UserClient(httpClient);
            var users = await client.GetAllAsync();

            return View(users);
        }
    }
}