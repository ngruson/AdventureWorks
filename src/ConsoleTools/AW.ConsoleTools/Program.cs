using AW.ConsoleTools;
using AW.Services.Product.Core;
using AW.Services.Product.Core.Handlers.GetAllProductsWithPhotos;
using AW.Services.Product.Core.Handlers.StoreProductPhotos;
using AW.Services.Product.Infrastructure.EFCore;
using AW.Services.SharedKernel.EFCore;
using AW.SharedKernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

//Log.Logger = new LoggerConfiguration()
//    .MinimumLevel.Verbose()    
//    .Enrich.FromLogContext()
//    .WriteTo.Console()
//    .CreateLogger();

using IHost host = Host.CreateDefaultBuilder(args).Build();
var configuration = host.Services.GetRequiredService<IConfiguration>();

var serviceProvider = new ServiceCollection()
    .AddScoped(_ => configuration)
    .AddTransient(provider =>
    {
        var builder = new DbContextOptionsBuilder<AWContext>();
        builder.UseSqlServer(configuration["ConnectionStrings:DbConnection"]);
        builder.AddInterceptors(new AzureAdAuthenticationDbConnectionInterceptor());

        return new AWContext(
            builder.Options,
            typeof(EfRepository<>).Assembly,
            provider.GetService<IMediator>()
        );
    })
    .AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
    .AddAutoMapper(typeof(MappingProfile).Assembly, typeof(GetAllProductsWithPhotosQuery).Assembly)
    .AddMediatR(typeof(GetAllProductsWithPhotosQuery))
    .AddLogging()
    .BuildServiceProvider();

var targetFolder = Path.Combine(
    Env.RepositoryRoot,
    "src", "services", "product", "productphotos"
);

var mediator = serviceProvider.GetRequiredService<IMediator>();

await mediator.Send(new StoreProductPhotosCommand {  TargetFolder = targetFolder });