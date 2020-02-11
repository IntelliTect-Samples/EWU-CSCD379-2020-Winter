using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Api;

namespace SecretSanta.Web.Controllers
{
    public class GroupController : Controller
    {

        IHttpClientFactory ClientFactory { get; set; }
        public GroupController(IHttpClientFactory clientFactory)
        {

            if (clientFactory is null)
            {
                throw new ArgumentNullException(nameof(clientFactory));
            }
            ClientFactory = clientFactory;
        }
        //Get: Group
        public async Task<IActionResult> Index()
        {
            HttpClient client = ClientFactory.CreateClient("SecretSantaApi");
            var groupClient = new GroupClient(client);
            var groups = await groupClient.GetAllAsync();
            return View(groups);
        }

    }

} 
