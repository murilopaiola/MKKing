using Microsoft.Extensions.DependencyInjection;
using MP.MKKing.Infra.Data.Repositories;
using MP.MKKing.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MP.MKKing.API.Errors;
using MP.MKKing.Infra.Services;

namespace MP.MKKing.API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
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


            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}