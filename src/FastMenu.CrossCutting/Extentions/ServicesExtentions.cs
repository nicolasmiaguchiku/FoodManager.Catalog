using FastMenu.Domain.Services;
using FastMenu.Domain.Interfaces;
using FastMenu.Infrastructure.Caching;
using FastMenu.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FastMenu.CrossCutting.Extentions
{
    public static class ServicesExtentions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IFoodRepository, FoodRepository>();
            services.AddScoped<ICacheService, MemoryCacheService>();

            return services;
        }
    }
}