using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SecretSanta.Web
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("https://localhost:44394").UseStartup<Startup>();
                });
    }
}
