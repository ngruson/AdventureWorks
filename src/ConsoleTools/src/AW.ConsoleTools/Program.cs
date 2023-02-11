using AW.ConsoleTools;
using AW.ConsoleTools.AutoMapper;
using AW.ConsoleTools.DependencyInjection;
using AW.ConsoleTools.Handlers.AzureAD.CreateGroups;
using AW.ConsoleTools.Handlers.AzureAD.CreateUsers;
using AW.ConsoleTools.Handlers.CreateLoginsForCustomers;
using AW.Services.Customer.Core.Handlers.GetAllCustomers;
using AW.Services.HumanResources.Core.Handlers.GetAllEmployees;
using AW.Services.IdentityServer.Core.Handlers.CreateLogin;
using AW.Services.Product.Core.Handlers.GetAllProductsWithPhotos;
using AW.Services.Product.Core.Handlers.StoreProductPhotos;
using AW.SharedKernel.FileHandling;
using AW.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services
            .AddSingleton<IAWContextFactory, AWContextFactory>()
            .AddCustomerServices()
            .AddHumanResourcesServices()
            .AddProductServices()            
            .AddIdentityServerServices()
            .AddGraphClient()
            .AddAutoMapper(
                typeof(MappingProfile).Assembly, 
                typeof(GetAllProductsWithPhotosQuery).Assembly,
                typeof(GetAllCustomersQuery).Assembly,
                typeof(GetAllEmployeesQuery).Assembly
            )
            .AddMediatR(
                typeof(Program).Assembly, 
                typeof(GetAllProductsWithPhotosQuery).Assembly,
                typeof(GetAllCustomersQuery).Assembly,
                typeof(GetAllEmployeesQuery).Assembly,
                typeof(CreateLoginCommand).Assembly
             )
            .AddScoped<IFileHandler, FileHandler>();
    })
    .UseSerilog()
    .Build();

var targetFolder = Path.Combine(
    Env.RepositoryRoot,
    "src", "services", "product", "productphotos"
);

using (var scope = host.Services.CreateScope())
{
    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

    if (args[0] == "store-product-photos")
        await mediator.Send(new StoreProductPhotosCommand(targetFolder));
    else if (args[0] == "create-customer-logins")
        await mediator.Send(new CreateLoginsForCustomersCommand());
    else if (args[0] == "create-aad-groups")
        await mediator.Send(new CreateGroupsCommand());
    else if (args[0] == "create-aad-users")
        await mediator.Send(new CreateUsersCommand());
}