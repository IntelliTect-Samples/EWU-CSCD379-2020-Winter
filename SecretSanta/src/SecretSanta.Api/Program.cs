using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace SecretSanta.Api
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
                    config.AddInMemoryCollection
                    (
                        new Dictionary<string, string> { { "ConnectionStrings:DefaultConnection", "Data Source=SecretSanta.db" } }
                    ))
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseUrls("https://localhost:44388").UseStartup<Startup>();
            });
    }
}
