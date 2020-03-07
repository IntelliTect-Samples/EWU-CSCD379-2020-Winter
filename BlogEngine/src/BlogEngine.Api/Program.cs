using BlogEngine.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Security.Policy;

namespace BlogEngine.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;
                using var context = services.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(config =>
                    config.AddInMemoryCollection(
                        new Dictionary<string, string> { { "ConnectionStrings:DefaultConnection", "Data Source=Blog.db" } })
                )
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
