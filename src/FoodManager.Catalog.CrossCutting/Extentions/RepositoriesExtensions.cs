using FoodManager.Catalog.Domain.Interfaces.Repositories;
using FoodManager.Catalog.Domain.Interfaces.Services;
using FoodManager.Catalog.Infrastructure.Persistence.Repositories;
using FoodManager.Catalog.Infrastructure.Persistence.Services;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;

namespace FoodManager.Catalog.CrossCutting.Extentions
{
    public static class RepositoriesExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IFoodRepository, FoodRepository>();
            services.AddScoped<ICacheService, MemoryCacheService>();
            services.AddSingleton<JwtSecurityTokenHandler>();

            return services;
        }
    }
}