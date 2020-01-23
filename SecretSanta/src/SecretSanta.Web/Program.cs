using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SecretSanta.Web
{
<<<<<<< master
    public static class Program
=======
    static public class Program
>>>>>>> Refactored out suppression and msbuild files.
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
