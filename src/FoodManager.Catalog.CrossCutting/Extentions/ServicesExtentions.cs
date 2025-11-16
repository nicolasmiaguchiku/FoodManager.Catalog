using FoodManager.Catalog.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using FoodManager.Catalog.Domain.Interfaces.Services;
using FoodManager.Catalog.Domain.Interfaces.Repositories;
using FoodManager.Catalog.Infrastructure.Persistence.Services;

namespace FoodManager.Catalog.CrossCutting.Extentions
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