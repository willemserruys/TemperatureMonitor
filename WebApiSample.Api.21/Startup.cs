using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
// using Swashbuckle.AspNetCore.Swagger;
using HourRegistration.DataAccess;
using HourRegistration.DataAccess.Repositories;

using NSwag;
using NSwag.AspNetCore;


namespace HourRegistration.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<UserRepository>();
            services.AddDbContext<UserContext>();

            // Configure CORS
            services.AddCors(corsOptions => corsOptions.AddPolicy(
                "Default",
                corsPolicyBuilder => corsPolicyBuilder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()));

            // Register Mvc
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerDocument();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Register the Swagger generator and the Swagger UI middlewares
            //app.UseSwaggerUi3WithApiExplorer(settings =>
            //{
            //    //settings.GeneratorSettings.DefaultPropertyNameHandling =
            //    //    PropertyNameHandling.CamelCase;
            //});

            app.UseSwagger();
            app.UseSwaggerUi3(c =>
            {
                c.Path = string.Empty;
            }
            );
            // app.UseHttpsRedirection();

            // Enabling CORS
            app.UseCors("Default");

            app.UseMvc();
        }
    }
}
