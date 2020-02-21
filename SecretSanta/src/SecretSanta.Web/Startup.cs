using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.Configuration;

namespace SecretSanta.Web
{

    public class Startup
    {

        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddControllersWithViews();

            services.AddHttpClient("SecretSantaApi", options => options.BaseAddress = new Uri(Configuration["ApiUrl"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
#pragma warning disable CA1822 // Told not to make these static
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
#pragma warning restore CA1822
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseEndpoints(endpoint => endpoint.MapDefaultControllerRoute());
        }

    }

}
