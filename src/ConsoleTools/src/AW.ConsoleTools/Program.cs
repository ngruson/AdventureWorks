using AW.ConsoleTools;
using AW.ConsoleTools.AutoMapper;
using AW.ConsoleTools.DependencyInjection;
using AW.ConsoleTools.Handlers.CreateLoginsForCustomers;
using AW.Services.Customer.Core.Handlers.GetAllCustomers;
using AW.Services.IdentityServer.Core.Handlers.CreateLogin;
using AW.Services.Product.Core.Handlers.GetAllProductsWithPhotos;
using AW.Services.Product.Core.Handlers.StoreProductPhotos;
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
            .AddDbContext()
            .AddCustomerServices()
            .AddProductServices()
            .AddIdentityServerServices()
            .AddAutoMapper(
                typeof(MappingProfile).Assembly, 
                typeof(GetAllProductsWithPhotosQuery).Assembly,
                typeof(GetAllCustomersQuery).Assembly
            )
            .AddMediatR(
                typeof(Program).Assembly, 
                typeof(GetAllProductsWithPhotosQuery).Assembly,
                typeof(GetAllCustomersQuery).Assembly,
                typeof(CreateLoginCommand).Assembly
             );
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
        await mediator.Send(new StoreProductPhotosCommand { TargetFolder = targetFolder });
    else if (args[0] == "create-customer-logins")
        await mediator.Send(new CreateLoginsForCustomersCommand());
}