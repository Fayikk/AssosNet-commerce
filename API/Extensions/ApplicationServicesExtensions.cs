using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Repository;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services){
            services.AddScoped<ITokenService , TokenService>();
            services.AddScoped<IOrderService , OrderService>();
            services.AddScoped<IProductRepository , ProductRepository>();
            services.AddScoped<IBasketRepository , BasketRepository>();
            services.AddScoped(typeof(IGenericRepository<>),(typeof(GenericRepository<>)));
            services.Configure<ApiBehaviorOptions>(options => {
                options.InvalidModelStateResponseFactory = ActionContext => {
                    var errors = ActionContext.ModelState
                        .Where(e=>e.Value.Errors.Count>0)
                        .SelectMany(x=>x.Value.Errors)
                        .Select(x=>x.ErrorMessage).ToArray();
                        
                        var errorResponse = new ApiValidationErrorResponse{
                            Errors = errors
                        };
                        return new BadRequestObjectResult(errorResponse);
                };
            
            });
            return services;
        }
    }
}