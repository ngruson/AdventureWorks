using AW.Services.HumanResources.Core.Handlers.CreateDepartment;
using AW.Services.HumanResources.Core.Handlers.CreateShift;
using AW.Services.HumanResources.Core.Handlers.DeleteDepartment;
using AW.Services.HumanResources.Core.Handlers.DeleteShift;
using AW.Services.HumanResources.Core.Handlers.UpdateDepartment;
using AW.Services.HumanResources.Core.Handlers.UpdateEmployee;
using AW.Services.HumanResources.Core.Handlers.UpdateShift;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AW.Services.HumanResources.SharedKernel
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateDepartmentCommand>, CreateDepartmentCommandValidator>();
            services.AddScoped<IValidator<UpdateDepartmentCommand>, UpdateDepartmentCommandValidator>();
            services.AddScoped<IValidator<DeleteDepartmentCommand>, DeleteDepartmentCommandValidator>();

            services.AddScoped<IValidator<UpdateEmployeeCommand>, UpdateEmployeeCommandValidator>();

            services.AddScoped<IValidator<CreateShiftCommand>, CreateShiftCommandValidator>();
            services.AddScoped<IValidator<UpdateShiftCommand>, UpdateShiftCommandValidator>();
            services.AddScoped<IValidator<DeleteShiftCommand>, DeleteShiftCommandValidator>();

            return services;
        }
    }
}
