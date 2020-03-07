using AutoMapper;
using BlogEngine.Business;
using BlogEngine.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Text;

namespace BlogEngine.Api
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.EnableSensitiveDataLogging()
                       .UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IPostService, PostService>();

            System.Type profileType = typeof(AutomapperProfileConfiguration);
            System.Reflection.Assembly assembly = profileType.Assembly;
            services.AddAutoMapper(new[] { assembly });

            services.AddControllers();

            services.AddSwaggerDocument();

            services.AddCors(options =>
             {
                 options.AddDefaultPolicy(builder =>
                  {
                      builder.AllowAnyOrigin()
                             .AllowAnyMethod()
                             .AllowAnyHeader();
                  });
             });
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            StringBuilder message = new StringBuilder("Configuration:\n");
            foreach(var configItem in configuration.AsEnumerable().OrderBy(item=>item.Key))
            {
                message.AppendLine( $"\t{configItem.Key}={configItem.Value}");
            }
            logger.LogInformation(message.ToString());

            app.UseRouting();

            app.UseOpenApi();
            //http://localhost/swagger
            app.UseSwaggerUi3();

            app.UseCors();

            app.UseEndpoints(endpoint =>
            {
                endpoint.MapDefaultControllerRoute();
            });
        }
    }
}
