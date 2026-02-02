using Flurl;
using FoodManager.Internal.Shared.Http.Auth.Clients;
using FoodManager.Internal.Shared.Http.Auth.Models;
using Microsoft.Extensions.DependencyInjection;

namespace FoodManager.Catalog.CrossCutting.Extentions
{
    public static class ClientsExtensions
    {
        public static IServiceCollection AddApiClients(this IServiceCollection services, KeycloakSettings keycloakSettings)
        {
            services.AddHttpClient<IAuthClient, AuthClient>(client =>
            {
                client.DefaultRequestHeaders.Add("Tenant", "FoodManager");
                client.BaseAddress = new Uri(keycloakSettings.BaseUrl
                        .AppendPathSegment("api")
                        .AppendPathSegment("v1/"));
            });

            return services;
        }
    }
}