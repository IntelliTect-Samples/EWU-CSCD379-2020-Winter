using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BlogEngine.Web
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddHttpClient("BlogApi", options =>
            {
                options.BaseAddress = new Uri(Configuration["ApiUrl"]);
            });
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            StringBuilder message = new StringBuilder("Configuration:");
            foreach (var configItem in configuration.AsEnumerable().OrderBy(item => item.Key))
            {
                message.AppendLine($"\t{configItem.Key}={configItem.Value}");
            }
            logger.LogInformation(message.ToString());

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("post", "{controller=Blog}/{action=Post}/{year}/{month}/{day}/{slug}");
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
