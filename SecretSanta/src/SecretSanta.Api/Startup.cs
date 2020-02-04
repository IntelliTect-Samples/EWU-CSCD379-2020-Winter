using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SecretSanta.Business;
using SecretSanta.Business.Services;
using SecretSanta.Data;

namespace SecretSanta.Api
{

    // Justification: Disable until ConfigureServices is added back.
#pragma warning disable CA1052 // Static holder types should be Static or NotInheritable
    public class Startup
#pragma warning restore CA1052 // Static holder types should be Static or NotInheritable
    {

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
// We want the connection to exist for the duration of our program running
#pragma warning disable CA2000 // Dispose objects before losing scope
            var connection = new SqliteConnection("DataSource=:memory:");
#pragma warning restore CA2000
            connection.Open();

            services.AddDbContext<ApplicationDbContext>(
                options => options.EnableSensitiveDataLogging().UseSqlite(connection));

            services.AddScoped<IUserService, UserService>()
                    .AddScoped<IGiftService, GiftService>()
                    .AddScoped<IGroupService, GroupService>();

            services.AddAutoMapper(typeof(AutomapperConfigurationProfile).Assembly)
                    .AddSwaggerDocument()
                    .AddMvc(options => options.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting().UseOpenApi().UseMvc().UseSwaggerUi3();
        }

    }

}
