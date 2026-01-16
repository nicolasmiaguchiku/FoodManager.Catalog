using FluentValidation;
using FluentValidation.AspNetCore;
using FoodManager.Catalog.Application.Validations;
using FoodManager.Internal.Shared.Dtos;
using FoodManager.Internal.Shared.Http.Catalog.Requests;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.DependencyInjection;

namespace FoodManager.Catalog.CrossCutting.Extentions
{
    public static class ValidationExtention
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation(options =>
            {
                options.DisableDataAnnotationsValidation = true;
            });

            services.AddScoped<IValidator<JsonPatchError>, JsonPatchValidator>();
            services.AddScoped<IValidator<FoodDto>, FoodDtoValidator>();
            services.AddScoped<IValidator<AddFoodRequest>, AddFoodValidator>();
            
            return services;
        }
    }
}