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
using MP.MKKing.API.Helpers;
using MP.MKKing.API.Middleware;
using MP.MKKing.Infra.CrossCutting.Bootstrapper;

namespace MP.MKKing.API
{
    public class Startup
    {
        public const string jwtSecret = "409F9F5D-6CB8-48ED-A1C0-AD602950B308";
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

            services.Register(Configuration);
            
            // services.AddJwtConfiguration(System.Text.Encoding.ASCII.GetBytes(jwtSecret));

            // Get the model state errors inside actionContext (which is what the ApiController attribute is using 
            // to populate any errors that is related to validation, adding it to a model state dictionary).
            services.Configure<ApiBehaviorOptions>(options => 
            {
                options.InvalidModelStateResponseFactory = actionContext => 
                {
                    // Extract the errors if there are any, and populate them into an array, which we pass into our ApiValidationErrorResponse
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();
                    
                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

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

            app.UseSwaggerConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
