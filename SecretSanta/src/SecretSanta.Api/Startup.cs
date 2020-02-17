using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SecretSanta.Business;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using System;

namespace SecretSanta.Api
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) =>
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerDocument();

            services.AddScoped<IGiftService, GiftService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGroupService, GroupService>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.EnableSensitiveDataLogging()
                       .UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(new[] { typeof(AutomapperConfigurationProfile).Assembly });
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseRouting();

            app.UseEndpoints(endpoint =>
                endpoint.MapDefaultControllerRoute()
            );
        }
    }
}
