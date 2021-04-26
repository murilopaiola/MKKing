using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MP.MKKing.API.Configuration.JWT;
using MP.MKKing.API.Configuration.Swagger;
using MP.MKKing.API.Errors;
using MP.MKKing.API.Extensions;
using MP.MKKing.API.Helpers;
using MP.MKKing.API.Middleware;

namespace MP.MKKing.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddDbContexts(Configuration);

            services.AddAppServices();

            services.AddIdentityServices(Configuration);
            
            services.AddSwagger();

            // Add CORS, so that any client that is not coming from port 4200 doesn't receive the headers that allow the browser to access our resources
            services.AddCors(opt => 
            {
                opt.AddPolicy("CorsPolicy", policy => 
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Use an exception handling middleware
            app.UseMiddleware<ExceptionMiddleware>();
            
            // Middleware to redirect missing endpoint requests to our errors controller
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwaggerConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
