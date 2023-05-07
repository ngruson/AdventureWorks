using AW.Services.HumanResources.Core.Handlers.CreateShift;
using AW.Services.HumanResources.Core.Handlers.DeleteShift;
using AW.Services.HumanResources.Core.Handlers.UpdateShift;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AW.Services.HumanResources.SharedKernel
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateShiftCommand>, CreateShiftCommandValidator>();
            services.AddScoped<IValidator<UpdateShiftCommand>, UpdateShiftCommandValidator>();
            services.AddScoped<IValidator<DeleteShiftCommand>, DeleteShiftCommandValidator>();

            return services;
        }
    }
}
