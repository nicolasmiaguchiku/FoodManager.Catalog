using FluentValidation;
using FluentValidation.AspNetCore;
using FoodManager.Catalog.Application.Dtos;
using FoodManager.Catalog.Application.Input.Requests;
using FoodManager.Catalog.Application.Validations;
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

            services.AddScoped<IValidator<AddFoodRequest>, AddFoodValidator>();
            services.AddScoped<IValidator<UpdateFoodRequest>, UpdateFoodRequestValidator>();
            services.AddScoped<IValidator<JsonPatchError>, JsonPatchValidator>();
            services.AddScoped<IValidator<FoodDto>, FoodDtoValidator>();

            return services;
        }
    }
}