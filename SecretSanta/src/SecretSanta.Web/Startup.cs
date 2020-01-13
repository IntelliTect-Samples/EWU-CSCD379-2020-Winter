using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace SecretSanta.Web
{
#pragma warning disable CA1052 // Static holder types should be Static or NotInheritable
    // referenced elsewhere in a way that depends on it being instantiatable
    public class Startup
#pragma warning restore CA1052 // Static holder types should be Static or NotInheritable
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        //public void ConfigureServices(Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        //{
        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task, fine to suppress. discussed on gitter
                    await context.Response.WriteAsync("Hello from Web!");
#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task, fine to suppress. discussed on gitter
                });
            });
        }
    }
}
